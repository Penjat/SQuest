using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager {

    Move _curMove;
    IDictionary<Move,IEnemy> _actions = new Dictionary<Move, IEnemy>();
    IDictionary<MoveType,Move> _usedParts = new Dictionary<MoveType, Move>();
    //TODO posibly have IEnemy[] for multiple targets

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
        _actions.Add(_curMove, enemy);
        Debug.Log("using move " + _curMove._name + " on enemy.");
        AddToUsedParts(_curMove);
        _curMove = null;

    }
    public void CancelMoveType(MoveType moveType){
        foreach(KeyValuePair<Move,IEnemy> action in _actions){
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
        //TODO refactor for mini game
        foreach(KeyValuePair<Move, IEnemy> action in _actions){
            Debug.Log("using "+ action.Key._name + " on " + action.Value.GetName());
            IEnemy enemy = action.Value;
            enemy.DoDmg(4.0f);
        }
    }
}
