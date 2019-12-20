using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMSG : MonoBehaviour {

    public Animator _animator;

    public void GoTOGame(){
        //TODO animate out
        _animator.SetTrigger("pressed_ok");
        Destroy(gameObject,3.0f);
    }
    public void ExitGame(){
        //TODO close the app
    }
}
