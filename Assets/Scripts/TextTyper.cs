using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum TypeState{
    Done,Waiting,Typing
};

public class TextTyper : MonoBehaviour {

    private TextTyperDelegate _delegate;

    public Text _textBox;
    private string _toType;
    private string _typed;
    private bool _isTyping = false;
    private float _timer = 0.0f;
    private float _rate;
    private float _waitTime;//how long to wait after
    private int _index;

    void Update() {
        if(_isTyping){
            _timer-=Time.deltaTime;
            if(_timer <= 0.0f){
                //increase the index
                _index++;
                if(_index < _toType.Length){
                    //add the next char
                    AddNextChar();
                }else if(_index == _toType.Length){
                    //if is the last char, wait a little
                    _timer += _waitTime;
                }else{
                    //tell the delegte we are done typing
                    DoneTyping();
                }
            }
        }
    }
    public void StartTyping(TextTyperDelegate textTyperDelegate, string toType, float rate, float waitTime){
        _delegate = textTyperDelegate;
        _isTyping = true;
        _typed = "";
        _textBox.text = "";
        _index = -1;
        _toType = toType;
        _rate = rate;
        _waitTime = waitTime;
    }
    public void SetText(string s){
        _textBox.text = s;
    }
    private void DoneTyping(){
        _isTyping = false;
        //in case no delegate
        if(_delegate != null){
            _delegate.DoneTyping();
        }
    }
    private void AddNextChar(){
        _typed += _toType[_index];
        _timer += _rate;
        _textBox.text = _typed;
    }
}

public interface TextTyperDelegate{
    void DoneTyping();
}
