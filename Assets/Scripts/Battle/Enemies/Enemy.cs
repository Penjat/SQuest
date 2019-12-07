using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, StatusBarDelegate, ICardDelegate {

    protected EnemyDelegate _delegate;
    protected ICard _card;

    private int _dmgToDo;

    protected float _curClimax = 0.0f;
    protected float _maxClimax = 8.0f;

    protected float _curArousal = 0.0f;
    protected float _maxArousal = 5.0f;

    private SelectState _state;

    private bool _isDead = false;
    private IDictionary<Move,float> _usedMoves = new Dictionary<Move,float>();
    private HashSet<Move> _targetedBy = new HashSet<Move>();

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

    public void SetState(SelectState state){
        if(_isDead){
            return;
        }
        _state = state;
        _card.SetState(state);
    }

    public void TakeTurn(){
        Debug.Log("taking my turn");
        //Dont attack if was targeted last turn
        if(_usedMoves.Count > 0){
            DoneTurn();
            return;
        }
        _delegate.AttackPlayer(2);
        _card.Attack();
        StartCoroutine(WaitFor(2.0f));
    }
    public IEnumerator WaitFor(float seconds){
        yield return new WaitForSeconds(seconds);
        DoneTurn();
    }
    private void DoneTurn(){
        _usedMoves.Clear();
        _delegate.EnemyDoneTurn();
    }
    public void UseMove(Move move, float result){
        _usedMoves.Add(move,result);
    }
    //---------Virtual METHODS
    public virtual string GetName(){
        return "GENERIC";
    }
    public virtual void SetUp(EnemyDelegate enemyDelegate){
        _delegate = enemyDelegate;
        _card = GetComponentInChildren<ICard>();
        _card.SetCardDelegate(this);
        _card.SetName(GetName());

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
        _delegate.PointerExit();
    }
    public void WasPressed(){
        _delegate.EnemyPressed(this);
    }
    public void TargetWith(Move move){
        _targetedBy.Add(move);
    }
    public void UnTarget(Move move){
        _targetedBy.Remove(move);
    }
    public void ClearTargets(){
        _targetedBy.Clear();
    }
    public HashSet<Move> GetTargets(){
        return _targetedBy;
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
}
