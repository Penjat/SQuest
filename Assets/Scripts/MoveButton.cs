using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour {

    MoveButtonDelegate _delegate;

    public void SetUp(MoveButtonDelegate moveButtonDelegate, Move move){
        _delegate = moveButtonDelegate;
        //TODO set up with move
    }

    public void WasPressed(){
        _delegate.MoveButtonPressed();
    }

}

public interface MoveButtonDelegate{
    void MoveButtonPressed();
}
