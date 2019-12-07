using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager : StatusBarDelegate {

    private Move _curMove;
    private IDictionary<Move,IEnemy[]> _actions = new Dictionary<Move, IEnemy[]>();
    private IDictionary<MoveType,Move> _usedParts = new Dictionary<MoveType, Move>();
    private IDictionary<Move,float> _actionResults;
    //TODO posibly have IEnemy[] for multiple targets

    private StatusBar _playerHealthBar;
    private StatusBar _playerClimaxBar;
    private Player _player;

    public PlayerActionManager(Player player, StatusBar playerHealthBar, StatusBar playerClimaxBar){
        _player = player;
        _playerHealthBar = playerHealthBar;
        _playerHealthBar.SetUp(this,_player.GetMaxHealth(),50.0f);
        _playerHealthBar.SetValue(_player.GetCurHealth());

        _playerClimaxBar = playerClimaxBar;
        _playerClimaxBar.SetUp(this,_player.GetMaxClimax(),50.0f);
        _playerClimaxBar.SetValue(_player.GetCurClimax());
    }
    public void SelectMove(Move move){
        //selected a move from a category
        _curMove = move;
        Debug.Log("selected Move " + move.GetName());
    }
    public void SelectTargets(IEnemy[] targets){
        //sets the move to be used on the seleceted enemy
        //TODO check if valid target
        if(_curMove == null){
            return;
        }
        UsedMoveOn(targets);
    }
    public void CancelSelected(){
        _curMove = null;
    }
    public void UsedMoveOn(IEnemy[] targets){
        //TODO change for area fx
        foreach(IEnemy enemy in targets){
            enemy.TargetWith(_curMove);
        }
        _actions.Add(_curMove, targets);
        AddToUsedParts(_curMove);
        _curMove = null;
    }
    public void CancelMoveType(MoveType moveType){
        foreach(KeyValuePair<Move,IEnemy[]> action in _actions){
            Move move = action.Key;
            IEnemy[] targets = action.Value;
            if(move.GetPartsUsed().Contains(moveType)){
                _actions.Remove(move);
                UnTargetAll(targets, move);
                RemoveFromUsedParts(move.GetPartsUsed());
                Debug.Log("removing move " + move.GetName());
                return;
                //doesn't cycle through everymove
                //This should be fine as there will only be on move using each body type
            }
        }
    }
    private void UnTargetAll(IEnemy[] targets, Move move){
        //untargets all the enemies from the move
        foreach(IEnemy enemy in targets){
            enemy.UnTarget(move);
        }
    }
    public void AddToUsedParts(Move move){

        foreach(MoveType m in move.GetPartsUsed()){
            _usedParts.Add(m,move);
        }
    }
    public void RemoveFromUsedParts(HashSet<MoveType> parts){
        foreach(MoveType m in parts){
            _usedParts.Remove(m);
        }
    }
    public IDictionary<MoveType,Move> GetUsedParts(){
        return _usedParts;
    }
    public void ClearUsedParts(){
        _usedParts.Clear();
        _actions.Clear();
    }
    public void UseMoves(){
        foreach(KeyValuePair<Move, IEnemy[]> action in _actions){
            IEnemy[] enemies = action.Value;
            Move move = action.Key;
            float percent = FindPercent(_actionResults,move);
            int dmg = move.GetDmg(percent);
            foreach(IEnemy enemy in enemies){
                enemy.AddToDmg(dmg);
                enemy.UseMove(move, percent);
            }
        }
    }
    private float FindPercent(IDictionary<Move,float> results, Move move){
        //trys to find the results for the given move
        if(results.ContainsKey(move)){
            float percent = results[move];
            return percent;
        }
        //if cant find the move for some reason
        //just return 100%
        Debug.Log("WARNING could not find key");
        return 100.0f;

    }
    public void PlayerTakeDmg(int dmg){
        _player.TakeDmg(dmg);
        _playerHealthBar.SetValueAnimated(_player.GetCurHealth());
    }
    public void GemCleared(MoveType moveType, float accuracy){
        //there will be many modifiers to add here
        if(moveType == MoveType.Hand){
            //hand jobs dont do dmg to player
            return;
        }
        //TODO calculate this properly
        if(accuracy > 99.0f){
            PlayerTakeDmg(1);
        }
    }
    public IEnemy[] GetTargetsFor(Move move){
        return _actions[move];
    }
    public bool IsSelectingTarget(){
        return _curMove != null;
    }
    public Move GetCurMove(){
        return _curMove;
    }
    public IDictionary<Move, IEnemy[]> GetActions(){
        return _actions;
    }
    public void SetResults(IDictionary<Move,float> results){
        _actionResults = results;
    }
    //---------------StatusBar Delegate-----------------
    public void DoneFilling(){

    }
}
