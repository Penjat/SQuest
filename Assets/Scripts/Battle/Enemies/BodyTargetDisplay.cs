using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTargetDisplay : MonoBehaviour, IBodyTargetDisplay {
    public Animator _animator;
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
