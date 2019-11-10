using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState{
    Waiting,TypingText,Playing
};

public class MiniGameManager : MonoBehaviour, TextTyperDelegate {
    MiniGameDelegate _delegate;
    private GameState _gameState;
    private double _timer = 0.0;

    public ActionInput[] _actionInputs;
    public TextTyper _descriptionLabel;

    private const int TOP = 0;
    private const int BOTTOM = 1;
    private const int RIGHT = 2;
    private const int LEFT = 3;

    public void StartGame(MiniGameDelegate miniGameDelegate, HashSet<MoveType> partsUsed){
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
        _descriptionLabel.StartTyping(this, toType, 0.1f, 2.0f);
    }
    private void CalcInputActions(HashSet<MoveType> partsUsed){
        _actionInputs[RIGHT].SetNeeded(partsUsed.Contains(MoveType.Hand));
        _actionInputs[TOP].SetNeeded(partsUsed.Contains(MoveType.Mouth));
        _actionInputs[LEFT].SetNeeded(false);
        _actionInputs[BOTTOM].SetNeeded(partsUsed.Contains(MoveType.Ass));

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
}

public interface MiniGameDelegate{
    void MiniGameFinished();
}
