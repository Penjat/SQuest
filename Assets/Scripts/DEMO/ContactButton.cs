using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactButton : MonoBehaviour {

    public Animator _animator;
    public Text _buttonLabel;
    public Image _iconImage;

    public Color _overColor;
    public Color _normColor;

    void Start(){
        _buttonLabel.color = _normColor;
        _iconImage.color = _normColor;
    }

    public void MouseOver(){
        _animator.Play("over_button");
        _buttonLabel.color = Color.white;
        _iconImage.color = _overColor;
    }
    public void MouseExit(){
        _animator.Play("norm");
        _buttonLabel.color = _normColor;
        _iconImage.color = _normColor;
    }
    public void MousePress(){

    }

}
