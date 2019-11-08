using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour {

    MoveButtonDelegate _delegate;
    public Text _buttonLabel;
    Move _move;

    public void SetUp(MoveButtonDelegate moveButtonDelegate, Move move){
        _delegate = moveButtonDelegate;
        _buttonLabel.text = move._name;
        _move = move;
    }

    public void WasPressed(){
        _delegate.MoveButtonPressed(_move);
    }

}

public interface MoveButtonDelegate{
    void MoveButtonPressed(Move move);
}
