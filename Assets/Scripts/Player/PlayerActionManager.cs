using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager : StatusBarDelegate {

    private PlayerAction _curAction;
    private IDictionary<PlayerAction,IEnemy[]> _actions = new Dictionary<PlayerAction, IEnemy[]>();
    private IDictionary<IBodyPart,Move> _usedParts = new Dictionary<IBodyPart, Move>();
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
    public void SelectMove(Move move, IBodyPart categoryBodyPart){
        //selected a move from a category

        //create a collection of body parts for the move
        List<IBodyPart> bodyParts = new List<IBodyPart>();
        foreach(MoveType moveType in move.GetPartsUsed()){
            //make sure the body part we clicked on is used
            if(moveType == move.GetPrimaryType() && !bodyParts.Contains(categoryBodyPart)){
                bodyParts.Add(categoryBodyPart);
                break;
            }
            //find the  body part that is not in used parts and matches the move type
            IBodyPart bodyPart = _player.GetBodyParts().First(x => x.GetMoveType() == moveType && !_usedParts.ContainsKey(x));
            bodyParts.Add(bodyPart);
        }

        _curAction = new PlayerAction(move, bodyParts.ToList());
        Debug.Log("selected Move " + move.GetName());
    }
    public void SelectTargets(IEnemy[] targets){
        //sets the move to be used on the seleceted enemy
        //TODO check if valid target
        if(_curAction == null){
            return;
        }
        UsedMoveOn(targets);
    }
    public void CancelSelected(){
        _curAction = null;
    }
    public void UsedMoveOn(IEnemy[] targets){
        //TODO change for area fx
        foreach(IEnemy enemy in targets){
            enemy.TargetWith(_curAction.GetMove());
        }
        _actions.Add(_curAction, targets);
        AddToUsedParts(_curAction.GetMove());
        _curAction = null;
    }
    public void CancelMoveType(IBodyPart bodyPart){
        Move move = _usedParts[bodyPart];
        PlayerAction action = _actions.Select(x => x.Key).First(x => x.GetParts().Contains(bodyPart));
        IEnemy[] targets = _actions[action];

        UnTargetAll(targets, move);
        RemoveFromUsedParts(action);
        _actions.Remove(action);
        Debug.Log("removing move " + move.GetName());
        return;
    }
    private void UnTargetAll(IEnemy[] targets, Move move){
        //untargets all the enemies from the move
        foreach(IEnemy enemy in targets){
            enemy.UnTarget(move);
        }
    }
    public void AddToUsedParts(Move move){

        foreach(MoveType moveType in move.GetPartsUsed()){
            //find the first available body part of the same type
            IBodyPart bodyPart = _player.GetBodyParts().First(x => x.GetMoveType() == moveType && !_usedParts.ContainsKey(x));
            _usedParts.Add(bodyPart,move);
        }
    }
    public void RemoveFromUsedParts(PlayerAction action){
        foreach(IBodyPart bodyPart in action.GetParts()){
            _usedParts.Remove(bodyPart);
        }
    }
    public IDictionary<IBodyPart,Move> GetUsedParts(){
        return _usedParts;
    }
    public void ClearUsedParts(){
        _usedParts.Clear();
        _actions.Clear();
    }
    public void UseMoves(){
        foreach(KeyValuePair<PlayerAction, IEnemy[]> action in _actions){
            IEnemy[] enemies = action.Value;
            Move move = action.Key.GetMove();
            List<IBodyPart> bodyParts = action.Key.GetParts();
            float percent = FindPercent(_actionResults,move);
            //TODO fix for Percent
            Dmg dmg = move.GetDmg();
            dmg = dmg.ApplyParts(bodyParts);
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
    public IEnemy[] GetTargetsFor(IBodyPart bodyPart){
        PlayerAction action = _actions.First(x => x.Key.GetParts().Contains(bodyPart)).Key;
        return _actions[action];
    }
    public bool IsSelectingTarget(){
        return _curAction != null;
    }
    public Move GetCurMove(){
        return _curAction.GetMove();
    }
    public IDictionary<PlayerAction, IEnemy[]> GetActions(){
        return _actions;
    }
    public void SetResults(IDictionary<Move,float> results){
        _actionResults = results;
    }
    //---------------StatusBar Delegate-----------------
    public void DoneFilling(int refNumber){

    }
}
