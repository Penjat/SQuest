using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, EnemyDelegate {

    private EnemyManagerDelegate _delegate;
    public IEnemyFactory _enemyFactory;

    private List<IEnemy> _enemies;
    private List<IEnemy> _waitingFor;

    public RectTransform _enemyContainer;

    private const double DELAY_TIME = 0.5;
    private int _curEnemyIndex = 0;

    public void SetUp(EnemyManagerDelegate enemyManagerDelegate){
        _delegate = enemyManagerDelegate;
        _enemyFactory = GetComponent<IEnemyFactory>();
    }
    public void StartBattle(Battle battle){
        ClearEnemies();
        CreateEnemies(battle);
    }
    private void CreateEnemies(Battle battle){
        foreach(string enemyName in battle.GetEnemies()){
            CreateEnemy(enemyName);
        }
    }
    public void TakeTurn(){
        //start with the first enemy
        _curEnemyIndex = 0;
        CurEnemyTurn();
    }
    public void CurEnemyTurn(){
        Debug.Log("cur enemy turn.");
        //check if enemies left to take turns
        if(_curEnemyIndex >= _enemies.Count){
            Debug.Log("No more enemies.");
            _delegate.EndEnemyTurn();
            return;
        }
        IEnemy enemy = _enemies[_curEnemyIndex];
        enemy.TakeTurn();
    }
    public void NextEnemyTurn(){
        Debug.Log("next enemy turn");
        _curEnemyIndex++;
        _delegate.ShowMsg("");
        CurEnemyTurn();
    }

    public void ClearEnemies(){
        if(_enemies != null){
            foreach(IEnemy enemy in _enemies){
                enemy.ClearSelf();
            }
            _enemies.Clear();
        }
        _enemies = new List<IEnemy>();
    }

    public void CreateEnemy(string enemyName){
        GameObject prefab = _enemyFactory.GetPrefab(enemyName);
        GameObject g = Instantiate(prefab);
        g.transform.SetParent(_enemyContainer.transform);

        //set button's text
        //TODO store in an array
        IEnemy enemy = g.GetComponent(typeof(IEnemy)) as IEnemy;

        enemy.SetUp(this);
        enemy.SetDelay(_enemies.Count*DELAY_TIME);
        _enemies.Add(enemy);
    }
    public bool CheckWin(){
        return (_enemies.Count == 0);
    }

    //---------------Enemy Delegate Methods--------------
    public void EnemyDoneTurn(){
        NextEnemyTurn();
    }
    public void EnemyPressed(IEnemy enemy){
        Debug.Log("an enemy was pressed.");
        _delegate.TargetPressed(enemy as ITarget);
    }
    public void RemoveEnemy(IEnemy enemy){
        _enemies.Remove(enemy);
        if(CheckWin()){
            _delegate.DoneBattle();
        }
    }
    public void PointerOver(IEnemy enemy, bool b){
        _delegate.OverEnemy(enemy);
    }
    public void PointerExit(){
        _delegate.ExitEnemy();
    }
    public void SetTargeted(ITarget[] targets, Move selectedMove){
        //check all the enemies to see if they should be targeted
        //TODO fix how bad this is maybe, maybe doen't matter

        //untarget all
        ClearTargets();

        //target the targeted
        foreach(ITarget target in targets){
            target.SetTargeted(true, selectedMove);
        }
    }
    public void ClearTargets(){
        //untarget all
        foreach(IEnemy enemy in _enemies){
            enemy.SetTargeted(false);
        }
    }
    public void AreaAffect(){
        //TODO pass in move
        foreach(IEnemy enemy in _enemies){
            enemy.SetState(SelectState.Targeted);
        }
    }
    public IEnemy[] GetEnemiesAsArray(){
        return _enemies.ToArray();
    }
    public List<IEnemy> GetEnemies(){
        return _enemies;
    }
    public void ResolveDMG(){
        //create a list of enemies we are waiting to finish resolving
        _waitingFor = new List<IEnemy>(_enemies);
        foreach(IEnemy enemy in _enemies){
            enemy.ResolveDMG();
        }
    }
    public void AttackPlayer(int dmg){
        _delegate.DmgPlayer(dmg);
    }
    public void EnemyMsg(string msg){
        //the enemy wants to display a msg
        Debug.Log("the enemy wants to send a msg: " + msg);
        _delegate.ShowMsg(msg);
    }
    public void Climax(IEnemy enemy){
        _delegate.EnemyClimax(enemy);
    }
    public void DoneResolving(IEnemy enemy){
        _waitingFor.Remove(enemy);
        if(_waitingFor.Count == 0){
            _delegate.DoneResolvingEnemies();
        }
    }
}

public interface EnemyManagerDelegate {
    void TargetPressed(ITarget enemy);
    void DoneBattle();
    void OverEnemy(IEnemy enemy);
    void ExitEnemy();
    void DmgPlayer(int dmg);
    void EndEnemyTurn();
    void ShowMsg(string msg);
    void EnemyClimax(IEnemy enemy);
    void DoneResolvingEnemies();
}
