using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCategory : MonoBehaviour {
    MoveCategoryDelegate _delegate;
    public MoveType _type;

    public void SetUp(MoveCategoryDelegate categoryDelegate){
        _delegate = categoryDelegate;
    }
    public void WasPressed(){
        _delegate.CategoryPressed(_type);
    }
}



public interface MoveCategoryDelegate {
    void CategoryPressed(MoveType moveType);
}
