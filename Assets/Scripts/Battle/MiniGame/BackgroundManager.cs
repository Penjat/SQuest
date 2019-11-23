using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour{

    public Animator _animator;
    public CanvasGroup _canvasGroup;

    private bool _isFading;
    private float _fadeRate = 0.5f;

    void Update(){
        if(_isFading){
            _canvasGroup.alpha -= Time.deltaTime*_fadeRate;
            if(_canvasGroup.alpha <= 0.0f){
                _isFading = false;
            }
        }
    }

    public void ShowImage(MoveType moveType){
        //TODO pass in more info
        Debug.Log("movetype = " + moveType);

        switch(moveType){
            case MoveType.Ass:
            _animator.Play("anal");
            break;
            case MoveType.Mouth:
            _animator.Play("bj");
            break;
            case MoveType.Hand:
            _animator.Play("hj");
            break;
            case MoveType.Breasts:
            _animator.Play("tits");
            break;
        }
        StartFade();
    }
    private void StartFade(){
        _isFading= true;
        _canvasGroup.alpha = 0.35f;
    }
}
