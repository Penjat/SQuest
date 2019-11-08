using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleManager : Menu, TurnManagerDelegate, EnemyManagerDelegate, ChooseMoveMenuDelegate, CategoryManagerDelegate {

    public EnemyManager _enemyManager;
    public CategoryManager _categoryManager;
    private TurnManager _turnManager;
    private PlayerActionManager _playerActionManager;

    private BattleManagerDelegate _delegate;


    public ChooseMoveMenu _moveMenu;
    public Text _infoLabel;

    public void SetUp(BattleManagerDelegate battleDelegate){
        _delegate = battleDelegate;
        _turnManager = new TurnManager(this);
        _enemyManager.SetUp(this);
        _categoryManager.SetUp(this);
        _playerActionManager = new PlayerActionManager();
        _moveMenu.SetUp(this);
    }
    public void StartBattle(Battle battle) {
        _turnManager.StartBattle();
        _enemyManager.StartBattle(battle);
        _moveMenu.Hide();
    }
    public void TakeTurn(){
        //triggered when the player is ready to take their turn
        _turnManager.EndPlayerTurn();
    }

    public void CloseCategory(){
        _moveMenu.Hide();
    }

    //------------Turn Manager Delegate------------
    public void StartPlayerTurn(){
        Debug.Log("Start player turn");
    }
    public void StartPlayerAction(){
        //start the mini game
        //TODO impliment Game
        Debug.Log("Start player action");
        _turnManager.EndPlayerAction();
    }
    public void StartEnemyTurn(){
        //start the enemies turn
        Debug.Log("Start enemy turn");
        //TODO take the turn
        _turnManager.EndEnemyTurn();
    }
    public void CancelSecetMove(){
        _infoLabel.text = "";
        _playerActionManager.CancelSelected();
    }

    //--------------Enemy Manager Delegate----------------
    public void EnemyPressed(IEnemy enemy){
        //make sure it is the player's turn
        if(_turnManager.GetStage() != TurnStage.PlayerTurn ){
            return;
        }
        _playerActionManager.SelectEnemy(enemy);
        _infoLabel.text = "";
    }
    //-------------Choose Move Menu Delegate---------------
    public void MoveSelected(Move move){
        _playerActionManager.SelectMove(move);
        _infoLabel.text = "use " + move._name + " on...";
    }

    //-------------Category Manager Delegate
    public void OpenCategory(MoveType moveType){
        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x._partsUsed.Contains(moveType)).ToList();
        _moveMenu.Show(moves);
    }
}

public interface BattleManagerDelegate{
    //TODO pass in data about battle results
    void DoneBattle();
    Player GetPlayer();
}
