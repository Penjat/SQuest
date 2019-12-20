using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemScore : MonoBehaviour{
    public Text _scoreLabel;
    public void ShowScore(float score){
        gameObject.SetActive(true);
        if(score >= 99.5f){
            _scoreLabel.text = "PERFECT";
            return;
        }
        _scoreLabel.text = score.ToString() + "%";
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
