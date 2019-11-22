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
        CheckActionInput(partsUsed, _actionInputs[RIGHT], MoveType.Hand);
        CheckActionInput(partsUsed, _actionInputs[TOP], MoveType.Mouth);
        CheckActionInput(partsUsed, _actionInputs[LEFT], MoveType.Breasts);
        CheckActionInput(partsUsed, _actionInputs[BOTTOM], MoveType.Ass);
    }
    private void CheckActionInput(IDictionary<MoveType,Move> partsUsed, ActionInput actionInput, MoveType moveType){
        if(partsUsed.ContainsKey(moveType)){
            actionInput.SetActive(moveType);
            return;
        }
        actionInput.Hide();
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
        _backgroundManager.ShowImage(moveType);
    }
}

public interface MiniGameDelegate{
    void MiniGameFinished();
    void GemCleared(MoveType moveType, float accuracy);
}
