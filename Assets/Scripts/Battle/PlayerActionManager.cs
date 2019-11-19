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
    //TODO posibly have IEnemy[] for multiple targets

    private StatusBar _playerHealthBar;
    private Player _player;

    public PlayerActionManager(Player player, StatusBar playerHealthBar){
        _player = player;
        _playerHealthBar = playerHealthBar;
        _playerHealthBar.SetUp(this,_player.GetMaxHealth(),50.0f);
        _playerHealthBar.SetValue(_player.GetCurHealth());
    }
    public void SelectMove(Move move){
        _curMove = move;
        Debug.Log("selected Move " + move._name);
    }
    public void SelectEnemy(IEnemy enemy){
        //TODO check if valid target
        if(_curMove == null){
            return;
        }
        UsedMoveOn(enemy);
    }
    public void CancelSelected(){
        _curMove = null;
    }
    public void UsedMoveOn(IEnemy enemy){
        //TODO fix to pass in array
        IEnemy[] targets = {enemy};
        _actions.Add(_curMove, targets);
        Debug.Log("using move " + _curMove._name + " on enemy.");
        AddToUsedParts(_curMove);
        _curMove = null;

    }
    public void CancelMoveType(MoveType moveType){
        foreach(KeyValuePair<Move,IEnemy[]> action in _actions){
            Move move = action.Key;
            if(move._partsUsed.Contains(moveType)){
                _actions.Remove(move);
                RemoveFromUsedParts(move._partsUsed);
                Debug.Log("removing move " + move._name);
                return;
                //doesn't cycle through everymove
                //This should be fine as there will only be on move using each body type
            }
        }
    }
    public void AddToUsedParts(Move move){

        foreach(MoveType m in move._partsUsed){
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
            foreach(IEnemy enemy in enemies){
                enemy.DoDmg(4.0f);
            }
        }
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
        PlayerTakeDmg(1);
    }
    public IEnemy[] GetTargetsFor(Move move){
        return _actions[move];
    }
    //---------------StatusBar Delegate-----------------
    public void DoneFilling(){

    }
}
