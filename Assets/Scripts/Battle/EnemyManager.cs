using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, EnemyDelegate {

    private EnemyManagerDelegate _delegate;
    private List<IEnemy> _enemies;

    //TODO move this to Enemy Factory
    public GameObject _impPrefab;
    public RectTransform _enemyContainer;

    private const double DELAY_TIME = 0.5;

    public void SetUp(EnemyManagerDelegate enemyManagerDelegate){
        _delegate = enemyManagerDelegate;
    }
    public void StartBattle(Battle battle){
        ClearEnemies();

        //TODO create based on battle
        CreateEnemy();
        CreateEnemy();
        CreateEnemy();
    }
    public void TakeTurn(){
        //cycle through the enemies and see how they should act
        foreach(IEnemy enemy in _enemies){
            Debug.Log("taking my turn");
            _delegate.DmgPlayer(2);
        }
        _delegate.EndEnemyTurn();
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

    public void CreateEnemy(){
        GameObject g = Instantiate(_impPrefab);
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
}

public interface EnemyManagerDelegate {
    void EnemyPressed(IEnemy enemy);
    void DoneBattle();
    void OverEnemy(IEnemy enemy);
    void ExitEnemy();
    void DmgPlayer(int dmg);
    void EndEnemyTurn();
}
