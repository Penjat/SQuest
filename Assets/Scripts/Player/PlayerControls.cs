using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public Animator _animator;
    public void ShowMenu(bool b){
        _animator.SetBool("isOpen",b);
    }
}
