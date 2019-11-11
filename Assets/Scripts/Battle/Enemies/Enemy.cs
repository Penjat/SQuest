using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy {

    public EnemyDelegate _delegate;
    public UIParticleSystem _explodeEffect;

    public void SetDelegate(EnemyDelegate enemyDelegate){
        _delegate = enemyDelegate;
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
        Destroy(gameObject,2.0f);
        _delegate.RemoveEnemy(this);
        _explodeEffect.Play();
    }
}

public interface EnemyDelegate {
    void EnemyPressed(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
}
