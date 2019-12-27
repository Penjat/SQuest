using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public class Enemy : MonoBehaviour, IEnemy, StatusBarDelegate, ICardDelegate {

    protected EnemyDelegate _delegate;
    protected ICard _card;

    // protected Dmg _dmgToDo;

    protected float _curClimax = 0.0f;
    protected float _maxClimax = 8.0f;

    protected float _curArousal = 0.0f;
    protected float _maxArousal = 5.0f;

    protected SelectState _state;

    protected bool _isDead = false;
    protected IDictionary<Move,float> _usedMoves = new Dictionary<Move,float>();
    protected IDictionary<Move,BodyTarget[]> _targetedBy = new Dictionary<Move,BodyTarget[]>();
    protected HashSet<BodyTarget> _bodyTargets = new HashSet<BodyTarget>();

    protected List<Dmg> _dmgToDo = new List<Dmg>();

    protected int waitingForBars = 0;

    public void SetDelay(double delay){
        _card.SetDelay((float)delay);
    }

    public void ClearSelf(){
        Destroy(gameObject);
    }

    public void Destroy(){
        Destroy(gameObject,8.0f);
        _delegate.RemoveEnemy(this);
        _delegate.Climax(this);
        _card.Climax();
        StartCoroutine(WaitFor(2.0f,DoneResolving));
        _isDead = true;
    }
    public void AddToDmg(Dmg dmg){
        _dmgToDo.Add(dmg);
    }
    private void DoneResolving(){
        _delegate.DoneResolving(this);
    }
    public void ResolveDMG(){
        //keep track of which bars are still waiting for
        waitingForBars = 2;
        //adds all the dmg done that round together
        if(_dmgToDo.Count == 0){
            _delegate.DoneResolving(this);
            return;
        }
        foreach(Dmg dmg in _dmgToDo){
            DoDmg(dmg);
        }
    }
    protected virtual Dmg CheckForFetish(Dmg dmg){
        //default to the regualar dmg
        Dmg modDmg = dmg;
        //checks the dmg to see if it matches fetish
        if(dmg.CheckForFetish(Fetish.Cum)){
            modDmg = dmg.TimesBy(2.0f);
        }
        return modDmg;
    }
    public void DoDmg(Dmg dmg){
        Debug.Log("doing dmg --------------------------");
        //TODO check fetishes and modify Dmg
        Dmg modDmg = CheckForFetish(dmg);
        _curClimax += modDmg.GetClimax();
        _curArousal += modDmg.GetArousal();
        _card.SetClimax(_curClimax, true);
        _card.SetArousal(_curArousal, true);
        if(dmg.GetClimax() != 0){
            _card.ShowDmg(modDmg.GetClimax(), DmgType.Climax);
        }
        if(dmg.GetArousal() != 0){
            _card.ShowDmg(modDmg.GetArousal(), DmgType.Arousal);
        }
    }
    public bool CheckClimax(){
        return (_curClimax >= _maxClimax);
    }
    public void TargetWithMove(Move move){
        SetState(SelectState.Targeted);
        ShowPartsFlashing(move);
    }
    private void ShowPartsFlashing(Move move){

        //create a new list so parts can be removed and not targeted twice
        List<BodyTarget> targets = new List<BodyTarget>(_bodyTargets);
        foreach(TargetType targetType in move.GetPartsTargeted()){
            BodyTarget bodyTarget = targets.First(x => x.GetTargetType() == targetType && x.IsAvailable());
            targets.Remove(bodyTarget);
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
        _dmgToDo.Clear();
        _delegate.EnemyDoneTurn();
    }
    public virtual MoveType GetMoveTypeClimax(){
        //returns the body part the enemy will climax on
        //return null if was not targeted
        //TODO fix this
        if(_targetedBy.Count == 0 ){
            return MoveType.Hand;
        }
        //Get a random move
        int rand = UnityEngine.Random.Range(0,_targetedBy.Count);
        return _targetedBy.ElementAt(rand).Key.GetPrimaryType();
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
        ResetBodyTargets();
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

        List<BodyTarget> partsTargeted = new List<BodyTarget>();

        //cycle through the parts targeted
        foreach(TargetType targetType in move.GetPartsTargeted()){
            //Find the first BodyTarget of this type that is available
            BodyTarget bodyTarget = _bodyTargets.First(x => x.IsAvailable() && x.GetTargetType() == targetType);
            bodyTarget.SetIsAvailble(false);
            partsTargeted.Add(bodyTarget);
        }
        _targetedBy.Add(move,partsTargeted.ToArray());
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
            bodyTarget.StopFlashing();
        }
    }
    public TargetResult CanTarget(Move move){
        if(move == null ){
            return TargetResult.NotMatch;
        };
        //check if this enemy has the targets available for this move
        List<TargetType> moveTargets = move.GetPartsTargeted();

        //get a hashset of all the targets regaurdless of if they are available
        HashSet<TargetType> allTargets = new HashSet<TargetType>(_bodyTargets.Select(x => x.GetTargetType()));
        //get a HashSet of all the availableTargets
        List<TargetType> availableTargets = new List<TargetType>(_bodyTargets.Where(x => x.IsAvailable()).Select(x => x.GetTargetType()));

        //TODO refactor to work with multiple of same targetType
        foreach(TargetType targetType in moveTargets){

            //if the enemy doesn't have this part,
            if(!allTargets.Contains(targetType)){
                //return not match
                return TargetResult.NotMatch;
            }
            //if the enemy DOES have this part available,
            if(availableTargets.Contains(targetType)){
                //remove it from the available list so it isn't counted twice
                availableTargets.Remove(targetType);
            }else{
                //if not, it must be already targeted
                return TargetResult.AlreadyTargeted;
            }
        }
        //if we have not returned yet, the move is a match
        return TargetResult.Available;
    }

    //--------------StatusBarDelegate---------------
    public void DoneFilling(int refNumber){
        if(refNumber == 1 && CheckClimax()){
            Destroy();
            return;
        }
        waitingForBars--;
        if(waitingForBars == 0){
            DoneResolving();
        }
    }
    public void SetTargeted(bool b, Move selectedMove=null){
        _card.SetTargeted(b);
        if(b){
            StopFlashingParts();
            return;
        }
        //just in case
        if(selectedMove == null){
            return;
        }
        BodyTarget[] bodyTargets = _targetedBy[selectedMove];
        foreach(BodyTarget bodyTarget in bodyTargets){
            bodyTarget.StartFlashing();
        }
    }
}

public enum TargetResult{
    Available, AlreadyTargeted, NotMatch
};

public interface EnemyDelegate {
    void EnemyPressed(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
    void PointerOver(IEnemy enemy, bool b);
    void PointerExit();
    void EnemyDoneTurn();
    void AttackPlayer(int dmg);
    void EnemyMsg(string msg);
    void Climax(IEnemy enemy);
    void DoneResolving(IEnemy enemy);
}
