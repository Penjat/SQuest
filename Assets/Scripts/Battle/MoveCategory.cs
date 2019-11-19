using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCategory : MonoBehaviour {
    MoveCategoryDelegate _delegate;
    public MoveType _type;
    private Move _selectedMove;
    private bool _isLocked = false;

    //TODO change to use with animator
    public Button _button;
    public GameObject _cancleButton;
    public Animator _animator;
    public Text _moveNameLabel;

    public void SetUp(MoveCategoryDelegate categoryDelegate){
        _delegate = categoryDelegate;
    }
    public void WasPressed(){
        if(_isLocked){
            return;
        }
        _delegate.CategoryPressed(_type);
    }
    public void SetLocked(bool b){
        _isLocked = b;
        if(_isLocked){
            //_button.image.color = Color.grey;
            _animator.Play("FlipCategory");
            return;
        }
        //_button.image.color = Color.white;
        _animator.Play("FlipBack");
    }
    public void SetMove(Move move){
        _selectedMove = move;
        _moveNameLabel.text = move._name;
    }
    public void PressedCancel(){
        _delegate.CancelMove(_type);
    }

    public void MouseEnter(){
        if(_isLocked){
            Debug.Log("Mouse Over: Should show targets");
            _delegate.ShowTargets(_selectedMove);
            return;
        }
        Debug.Log("Mouse Over: Should highlight button");
    }
    public void MouseExit(){
        Debug.Log("Mouse Exit");
        _delegate.HideTargets();
    }

}
public interface MoveCategoryDelegate {
    void CategoryPressed(MoveType moveType);
    void CancelMove(MoveType moveType);
    void ShowTargets(Move selectedMove);
    void HideTargets();
}
