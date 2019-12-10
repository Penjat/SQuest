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
        // _animator.SetBool("Hover",true);
        _buttonLabel.color = Color.white;
        _iconImage.color = _overColor;
    }
    public void MouseExit(){
        // _animator.SetBool("Hover",false);
        _buttonLabel.color = _normColor;
        _iconImage.color = _normColor;
    }
    public void MousePress(){

    }

}
