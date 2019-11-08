using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleManager : Menu {

    private BattleManagerDelegate _delegate;
    private List<IEnemy> _enemies;

    //TODO move this to Enemy Factory
    public GameObject _impPrefab;

    public RectTransform _enemyContainer;
    public ChooseMoveMenu _moveMenu;

    public void SetUp(BattleManagerDelegate battleDelegate){
        _delegate = battleDelegate;
    }
    public void StartBattle(Battle battle) {
        Debug.Log("Starting Battle");
        _enemies = new List<IEnemy>();
        CreateEnemy();
        CreateEnemy();
        CreateEnemy();
        _moveMenu.Hide();
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

        _enemies.Add(enemy);
    }
    public void TakeTurn(){
        //triggered when the player is ready to take their turn
        if(_enemies.Count == 0){
            _delegate.DoneBattle();
        }
    }
    public void OpenCategory(int i){
        Debug.Log("open category " + i);
        MoveType moveType = MoveTypeHelper.FindMoveType(i);

        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x._partsUsed.Contains(moveType)).ToList();
        _moveMenu.Show(moves);
    }
    public void CloseCategory(){
        _moveMenu.Hide();
    }

}
public interface BattleManagerDelegate{
    //TODO pass in data about battle results
    void DoneBattle();
    Player GetPlayer();
}
