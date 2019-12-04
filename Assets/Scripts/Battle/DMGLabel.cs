using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMGLabel : MonoBehaviour {
    public Text _dmgLabel;
    public Animator _animator;

    public void ShowDmg(float f){
        _dmgLabel.text = f.ToString();
        _animator.SetTrigger("Show");
    }
}
