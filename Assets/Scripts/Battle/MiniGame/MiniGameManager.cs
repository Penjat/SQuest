using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState{
    Waiting,TypingText,Playing
};

public class MiniGameManager : MonoBehaviour, TextTyperDelegate, ActionInputDelegate {
    MiniGameDelegate _delegate;
    public BackgroundManager _backgroundManager;
    private GameState _gameState;
    private double _timer = 0.0;

    public ActionInput[] _actionInputs;
    public TextTyper _descriptionLabel;

    private const int TOP = 0;
    private const int BOTTOM = 1;
    private const int RIGHT = 2;
    private const int LEFT = 3;

    private float bpm = 80.0f;
    private int numberOfBeats = 16;

    void Start(){
        //set up the actionInputs with the correct keycodes
        _actionInputs[TOP].SetUp(this,KeyCode.W);
        _actionInputs[BOTTOM].SetUp(this,KeyCode.S);
        _actionInputs[LEFT].SetUp(this,KeyCode.A);
        _actionInputs[RIGHT].SetUp(this,KeyCode.D);
    }
    void Update(){
        if(_gameState == GameState.Playing){
            _timer -= Time.deltaTime;
            if(_timer <= 0.0){
                EndRound();
            }
        }
    }
    public void StartGame(MiniGameDelegate miniGameDelegate, IDictionary<Move, IEnemy[]> actions, string battleText){
        Debug.Log("starting mini game");
        //TODO pass in info
        _delegate = miniGameDelegate;
        gameObject.SetActive(true);
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Hide();
        }
        //CalcInputActions(partsUsed);//should be an empty set
        Sequence[] sequenceArray = SequenceFactory.CreateSequenceArray(actions, numberOfBeats);
        for(int i=0;i<sequenceArray.Length;i++){
            _actionInputs[i].CreateGems(0.0f, sequenceArray[i]);
        }

        ShowText(battleText);
    }
    public void StopGame(){
        //Called if player dies
        _timer = 0.0;
        _gameState = GameState.Waiting;
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Clear();
        }
    }
    private void ShowText(string battleText){
        _gameState = GameState.TypingText;
        _descriptionLabel.StartTyping(this, battleText, 0.04f, 2.0f);
    }

    public void StartRound(){
        _gameState = GameState.Playing;
        _timer = (numberOfBeats+1)*(60/bpm);
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Show(true);
            //actionInput.CreateGems(0.0f,new int[]{8,8,4,4});
            actionInput.StartMoving(bpm, numberOfBeats);
        }
    }

    private void EndRound(){
        _timer = 0.0;
        _gameState = GameState.Waiting;
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Clear();
        }
        _delegate.MiniGameFinished();
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
    //------------TextTyperDelegate
    public void DoneTyping(){
        StartRound();
    }
    //------------ActionInputDelegate--------------
    public void GemCleared(MoveType moveType){
        //TODO fix for missed
        _delegate.GemCleared(moveType, 0.0f);
        _backgroundManager.ShowImage(moveType);
    }
    public void GemMissed(MoveType moveType){
        //TODO fix for missed
        _delegate.GemCleared(moveType, 100.0f);
    }
}

public interface MiniGameDelegate{
    void MiniGameFinished();
    void GemCleared(MoveType moveType, float accuracy);
}
