using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectState{
Norm, Over, Targeted
};

public class Enemy : MonoBehaviour, IEnemy, StatusBarDelegate {

    public EnemyDelegate _delegate;
    public UIParticleSystem _explodeEffect;
    public Text _nameLabel;
    public StatusBar _climaxBar;
    public Animator _animator;

    public Image _button;
    public DMGLabel _dmgLabel;
    private int _dmgToDo;

    private float _curClimax = 0.0f;
    private float _maxClimax = 8.0f;

    private SelectState _state;
    public Color _overColor;
    public Color _normColor;

    private bool _willAppear;
    private double _delayTime = 0.0;
    private bool _isDead = false;
    private bool _wasTargeted;

    void Update(){
        if(_willAppear){
            _delayTime -= Time.deltaTime;
            if(_delayTime <= 0){
                _willAppear = false;
                _animator.speed = 1.0f;
            }
        }
    }
    public void SetDelay(double delay){
        _delayTime = delay;
        _willAppear = true;
        _animator.speed = 0.0f;
        //TODO maybe trigger animation
    }
    public void SetUp(EnemyDelegate enemyDelegate){
        _delegate = enemyDelegate;
        _nameLabel.text = GetName();
        _climaxBar.SetUp(this,_maxClimax,2.0f);
        _climaxBar.SetValue(0.0f);
        _state = SelectState.Norm;
        _button.color = _normColor;
    }
    public void WasPressed(){
        _delegate.EnemyPressed(this);
    }
    public void ClearSelf(){
        Destroy(gameObject);
    }
    public virtual string GetName(){
        return "GENERIC";
    }
    public void Destroy(){
        Destroy(gameObject,8.0f);
        _delegate.RemoveEnemy(this);
        _explodeEffect.Play();
        _animator.Play("Climax");
        _isDead = true;
    }
    public void AddToDmg(int dmg){
        _dmgToDo += dmg;
    }
    public void ResolveDMG(){
        //adds all the dmg done that round together
        if(_dmgToDo == 0){
            return;
        }
        DoDmg(_dmgToDo);
        _dmgToDo = 0;
    }
    public void DoDmg(float dmg){
        Debug.Log("doing dmg --------------------------");
        _curClimax += dmg;
        _climaxBar.SetValueAnimated(_curClimax);
        _dmgLabel.ShowDmg(dmg);
    }
    public void CheckClimax(){
        if(_curClimax >= _maxClimax){
            Destroy();
        }
    }
    public void MouseEnter(){
        if(_isDead){
            return;
        }
        SetState(SelectState.Over);
        _delegate.PointerOver(this,true);
    }
    public void MouseExit(){
        if(_isDead){
            return;
        }
        SetState(SelectState.Norm);
        _delegate.PointerExit();
    }
    public void SetState(SelectState state){
        if(_isDead){
            return;
        }
        _state = state;
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
    public void SetTargeted(bool b){
        if(b){
            _animator.Play("Targeted");
            return;
        }
        _animator.Play("Idle");
    }
    public void TakeTurn(){
        Debug.Log("taking my turn");
        //Dont attack if was targeted last turn
        if(_wasTargeted){
            DoneTurn();
            return;
        }
        _delegate.AttackPlayer(2);
        _animator.SetTrigger("Attack");
        StartCoroutine(WaitFor(2.0f));
    }
    public IEnumerator WaitFor(float seconds){
        yield return new WaitForSeconds(seconds);
        DoneTurn();
    }
    private void DoneTurn(){
        _wasTargeted = false;
        _delegate.EnemyDoneTurn();
    }
    public void WasTargeted(){
        _wasTargeted = true;
    }
    //--------------StatusBarDelegate---------------
    public void DoneFilling(){
        CheckClimax();
    }
}

public interface EnemyDelegate {
    void EnemyPressed(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
    void PointerOver(IEnemy enemy, bool b);
    void PointerExit();
    void EnemyDoneTurn();
    void AttackPlayer(int dmg);
}
