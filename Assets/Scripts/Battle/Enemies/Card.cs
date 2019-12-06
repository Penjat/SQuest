﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectState{
Norm, Over, Targeted
};

//The card is the UI interface for each enemy
//the logic for the enemy is controlled by its class
public class Card : MonoBehaviour, ICard {

    private ICardDelegate _delegate;

    private const float FILL_RATE = 2.0f;

    public UIParticleSystem _explodeEffect;
    public Text _nameLabel;
    public StatusBar _climaxBar;
    public Color _overColor;
    public Color _normColor;
    public Animator _animator;
    public Image _button;
    public DMGLabel _dmgLabel;

    void Start(){
        _button.color = _normColor;
    }

    public void SetName(string name){
        _nameLabel.text = name;
    }
    public void SetUpClimax(StatusBarDelegate statusBarDelegate, float max){
        _climaxBar.SetUp(statusBarDelegate, max, FILL_RATE);
    }
    public void SetClimax(float climax, bool animated=false){
        _climaxBar.SetValue(climax);
    }
    public void Climax(){
        _explodeEffect.Play();
        _animator.Play("Climax");
    }
    public void ShowDmg(int dmg){
        _dmgLabel.ShowDmg(dmg);
    }
    public void SetTargeted(bool b){
        if(b){
            Debug.Log("should play targeted");
            _animator.Play("Targeted");
            return;
        }
        _animator.Play("Idle");
    }
    public void SetState(SelectState state){
        switch(state){
            case SelectState.Norm:
            _button.color = _normColor;
            SetTargeted(false);
            break;
            case SelectState.Targeted:
            _button.color = _normColor;
            SetTargeted(true);
            break;

            case SelectState.Over:
            _button.color = _overColor;
            SetTargeted(false);
            break;
        }
    }
    public void SetDelay(float delay){
        StartCoroutine(AppearIn(delay));
    }
    private IEnumerator AppearIn(float delay){
        _animator.speed = 0.0f;
        yield return new WaitForSeconds(delay);
        _animator.speed = 1.0f;
    }
    public void Attack(){
        _animator.Play("Attack");
    }
    public void SetCardDelegate(ICardDelegate cardDelegate){
        _delegate = cardDelegate;
    }

    //---------------Input Methods----------------------
    public void MouseEnter(){
        _delegate.MouseEnter();
    }
    public void MouseExit(){
        _delegate.MouseExit();
    }
    public void MousePress(){
        _delegate.WasPressed();
    }
}

public interface ICard {
    void Climax();
    void SetName(string name);
    void SetUpClimax(StatusBarDelegate statusBarDelegate, float max);
    void SetClimax(float climax, bool animated=false);
    void SetState(SelectState selectState);
    void Attack();
    void ShowDmg(int dmg);
    void SetCardDelegate(ICardDelegate cardDelegate);
    void SetTargeted(bool b);
    void SetDelay(float delay);
}
public interface ICardDelegate{
    void MouseEnter();
    void MouseExit();
    void WasPressed();
}
