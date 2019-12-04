using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionInput : MonoBehaviour {

    private ActionInputDelegate _delegate;
    private MoveType _moveType;
    private KeyCode _keyCode;
    public Animator _animator;
    public GemScore _gemScore;

    private List<Gem> _gems;
    public RectTransform _track;
    public GameObject _gemPrefab;

    private bool _isMoving = false;
    private bool _isNeeded = false;
    private int _curIndex = 0;

    private float _edgeOfScreen = 400.0f;
    private float _spacing = 600.0f;//spacing for one bar
    private float _gemOffset = 0.0f;


    private Vector2 _startPos = new Vector2(0.0f,0.0f);
    private Vector2 _endPos;
    private float _timer = 0.0f;
    private float _travelTime = 6.0f;

    private float _accuracy;

    void Update(){
        if(_isMoving && _isNeeded){
            MoveTrack();
            CheckInput();
            CheckNextGem();
        }
    }
    private void MoveTrack(){
        _timer += Time.deltaTime;
        float normalizedValue = _timer/_travelTime;
        _track.anchoredPosition = Vector3.Lerp(_startPos,_endPos, normalizedValue);
    }
    private void CheckInput(){
        if (Input.GetKeyDown(_keyCode)){
            WasPressed();
        }
    }
    public void SetUp(ActionInputDelegate actionInputDelegate, KeyCode keyCode){
        //set up delegate
        //connect key code to detect press
        _delegate = actionInputDelegate;
        _keyCode = keyCode;

        //set the button's text
        Text buttonText = GetComponentInChildren<Text>();
        buttonText.text = keyCode.ToString();
    }
    public void StartMoving(float bpm,int numberOfBeats){
        _curIndex = 0;
        _timer = 0.0f;
        _accuracy = 0.0f;
        _gemScore.Hide();
        //TODO fix how this is calculated
        _travelTime = (float)numberOfBeats*(60.0f/bpm);
        float endY = -(_spacing/4*numberOfBeats + _edgeOfScreen);
        _endPos = new Vector2(0.0f,endY);
        _isMoving = true;
    }
    public void Clear(){
        _isMoving = false;
        if(_gems == null){
            return;
        }
        foreach(Gem gem in _gems){
            Destroy(gem.gameObject);
        }
        _gems = null;
    }

    private void CheckNextGem(){
        if(_curIndex < _gems.Count){
            Gem gem = _gems[_curIndex];
            if (gem.CheckMissed(_track.anchoredPosition.y)){
                ClearCurGem(true);
            }
            return;
        }
        GemsDone();
    }
    private void GemsDone(){

        _isMoving = false;
        float gemAccuracy = _accuracy / (float)_gems.Count;
        Debug.Log("Done. Accuracy was " + gemAccuracy);
    }

    public void CreateGems(float startingOffset, Sequence sequence){
        Debug.Log("creating gems " + sequence.GetNotes().Length);
        _isNeeded = true;

        Note[] noteArray = sequence.GetNotes();
        SetActive(sequence.GetMoveType());

        //reset where the gems start
        _gemOffset = _edgeOfScreen+startingOffset;
        int numGems = noteArray.Length;
        _track.anchoredPosition = new Vector2(0.0f,0.0f);
        _gems = new List<Gem>();
        for(int i=0;i<numGems;i++){
            Note note = noteArray[i];
            CreateGem(i, note);
        }
    }
    private void CreateGem(int index, Note note){
        if(note.IsRest()){
            _gemOffset += _spacing/note.GetDuration();
            Debug.Log("is rest");
            return;
        }
        GameObject g = Instantiate(_gemPrefab);
        g.transform.SetParent(_track);

        Gem gem = g.GetComponent<Gem>();
        //float pos = _edgeOfScreen + index*_spacing;
        if(note.GetBeatPos()%16 == 0){
            gem.SetDownBeat(true);
        }
        gem.SetPosition(_gemOffset);
        _gemOffset += _spacing/note.GetDuration();
        _gems.Add(gem);
    }
    public void WasPressed(){
        Debug.Log("should delete the next gem");

        //clear the next gem according to the curIndex
        //make sure not outside of range
        if(_curIndex < _gems.Count){
            ClearCurGem();
        }
        _animator.Play("Press");

    }
    public void SetActive(MoveType moveType){
        _isNeeded = true;
        _moveType = moveType;
    }
    public void Hide(){
        _isNeeded = false;
        gameObject.SetActive(false);
    }
    public void Show(bool b){
        //only shows if needed
        //TODO fade in
        gameObject.SetActive(b && _isNeeded);
    }
    private void ClearCurGem(bool wasMissed=false){
        Gem gem = _gems[_curIndex];
        if(!gem.InRange(_track.anchoredPosition.y)){
            return;
        }
        gem.Clear(_track.anchoredPosition.y);
        _curIndex++;
        if(wasMissed){
            //TODO assume 100 accuarcy for miss
            _accuracy += 100.0f;
            _delegate.GemMissed(_moveType);
            return;
        }
        //TODO calculate accuracy
        //pretend has perfect accuracy
        _delegate.GemCleared(_moveType);
    }
    public bool GetIsNeeded(){
        return _isNeeded;
    }
    public void ShowAccuracy(){
        if(_isNeeded){
            float gemAccuracy = Mathf.Round(100.0f - (_accuracy / (float)_gems.Count));
            _gemScore.ShowScore(gemAccuracy);
        }
    }
}

public interface ActionInputDelegate{
    void GemCleared(MoveType moveType);
    void GemMissed(MoveType moveType);
}
