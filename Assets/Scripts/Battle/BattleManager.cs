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
    public InfoLabelManager _infoLabelManager;

    private BattleManagerDelegate _delegate;

    public ChooseMoveMenu _moveMenu;

    private double _lastPress = 0.0f;

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            double click = Time.time;
            if(click - _lastPress <= 0.3){
                CancelSecetMove();
            }
            _lastPress = click;
        }
    }

    public void SetUp(BattleManagerDelegate battleDelegate){
        _delegate = battleDelegate;
        _turnManager = new TurnManager(this);
        _enemyManager.SetUp(this);
        _categoryManager.SetUp(this);

        StatusBar playerHealthBar = GameObject.Find("Player Health").GetComponent<StatusBar>();
        _playerActionManager = new PlayerActionManager(_delegate.GetPlayer(), playerHealthBar);
        _moveMenu.SetUp(this);
        _infoLabelManager.SetUp(_playerActionManager);
    }
    public void StartBattle(Battle battle) {
        _enemyManager.StartBattle(battle);
        _moveMenu.Hide();
        _miniGameManager.Hide();
        _turnManager.StartBattle();
        _infoLabelManager.CheckState();
    }
    public void TakeTurn(){
        //triggered when the player is ready to take their turn
        _playerControls.ShowMenu(false);
        _turnManager.EndPlayerTurn();
        _playerActionManager.CancelSelected();
        _infoLabelManager.EndTurn();
    }

    public void CloseCategory(){
        _moveMenu.Hide();
    }



    //------------------------------------------------------
    //                   DELEGATE METHODS
    //------------------------------------------------------

    //------------Turn Manager Delegate------------
    public void StartPlayerTurn(){
        Debug.Log("Start player turn");
        _playerControls.ShowMenu(true);

        //Reset all moves
        _playerActionManager.ClearUsedParts();
        IDictionary<MoveType,Move> used = _playerActionManager.GetUsedParts();
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
        _playerActionManager.CancelSelected();
        _infoLabelManager.MoveSelected();
        //TODO make sure this is ok
    }

    //--------------Enemy Manager Delegate----------------
    public void EnemyPressed(IEnemy enemy){
        //make sure it is the player's turn
        //make sure we are selecting a target
        if(_turnManager.GetStage() != TurnStage.PlayerTurn || !_playerActionManager.IsSelectingTarget()){
            return;
        }
        if(_playerActionManager.GetCurMove()._areaAffect){
            _playerActionManager.SelectTargets(_enemyManager.GetEnemiesAsArray());
        }else{
            _playerActionManager.SelectTargets(new IEnemy[]{enemy});
        }

        _categoryManager.CheckCategories(_playerActionManager.GetUsedParts());
        _infoLabelManager.MoveSelected();
        enemy.SetState(SelectState.Over);
        _enemyManager.ClearTargets();
    }
    public void DoneBattle(){
        //TODO post battle screen
        _enemyManager.ClearEnemies();
        _delegate.DoneBattle();

    }
    public void OverEnemy(IEnemy enemy){
        _infoLabelManager.OverEnemy(enemy);
        //TODO check if is area effect
        if(_playerActionManager.IsSelectingTarget()){
            if(_playerActionManager.GetCurMove()._areaAffect){
                Debug.Log("this is an area affect move");
                _enemyManager.AreaAffect();
            }
            enemy.SetState(SelectState.Targeted);
        }
    }
    public void ExitEnemy(){
        _infoLabelManager.ExitEnemy();
        _enemyManager.ClearTargets();
    }
    //-------------Choose Move Menu Delegate---------------
    public void MoveSelected(Move move){
        _playerActionManager.SelectMove(move);
        _infoLabelManager.MoveSelected();
    }

    //-------------Category Manager Delegate--------------
    public void OpenCategory(MoveType moveType){
        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x._partsUsed.Contains(moveType)).ToList();
        IDictionary<MoveType,Move> partsUsed = _playerActionManager.GetUsedParts();
        _moveMenu.Show(moves, partsUsed, moveType);
        _playerActionManager.CancelSelected();
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
        _infoLabelManager.ShowTargetsForMove(selectedMove);
    }
    public void HideTargets(){
        _enemyManager.ClearTargets();
        _infoLabelManager.HideTargets();
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
