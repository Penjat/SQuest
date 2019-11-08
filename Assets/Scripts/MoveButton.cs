using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour {

    MoveButtonDelegate _delegate;
    public Text _buttonLabel;

    public void SetUp(MoveButtonDelegate moveButtonDelegate, Move move){
        _delegate = moveButtonDelegate;
        _buttonLabel.text = move._name;
    }

    public void WasPressed(){
        _delegate.MoveButtonPressed();
    }

}

public interface MoveButtonDelegate{
    void MoveButtonPressed();
}
