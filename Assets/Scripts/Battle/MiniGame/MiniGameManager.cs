﻿using System.Collections;
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

    private double bpm = 120.0;
    private int numberOfBeats = 17;

    void Start(){
        //set up the actionInputs with the correct keycodes
        _actionInputs[TOP].SetUp(this,KeyCode.W);
        _actionInputs[BOTTOM].SetUp(this,KeyCode.S);
        _actionInputs[LEFT].SetUp(this,KeyCode.A);
        _actionInputs[RIGHT].SetUp(this,KeyCode.D);
    }
    public void StartGame(MiniGameDelegate miniGameDelegate, IDictionary<Move, IEnemy[]> actions){
        Debug.Log("starting mini game");
        //TODO pass in info
        _delegate = miniGameDelegate;
        gameObject.SetActive(true);
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Hide();
        }
        //CalcInputActions(partsUsed);//should be an empty set
        Sequence[] sequenceArray = SequenceFactory.CreateSequenceArray(actions);
        for(int i=0;i<sequenceArray.Length;i++){
            _actionInputs[i].CreateGems(0.0f, sequenceArray[i]);
        }

        ShowText();
    }
    private void ShowText(){
        //TODO calculate time properly
        _gameState = GameState.TypingText;
        string toType = "here is some text";
        _descriptionLabel.StartTyping(this, toType, 0.04f, 2.0f);
    }

    public void StartRound(){
        _gameState = GameState.Playing;
        _timer = 10.0;
        foreach(ActionInput actionInput in _actionInputs){
            actionInput.Show(true);
            //actionInput.CreateGems(0.0f,new int[]{8,8,4,4});
            actionInput.StartMoving(bpm, numberOfBeats);
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
        //TODO fix for missed
        _delegate.GemCleared(moveType, 33.0f);
        _backgroundManager.ShowImage(moveType);
    }
    public void GemMissed(MoveType moveType){
        //TODO fix for missed
        _delegate.GemCleared(moveType, 33.0f);
    }
}

public interface MiniGameDelegate{
    void MiniGameFinished();
    void GemCleared(MoveType moveType, float accuracy);
}
