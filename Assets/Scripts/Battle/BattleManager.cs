using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleManager : Menu, TurnManagerDelegate, EnemyManagerDelegate, ChooseMoveMenuDelegate, CategoryManagerDelegate, MiniGameDelegate {

    public EnemyManager _enemyManager;
    public CategoryManager _categoryManager;
    private TurnManager _turnManager;
    private PlayerActionManager _playerActionManager;
    public MiniGameManager _miniGameManager;
    public PlayerControls _playerControls;

    private BattleManagerDelegate _delegate;

    public ChooseMoveMenu _moveMenu;
    public Text _infoLabel;


    public void SetUp(BattleManagerDelegate battleDelegate){
        _delegate = battleDelegate;
        _turnManager = new TurnManager(this);
        _enemyManager.SetUp(this);
        _categoryManager.SetUp(this);

        StatusBar playerHealthBar = GameObject.Find("Player Health").GetComponent<StatusBar>();
        _playerActionManager = new PlayerActionManager(_delegate.GetPlayer(), playerHealthBar);
        _moveMenu.SetUp(this);
    }
    public void StartBattle(Battle battle) {
        _enemyManager.StartBattle(battle);
        _moveMenu.Hide();
        _miniGameManager.Hide();
        _turnManager.StartBattle();
    }
    public void TakeTurn(){
        //triggered when the player is ready to take their turn
        _playerControls.ShowMenu(false);
        _turnManager.EndPlayerTurn();
    }

    public void CloseCategory(){
        _moveMenu.Hide();
    }

    //------------Turn Manager Delegate------------
    public void StartPlayerTurn(){
        Debug.Log("Start player turn");
        _playerControls.ShowMenu(true);

        //Reset all moves
        _playerActionManager.ClearUsedParts();
        IDictionary<MoveType,Move> used = _playerActionManager.GetUsedParts();
        print("used count = " + used.Count);
        _categoryManager.CheckCategories(used);
    }
    public void StartPlayerAction(){
        //start the mini game
        Debug.Log("Start player action");
        _miniGameManager.StartGame(this,_playerActionManager.GetUsedParts());
    }
    public void StartEnemyTurn(){
        //start the enemies turn
        Debug.Log("Start enemy turn");
        //TODO take the turn
        //_turnManager.EndEnemyTurn();
        _playerActionManager.UseMoves();
        //TODO wait some time
        _turnManager.EndEnemyTurn();
    }
    public void CancelSecetMove(){
        _infoLabel.text = "";
        _playerActionManager.CancelSelected();
    }
    //---------------------------------------------------
    //-------------Delegate Methods---------------------
    //---------------------------------------------------

    //--------------Enemy Manager Delegate----------------
    public void EnemyPressed(IEnemy enemy){
        //make sure it is the player's turn
        if(_turnManager.GetStage() != TurnStage.PlayerTurn ){
            return;
        }
        _playerActionManager.SelectEnemy(enemy);
        _infoLabel.text = "";
        _categoryManager.CheckCategories(_playerActionManager.GetUsedParts());
    }
    public void DoneBattle(){
        //TODO post battle screen
        _enemyManager.ClearEnemies();
        _delegate.DoneBattle();

    }
    //-------------Choose Move Menu Delegate---------------
    public void MoveSelected(Move move){
        _playerActionManager.SelectMove(move);
        _infoLabel.text = "use " + move._name + " on...";
    }

    //-------------Category Manager Delegate--------------
    public void OpenCategory(MoveType moveType){
        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x._partsUsed.Contains(moveType)).ToList();
        IDictionary<MoveType,Move> partsUsed = _playerActionManager.GetUsedParts();
        _moveMenu.Show(moves, partsUsed, moveType);
    }
    public void CancelMove(MoveType moveType){
        //make sure it is the player's turn
        if(_turnManager.GetStage() != TurnStage.PlayerTurn ){
            return;
        }
        _playerActionManager.CancelMoveType(moveType);
        _categoryManager.CheckCategories(_playerActionManager.GetUsedParts());
    }
    public void ShowTargets(Move selectedMove){
        IEnemy[] targeted = _playerActionManager.GetTargetsFor(selectedMove);
        _enemyManager.SetTargeted(targeted);
    }
    public void HideTargets(){
        _enemyManager.ClearTargets();
    }

    //-------------MiniGame Delegate----------------------
    public void MiniGameFinished(){
        _miniGameManager.Hide();
        _turnManager.EndPlayerAction();
    }
    public void GemCleared(MoveType moveType, float accuracy){
        _playerActionManager.GemCleared(moveType, accuracy);
    }
}

public interface BattleManagerDelegate{
    //TODO pass in data about battle results
    void DoneBattle();
    Player GetPlayer();
}
