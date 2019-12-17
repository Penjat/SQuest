using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMGLabel : MonoBehaviour {
    public Text _dmgLabel;
    public Animator _animator;

    public void ShowDmg(DmgWithType dmgWithType){
        //TODO toggle for type
        switch(dmgWithType._dmgType){
            case DmgType.Climax:
            _dmgLabel.color = Color.red;
            break;
            case DmgType.Arousal:
            _dmgLabel.color = Color.blue;
            break;
        }
        _dmgLabel.text = dmgWithType._dmg.ToString();
        //_dmgLabel.color = Color.red;
        _animator.SetTrigger("Show");
    }
}
public enum DmgType{
    Climax, Arousal
};
