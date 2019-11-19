﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour {
    public RectTransform _rect;
    private float _size = 60.0f;
    private float _goal;

    private bool _wasCleared = false;
    private float _accuracy;
    public UIParticleSystem _explode;
    public Animator _animator;

    public bool CheckMissed(float pos){
        //check if has moved too far
        return (_goal + 40.0f + pos < 0.0f);//adding half the size of the button
    }

    public void SetPosition(float pos){
        _rect.sizeDelta = new Vector2(_size,_size);
        _goal = pos;
        _rect.anchoredPosition = new Vector2(0.0f,pos);
    }
    public void Clear(float trackPos){
        _wasCleared = true;
        //gameObject.SetActive(false);
        _accuracy = _goal + trackPos;
        _explode.Play();
        _animator.Play("gem_clear");
    }
    public bool InRange(float trackPos){
        //checks if the gem is in range to be cleared
        //assuming a range of 40 for button size
        return (_goal + trackPos < 40.0f + _rect.rect.width/2);

    }
}
