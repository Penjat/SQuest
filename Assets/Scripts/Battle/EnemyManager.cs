using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, EnemyDelegate {

    private EnemyManagerDelegate _delegate;
    private List<IEnemy> _enemies;

    //TODO move this to Enemy Factory
    public GameObject _impPrefab;
    public RectTransform _enemyContainer;

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
        float buttonWidth = 200.0f;
        float buttonHeight = 200.0f;

        float x1 = + _enemies.Count*buttonWidth + 0.0f;
        float y1 = -buttonHeight;
        float x2 = x1 + buttonWidth;
        float y2 = y1 + buttonHeight;

        //set the position
        RectTransform rectTransform = g.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(x1,y1);
        rectTransform.offsetMax = new Vector2(x2,y2);

        enemy.SetDelegate(this);
        _enemies.Add(enemy);
    }

    //---------------Enemy Delegate Methods--------------
    public void EnemyPressed(IEnemy enemy){
        Debug.Log("an enemy was pressed.");
        _delegate.EnemyPressed(enemy);
    }
}

public interface EnemyManagerDelegate {
    void EnemyPressed(IEnemy enemy);
}
