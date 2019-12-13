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
            _animator.Play("BodyPartFlashing");
            break;
            case BodyTargetState.Targeted:
            _animator.Play("BodyPartTargeted");
            break;
        }
    }
}
