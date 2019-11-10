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

    public void StartGame(MiniGameDelegate miniGameDelegate){
        //TODO pass in info
        _delegate = miniGameDelegate;
        gameObject.SetActive(true);
        ShowText();
    }
    public void ShowText(){
        //TODO calculate time properly
        _gameState = GameState.TypingText;
        string toType = "here is some text";
        _descriptionLabel.StartTyping(this, toType, 0.1f, 2.0f);
    }
    public void StartRound(){
        _gameState = GameState.Playing;
        _timer = 10.0;
        foreach(ActionInput actionInput in _actionInputs){
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
