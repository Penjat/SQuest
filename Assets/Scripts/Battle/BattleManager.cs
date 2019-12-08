using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleManager : Menu, TurnManagerDelegate, EnemyManagerDelegate, ChooseMoveMenuDelegate, CategoryManagerDelegate, MiniGameDelegate, BattleMenuManagerDelegate, MainManagerDelegate {

    private SubManagerDelegate _delegate;

    public EnemyManager _enemyManager;
    public CategoryManager _categoryManager;
    private TurnManager _turnManager;
    private PlayerActionManager _playerActionManager;
    public MiniGameManager _miniGameManager;
    public PlayerControls _playerControls;
    public InfoLabelManager _infoLabelManager;
    private BattleTextFactory _battleTextFactory;

    public BattleMenuManager _battleMenuManager;

    public ChooseMoveMenu _moveMenu;
    public Text _battleStateLabel;

    

    private double _lastPress = 0.0f;

    void Start(){
        //Find the main manager
        SubManagerDelegate subDelegate = GameObject.Find("MainManager").GetComponent<SubManagerDelegate>();
        SetUp(subDelegate);
        Battle battle = _delegate.GetBattle();
        StartBattle(battle);
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            double click = Time.time;
            if(click - _lastPress <= 0.3){
                CancelSecetMove();
            }
            _lastPress = click;
        }
    }
    public void WaitBeforeTurn(float seconds){
        StartCoroutine(WaitTurn(seconds));
    }
    private IEnumerator WaitTurn(float seconds){
        yield return new WaitForSeconds(seconds);
        _turnManager.NextTurnEvent();
    }

    public void SetUp(SubManagerDelegate battleDelegate){

        Debug.Log("setting up for battle");
        _delegate = battleDelegate;
        _delegate.SetSubManager(this);

        _turnManager = new TurnManager(this);
        _enemyManager.SetUp(this);
        _categoryManager.SetUp(this);

        StatusBar playerHealthBar = GameObject.Find("Player Health").GetComponent<StatusBar>();
        StatusBar playerClimaxBar = GameObject.Find("Player Climax").GetComponent<StatusBar>();

        _playerActionManager = new PlayerActionManager(_delegate.GetPlayer(), playerHealthBar, playerClimaxBar);
        _moveMenu.SetUp(this);
        _infoLabelManager.SetUp(_playerActionManager);
        _battleTextFactory = new BattleTextFactory();
        _battleMenuManager.SetUp(this);
    }
    public void StartBattle(Battle battle) {
        _enemyManager.StartBattle(battle);
        _moveMenu.Hide();
        _miniGameManager.Hide();
        _turnManager.StartBattle();
        _infoLabelManager.CheckState();
        _battleStateLabel.text = "Ememies Appear!";
    }
    public void TakeTurn(){
        //triggered when the player is ready to take their turn
        _playerControls.ShowMenu(false);
        _turnManager.EndPlayerTurn();
        _playerActionManager.CancelSelected();
        _infoLabelManager.EndTurn();
        _battleStateLabel.text = "ending turn...";
    }

    public void CloseCategory(){
        _moveMenu.Hide();
    }
    public void PlayerDeath(){
        _miniGameManager.StopGame();
        _battleMenuManager.ShowLoseScreen();
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
        _infoLabelManager.CheckState();
        _battleStateLabel.text = "Player's Turn";
        Debug.Log("for realz " + _battleStateLabel.text);
    }
    public void StartPlayerAction(){
        //start the mini game
        Debug.Log("Start player action");
        string textToType = _battleTextFactory.GetText(_delegate.GetPlayer(), _playerActionManager.GetActions());
        _miniGameManager.StartGame(this,_playerActionManager.GetActions(),textToType);
        _battleStateLabel.text = "";
    }
    public void ResolvePlayerActions(){
        Debug.Log("Resolving Actions");
        _playerActionManager.UseMoves();
        _enemyManager.ResolveDMG();
        _battleStateLabel.text = "resolving actions...";
        _turnManager.EndResolveActions();
    }
    public void StartEnemyTurn(){
        //start the enemies turn
        Debug.Log("Start enemy turn");
        //TODO take the turn
        //_turnManager.EndEnemyTurn();

        //TODO wait some time
        _battleStateLabel.text = "Enemy Turn";
        _enemyManager.TakeTurn();
        //_turnManager.EndEnemyTurn();

    }
    public void CancelSecetMove(){
        _playerActionManager.CancelSelected();
        _infoLabelManager.MoveSelected();
        //TODO make sure this is ok
    }

    //--------------Enemy Manager Delegate----------------
    public void ShowMsg(string msg){
        _infoLabelManager.ShowMsg(msg);
    }
    public void EnemyPressed(IEnemy enemy){

        //make sure it is the player's turn
        bool isTurn = _turnManager.GetStage() == TurnStage.PlayerTurn;

        //make sure we are selecting a target
        bool selectingTarget = _playerActionManager.IsSelectingTarget();

        //make sure we can target the enemy with this move
        bool canTarget = enemy.CanTarget(_playerActionManager.GetCurMove());

        //return if any are not true
        if( !isTurn || !selectingTarget || !canTarget ){
            return;
        }

        //Check if it is an area affect move an sould target everyone
        if(_playerActionManager.GetCurMove().IsAreaFX()){
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
        _battleMenuManager.ShowWinScreen();

    }
    public void OverEnemy(IEnemy enemy){
        //only works if is the player's turn
        if(_turnManager.GetStage() != TurnStage.PlayerTurn){
            return;
        }

        //TODO check if is area effect
        if(_playerActionManager.IsSelectingTarget()){
            if(_playerActionManager.GetCurMove().IsAreaFX()){
                Debug.Log("this is an area affect move");
                _enemyManager.AreaAffect();
                //TODO change to area effect text
                _infoLabelManager.OverEnemy(enemy);
                return;
            }
            //check if the enemy can be targeted by the current move
            if(!enemy.CanTarget(_playerActionManager.GetCurMove())){
                _infoLabelManager.BlockedEnemy(enemy);
                return;
            }
            _infoLabelManager.OverEnemy(enemy);
            enemy.SetState(SelectState.Targeted);
        }
    }
    public void ExitEnemy(){
        _infoLabelManager.ExitEnemy();
        _enemyManager.ClearTargets();
    }
    public void DmgPlayer(int dmg){
        _playerActionManager.PlayerTakeDmg(dmg);
    }
    public void EndEnemyTurn(){
        Debug.Log("Ending Enemy Turn.");
        _turnManager.EndEnemyTurn();
    }
    //-------------Choose Move Menu Delegate---------------
    public void MoveSelected(Move move){
        _playerActionManager.SelectMove(move);
        _infoLabelManager.MoveSelected();
    }

    //-------------Category Manager Delegate--------------
    public void OpenCategory(MoveType moveType){
        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x.GetPartsUsed().Contains(moveType)).ToList();
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
    public void MiniGameFinished(IDictionary<Move,float> results){
        _playerActionManager.SetResults(results);
        _miniGameManager.Hide();
        _turnManager.EndPlayerAction();
    }
    public void GemCleared(MoveType moveType, float accuracy){
        _playerActionManager.GemCleared(moveType, accuracy);
    }
    //-----------------BattleMenuManager delegte--------------------
    public void ExitBattle(){
        _delegate.ExitBattle();
    }
}
