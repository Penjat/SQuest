using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public class Enemy : MonoBehaviour, IEnemy, StatusBarDelegate, ICardDelegate {

    protected EnemyDelegate _delegate;
    protected ICard _card;

    protected int _dmgToDo;

    protected float _curClimax = 0.0f;
    protected float _maxClimax = 8.0f;

    protected float _curArousal = 0.0f;
    protected float _maxArousal = 5.0f;

    protected SelectState _state;

    protected bool _isDead = false;
    protected IDictionary<Move,float> _usedMoves = new Dictionary<Move,float>();
    protected HashSet<Move> _targetedBy = new HashSet<Move>();
    protected HashSet<BodyTarget> _bodyTargets = new HashSet<BodyTarget>();

    public void SetDelay(double delay){
        _card.SetDelay((float)delay);
    }

    public void ClearSelf(){
        Destroy(gameObject);
    }

    public void Destroy(){
        Destroy(gameObject,8.0f);
        _delegate.RemoveEnemy(this);
        _card.Climax();
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
        _card.SetClimax(_curClimax, true);
        _card.ShowDmg((int)dmg);
    }
    public void CheckClimax(){
        if(_curClimax >= _maxClimax){
            Destroy();
        }
    }
    public void TargetWithMove(Move move){
        SetState(SelectState.Targeted);
        ShowPartsFlashing(move);
    }
    private void ShowPartsFlashing(Move move){
        foreach(TargetType targetType in move.GetPartsTargeted()){
            BodyTarget bodyTarget = _bodyTargets.First(x => x.GetTargetType() == targetType);
            bodyTarget.StartFlashing();
        }
    }
    public void SetState(SelectState state){
        if(_isDead){
            return;
        }
        _state = state;
        _card.SetState(state);
    }

    public IEnumerator WaitFor(float seconds,Action func){
        yield return new WaitForSeconds(seconds);
        func();
    }

    protected void DoneTurn(){
        Debug.Log("End Turn");
        _usedMoves.Clear();
        _targetedBy.Clear();
        ResetBodyTargets();
        _delegate.EnemyDoneTurn();

    }
    public void UseMove(Move move, float result){
        //after the mini game, add the move so its effects can be relized later
        _usedMoves.Add(move,result);
    }
    //---------Virtual METHODS
    public virtual string GetName(){
        return "GENERIC";
    }
    public virtual void TakeTurn(){
        Debug.Log("taking my turn");
        //Dont attack if was targeted last turn
        if(_usedMoves.Count > 0){
            DoneTurn();
            return;
        }
        _delegate.EnemyMsg("The " + GetName().ColorFor(Entity.ENEMY) + " is mad you ignored it...");
        //TODO make an attack method

        Action attack = Attack;
        StartCoroutine(WaitFor(1.0f, attack) );
    }
    protected virtual void Attack(){
        _delegate.AttackPlayer(2);
        _delegate.EnemyMsg("The " + "Imp".ColorFor(Entity.ENEMY) + " slaps you");
        _card.Attack();
        StartCoroutine(WaitFor(2.0f, DoneTurn) );
    }
    public virtual void SetUp(EnemyDelegate enemyDelegate){
        _delegate = enemyDelegate;
        _card = GetComponentInChildren<ICard>();
        _card.SetCardDelegate(this);
        _card.SetName(GetName());
        _card.SetUpBodyTargets(_bodyTargets);

        //set up the climax bar
        _card.SetUpClimax(this,_maxClimax);
        _card.SetClimax(_curClimax);

        //set up the arousal bar
        _card.SetUpArousal(this,_maxArousal);
        _card.SetArousal(_curArousal);
        _state = SelectState.Norm;
    }
    //--------------ICardDelegate------------------
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
        StopFlashingParts();
        _delegate.PointerExit();
    }
    public void StopFlashingParts(){
        foreach(BodyTarget bodyTarget in _bodyTargets){
            bodyTarget.StopFlashing();
        }
    }
    public void WasPressed(){
        _delegate.EnemyPressed(this);
    }
    public void TargetWith(Move move){
        //When the player commits to targeting enemy
        _targetedBy.Add(move);

        //cycle through the parts targeted
        foreach(TargetType targetType in move.GetPartsTargeted()){
            //Find the first BodyTarget of this type that is available
            BodyTarget bodyTarget = _bodyTargets.First(x => x.IsAvailable() && x.GetTargetType() == targetType);
            bodyTarget.SetIsAvailble(false);
        }

    }
    public void UnTarget(Move move){
        _targetedBy.Remove(move);
        //cycle through the parts targeted
        foreach(TargetType targetType in move.GetPartsTargeted()){
            //Find the first BodyTarget of this type that is available
            BodyTarget bodyTarget = _bodyTargets.First(x => !x.IsAvailable() && x.GetTargetType() == targetType);
            bodyTarget.SetIsAvailble(true);
        }
    }
    public void ClearTargets(){
        _targetedBy.Clear();
    }
    private void ResetBodyTargets(){
        foreach(BodyTarget bodyTarget in _bodyTargets){
            bodyTarget.SetIsAvailble(true);
        }
    }
    public bool CanTarget(Move move){
        if(move == null ){
            return false;
        };
        //check if this enemy has the targets available for this move
        HashSet<TargetType> moveTargets = move.GetPartsTargeted();

        //get a HashSet of all the availableTargets
        HashSet<TargetType> availableTargets = new HashSet<TargetType>(_bodyTargets.Where(x => x.IsAvailable()).Select(x => x.GetTargetType()));


        foreach(TargetType targetType in moveTargets){
            if(!availableTargets.Contains(targetType)){
                return false;
            }
        }
        return true;
    }

    //--------------StatusBarDelegate---------------
    public void DoneFilling(){
        CheckClimax();
    }
    public void SetTargeted(bool b){
        _card.SetTargeted(b);
    }
}

public interface EnemyDelegate {
    void EnemyPressed(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
    void PointerOver(IEnemy enemy, bool b);
    void PointerExit();
    void EnemyDoneTurn();
    void AttackPlayer(int dmg);
    void EnemyMsg(string msg);
}
