using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTargetDisplay : MonoBehaviour, IBodyTargetDisplay {
    public Animator _animator;
    public Image _image;
    public Sprite[] _images;

    public void SetUp(TargetType targetType){
        switch(targetType){
            case TargetType.Penis:
            _image.sprite = _images[0];
            break;

            case TargetType.Vagina:
            _image.sprite = _images[1];
            break;
        }
    }
    public void SetState(BodyTargetState state){
        switch(state){
            case BodyTargetState.Available:
            _animator.Play("BodyPartNorm");
            break;
            case BodyTargetState.Flashing:
            Debug.Log("flashing");
            _animator.Play("BodyPartFlashing");
            break;
            case BodyTargetState.Used:
            _animator.Play("BodyPartUsed");
            break;
        }
    }
}
