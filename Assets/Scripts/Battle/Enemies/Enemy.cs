using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IEnemy, StatusBarDelegate {

    public EnemyDelegate _delegate;
    public UIParticleSystem _explodeEffect;
    public Text _nameLabel;
    public StatusBar _climaxBar;
    public Animator _animator;

    private float _curClimax = 0.0f;
    private float _maxClimax = 8.0f;

    public void SetUp(EnemyDelegate enemyDelegate){
        _delegate = enemyDelegate;
        _nameLabel.text = GetName();
        _climaxBar.SetUp(this,_maxClimax,2.0f);
        _climaxBar.SetValue(0.0f);
    }
    public void WasPressed(){
        _delegate.EnemyPressed(this);
    }
    public void ClearSelf(){
        Destroy(gameObject);
    }
    public string GetName(){
        return "GENERIC";
    }
    public void Destroy(){
        Destroy(gameObject,8.0f);
        _delegate.RemoveEnemy(this);
        _explodeEffect.Play();
        _animator.Play("Climax");
    }
    public void DoDmg(float dmg){
        Debug.Log("doing dmg --------------------------");
        _curClimax += dmg;
        _climaxBar.SetValueAnimated(_curClimax);
    }
    public void CheckClimax(){
        if(_curClimax >= _maxClimax){
            Destroy();
        }
    }
    //--------------StatusBarDelegate---------------
    public void DoneFilling(){
        CheckClimax();
    }
}

public interface EnemyDelegate {
    void EnemyPressed(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
}
