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

    // Update is called once per frame
    void Update() {
        if(_isTyping){
            _timer-=Time.deltaTime;
            if(_timer <= 0.0f){
                //increase the index
                _index++;
                if(_index < _toType.Length){
                    //add the next char
                    _typed += _toType[_index];
                    _timer += _rate;
                    _textBox.text = _typed;
                }else if(_index == _toType.Length){
                    //if is the last char, wait a little
                    _timer += _waitTime;
                }else{
                    //tell the delegte we are done typing
                    _delegate.DoneTyping();
                    _isTyping = false;
                }
            }
        }

    }
    public void StartTyping(TextTyperDelegate textTyperDelegate, string toType, float rate, float waitTime){
        _delegate = textTyperDelegate;
        _isTyping = true;
        _textBox.text = "";
        _index = -1;
        _toType = toType;
        _rate = rate;
        _waitTime = waitTime;
    }
}

public interface TextTyperDelegate{
    void DoneTyping();
}
