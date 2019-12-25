using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//TODO remove
using UnityEngine.SceneManagement;

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
        GameObject g = GameObject.Find("MainManager");
        //TODO do this properly
        //check if on correct scene
        if(g == null){
            SceneManager.LoadScene("Title");
            return;
        }
        SubManagerDelegate subDelegate = g.GetComponent<SubManagerDelegate>();



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
        _categoryManager.SetUp(this,_delegate.GetPlayer().GetBodyParts());

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


    public void OverPlayer(){
        Debug.Log("Over Player.");
        _infoLabelManager.OverPlayer();
    }
    public void ExitPlayer(){
        Debug.Log("Exit Player.");
        _infoLabelManager.CheckState();
    }
    public void PressedPlayer(){
        Debug.Log("Pressed Player.");
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

        //check what body parts are available and unlock categories
        IDictionary<IBodyPart,Move> used = _playerActionManager.GetUsedParts();
        _categoryManager.CheckCategories(used);

        //check what categories should be shown
        HashSet<MoveType> availableTypes = new HashSet<MoveType>(_delegate.GetPlayer().GetMoves().Select(x => x.GetPrimaryType()));
        _categoryManager.CheckAvailableCategories(availableTypes);

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
        //_turnManager.EndResolveActions();
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
    public void EnemyClimax(IEnemy enemy){
        _categoryManager.ClimaxOn(enemy);
    }
    public void DoneResolvingEnemies(){
        _turnManager.EndResolveActions();
    }
    public void ShowMsg(string msg){
        _infoLabelManager.ShowMsg(msg);
    }
    public void EnemyPressed(ITarget target){

        //make sure it is the player's turn
        bool isTurn = _turnManager.GetStage() == TurnStage.PlayerTurn;

        //make sure we are selecting a target
        bool selectingTarget = _playerActionManager.IsSelectingTarget();

        //make sure we can target the enemy with this move
        bool canTarget = target.CanTarget(_playerActionManager.GetCurMove()) == TargetResult.Available;

        //return if any are not true
        if( !isTurn || !selectingTarget || !canTarget ){
            return;
        }

        //Check if it is an area affect move an sould target everyone
        if(_playerActionManager.GetCurMove().IsAreaFX()){
            _playerActionManager.SelectTargets(_enemyManager.GetEnemiesAsArray());
        }else{
            _playerActionManager.SelectTargets(new ITarget[]{target});
        }

        _categoryManager.CheckCategories(_playerActionManager.GetUsedParts());
        _infoLabelManager.MoveSelected();
        target.SetState(SelectState.Over);
        target.StopFlashingParts();
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
            TargetResult targetResult = enemy.CanTarget(_playerActionManager.GetCurMove());

            switch(targetResult){
                case TargetResult.Available:
                _infoLabelManager.OverEnemy(enemy);
                enemy.TargetWithMove(_playerActionManager.GetCurMove());
                break;

                case TargetResult.AlreadyTargeted:
                _infoLabelManager.BlockedEnemy(enemy);
                break;

                case TargetResult.NotMatch:
                _infoLabelManager.MoveNotMatch(enemy,_playerActionManager.GetCurMove());
                break;
            }
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
    public void MoveSelected(Move move, IBodyPart bodyPart){
        _playerActionManager.SelectMove(move, bodyPart);
        _infoLabelManager.MoveSelected();
    }

    //-------------Category Manager Delegate--------------
    public void OpenCategory(IBodyPart bodyPart){
        List<Move> moves = _delegate.GetPlayer().GetMoves().Where(x => x.GetPrimaryType() == bodyPart.GetMoveType()).ToList();

        Player player = _delegate.GetPlayer();
        IDictionary<IBodyPart,Move> usedParts = _playerActionManager.GetUsedParts();
        List<MoveType> availableParts = player.GetBodyParts().Where(x => !usedParts.ContainsKey(x)).Select(x => x.GetMoveType()).ToList();

        _moveMenu.Show(moves, availableParts, bodyPart);
        _playerActionManager.CancelSelected();
    }
    public void CancelMove(IBodyPart bodyPart){
        //make sure it is the player's turn
        if(_turnManager.GetStage() != TurnStage.PlayerTurn ){
            return;
        }
        //TODO rename
        _playerActionManager.CancelMoveType(bodyPart);
        _categoryManager.CheckCategories(_playerActionManager.GetUsedParts());
    }
    public void ShowTargets(IBodyPart bodyPart, Move selectedMove){
        ITarget[] targeted = _playerActionManager.GetTargetsFor(bodyPart);
        _enemyManager.SetTargeted(targeted, selectedMove);
        _infoLabelManager.ShowTargetsForMove(bodyPart, selectedMove);
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
