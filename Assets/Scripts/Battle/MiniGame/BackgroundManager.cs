using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour{
    public Animator _animator;

    public void ShowImage(MoveType moveType){
        //TODO pass in more info
        Debug.Log("movetype = " + moveType);


        switch(moveType){
            case MoveType.Ass:
            _animator.Play("test");
            break;
            case MoveType.Mouth:
            _animator.Play("bj");
            break;
            case MoveType.Hand:
            //_animator.Play("hj");
            break;
            case MoveType.Breasts:
            _animator.Play("breasts");
            break;
        }
        _animator.SetTrigger("willShow");
    }
}
