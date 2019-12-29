using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCategory : MonoBehaviour {
    MoveCategoryDelegate _delegate;
    private IBodyPart _bodyPart;
    private Move _selectedMove;
    private bool _isLocked = false;

    //TODO change to use with animator
    public Button _button;
    public GameObject _cancleButton;
    public Animator _animator;
    public Text _moveNameLabel;
    public Image _image;
    public CategoryEffect _categoryEffect;

    public void SetUp(MoveCategoryDelegate categoryDelegate, IBodyPart bodyPart, Sprite sprite){
        _delegate = categoryDelegate;
        _bodyPart = bodyPart;
        _image.sprite = sprite;
        _categoryEffect.Show(_bodyPart.GetModifier() == BodyPartModifier.Cum);
    }
    public void WasPressed(){
        if(_isLocked){
            return;
        }
        _delegate.CategoryPressed(_bodyPart);
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
        _moveNameLabel.text = move.GetName();
    }
    public void PressedCancel(){
        _delegate.CancelMove(_bodyPart);
    }

    public void MouseEnter(){
        if(_isLocked){
            _delegate.ShowTargets(_bodyPart, _selectedMove);
            return;
        }
    }
    public void MouseExit(){
        _delegate.HideTargets();
    }
    public void SetAvailable(bool isAvailable){
        gameObject.SetActive(isAvailable);
    }
    public MoveType GetMoveType(){
        return _bodyPart.GetMoveType();
    }
    public IBodyPart GetBodyPart(){
        return _bodyPart;
    }
    public void ClimaxOn(){
        //TODO pass in cum data
        //TODO make it look different
        _bodyPart.SetModifier(BodyPartModifier.Cum);
        _categoryEffect.Show(true);
    }
    public void UpdateCategory(){
        //check if it should show the modifier
        _categoryEffect.Show(_bodyPart.GetModifier() == BodyPartModifier.Cum);
    }

}
public interface MoveCategoryDelegate {
    void CategoryPressed(IBodyPart bodyPart);
    void CancelMove(IBodyPart bodyPart);
    void ShowTargets(IBodyPart bodyPart, Move selectedMove);
    void HideTargets();
}
