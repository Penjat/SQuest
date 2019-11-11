using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour {

    MoveButtonDelegate _delegate;
    public Text _buttonLabel;
    public Button _button;
    public PartIndicator _partIndicator;
    private bool _isLocked;

    Move _move;

    public void SetUp(MoveButtonDelegate moveButtonDelegate, Move move){
        _delegate = moveButtonDelegate;
        _buttonLabel.text = move._name;
        _move = move;
        _partIndicator.SetUp(_move._partsUsed);
    }

    public void WasPressed(){

        _delegate.MoveButtonPressed(_move);
    }
    public void SetLocked(bool b){
        _isLocked = b;
        _button.enabled = !b;
        if(_isLocked){
            _button.image.color = Color.grey;
        }else{
            _button.image.color = Color.white;
        }
    }

}

public interface MoveButtonDelegate{
    void MoveButtonPressed(Move move);
}
