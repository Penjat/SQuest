using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, EnemyDelegate {

    private EnemyManagerDelegate _delegate;
    public IEnemyFactory _enemyFactory;
    private List<IEnemy> _enemies;

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
    private void CurEnemyTurn(){
        //check if enemies left to take turns
        if(_curEnemyIndex >= _enemies.Count){
            _delegate.EndEnemyTurn();
            return;
        }
        IEnemy enemy = _enemies[_curEnemyIndex];
        enemy.TakeTurn();
    }

    public void NextEnemyTurn(){
        _curEnemyIndex++;
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
        // locationButton.SetUp(this, location);

        //find the button's position
        float buttonSize = 5.0f;
        float buttonWidth = 64.0f*buttonSize;
        float buttonHeight = 89.0f*buttonSize;
        float padding = 40.0f;
        float dir = 1;
        float count = Mathf.Floor(_enemies.Count/2);
        if ((_enemies.Count & 1) == 0) {
            dir = count*-1;
        }else{
            dir = count+1;
        }

        float x1 = dir*(buttonWidth + padding);
        float y1 = -buttonHeight;
        float x2 = x1 + buttonWidth;
        float y2 = y1 + buttonHeight;

        //set the position
        RectTransform rectTransform = g.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x1,0.0f);
        rectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);

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
        _delegate.EnemyPressed(enemy);
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
    public void SetTargeted(IEnemy[] targetedEnemies){
        //check all the enemies to see if they should be targeted
        //TODO fix how bad this is maybe, maybe doen't matter

        //untarget all
        ClearTargets();

        //target the targeted
        foreach(IEnemy enemy in targetedEnemies){
            enemy.SetTargeted(true);
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
    public void ResolveDMG(){
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
    }
}

public interface EnemyManagerDelegate {
    void EnemyPressed(IEnemy enemy);
    void DoneBattle();
    void OverEnemy(IEnemy enemy);
    void ExitEnemy();
    void DmgPlayer(int dmg);
    void EndEnemyTurn();
}
