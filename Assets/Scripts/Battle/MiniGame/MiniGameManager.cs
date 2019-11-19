using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState{
    Waiting,TypingText,Playing
};

public class MiniGameManager : MonoBehaviour, TextTyperDelegate, ActionInputDelegate {
    MiniGameDelegate _delegate;
    private GameState _gameState;
    private double _timer = 0.0;

    public ActionInput[] _actionInputs;
    public TextTyper _descriptionLabel;

    private const int TOP = 0;
    private const int BOTTOM = 1;
    private const int RIGHT = 2;
    private const int LEFT = 3;

    void Start(){
        //set up the actionInputs with the correct keycodes
        _actionInputs[TOP].SetUp(this,KeyCode.W);
        _actionInputs[BOTTOM].SetUp(this,KeyCode.S);
        _actionInputs[LEFT].SetUp(this,KeyCode.A);
        _actionInputs[RIGHT].SetUp(this,KeyCode.D);
    }
    public void StartGame(MiniGameDelegate miniGameDelegate, IDictionary<MoveType,Move> partsUsed){
        //TODO pass in info
        _delegate = miniGameDelegate;
        gameObject.SetActive(true);
        CalcInputActions(partsUsed);
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Show(false);
        }
        ShowText();
    }
    public void ShowText(){
        //TODO calculate time properly
        _gameState = GameState.TypingText;
        string toType = "here is some text";
        _descriptionLabel.StartTyping(this, toType, 0.04f, 2.0f);
    }
    private void CalcInputActions(IDictionary<MoveType,Move> partsUsed){
        if(partsUsed.ContainsKey(MoveType.Hand)){
            _actionInputs[RIGHT].SetActive(MoveType.Hand);
        }
        if(partsUsed.ContainsKey(MoveType.Mouth)){
            _actionInputs[TOP].SetActive(MoveType.Mouth);
        }
        if(partsUsed.ContainsKey(MoveType.Breasts)){
            _actionInputs[LEFT].SetActive(MoveType.Breasts);
        }
        if(partsUsed.ContainsKey(MoveType.Ass)){
            _actionInputs[BOTTOM].SetActive(MoveType.Ass);
        }
    }
    public void StartRound(){
        _gameState = GameState.Playing;
        _timer = 10.0;
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Show(true);
            actionInput.CreateGems();
            actionInput.StartMoving();
        }
    }
    void Update(){
        if(_gameState == GameState.Playing){
            _timer -= Time.deltaTime;
            if(_timer <= 0.0){
                EndRound();
            }
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
        _delegate.GemCleared(moveType, 33.0f);
    }
}

public interface MiniGameDelegate{
    void MiniGameFinished();
    void GemCleared(MoveType moveType, float accuracy);
}
