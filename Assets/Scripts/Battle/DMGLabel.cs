using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMGLabel : MonoBehaviour {
    public Text _dmgLabel;
    public Animator _animator;

    public void ShowDmg(int i){
        _dmgLabel.text = i.ToString();
        _animator.SetTrigger("Show");
    }
}
