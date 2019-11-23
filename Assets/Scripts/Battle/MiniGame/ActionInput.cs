using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionInput : MonoBehaviour {

    private ActionInputDelegate _delegate;
    private MoveType _moveType;
    private KeyCode _keyCode;
    public Animator _animator;

    private Gem[] _gems;
    public RectTransform _track;
    public GameObject _gemPrefab;

    private bool _isMoving = false;
    private bool _isNeeded = false;
    private int _curIndex = 0;

    private float _edgeOfScreen = 400.0f;
    private float _spacing = 160.0f;


    private Vector2 _startPos = new Vector2(0.0f,0.0f);
    private Vector2 _endPos;
    private float _timer = 0.0f;
    private float _travelTime = 10.0f;

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
    public void StartMoving(){
        _curIndex = 0;
        _timer = 0.0f;
        float endY = -(160.0f*6.0f + _edgeOfScreen);
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
        if(_curIndex < _gems.Length){
            Gem gem = _gems[_curIndex];
            if (gem.CheckMissed(_track.anchoredPosition.y)){
                ClearCurGem(true);
            }
        }
    }

    public void CreateGems(){
        //check if needed for this round
        if(!_isNeeded){
            return;
        }
        int numGems = 5;
        _track.anchoredPosition = new Vector2(0.0f,0.0f);
        _gems = new Gem[numGems];
        for(int i=0;i<numGems;i++){
            CreateGem(i);
        }
    }
    private void CreateGem(int index){
        GameObject g = Instantiate(_gemPrefab);
        g.transform.SetParent(_track);

        Gem gem = g.GetComponent<Gem>();
        float pos = _edgeOfScreen + index*_spacing;
        gem.SetPosition(pos);
        _gems[index] = gem;
    }
    public void WasPressed(){
        Debug.Log("should delete the next gem");

        //clear the next gem according to the curIndex
        //make sure not outside of range
        if(_curIndex < _gems.Length){
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
            _delegate.GemMissed(_moveType);
            return;
        }
        //TODO calculate accuracy
        _delegate.GemCleared(_moveType);
    }
}

public interface ActionInputDelegate{
    void GemCleared(MoveType moveType);
    void GemMissed(MoveType moveType);
}
