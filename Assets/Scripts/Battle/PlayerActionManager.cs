using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager {

    Move _curMove;
    IDictionary<Move,IEnemy> _actions = new Dictionary<Move, IEnemy>();
    HashSet<MoveType> _usedParts = new HashSet<MoveType>();
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
        AddToUsedParts(_curMove._partsUsed);
        _curMove = null;

    }
    public void AddToUsedParts(MoveType[] parts){
        foreach(MoveType m in parts){
            _usedParts.Add(m);
        }
    }
    public HashSet<MoveType> GetUsedParts(){
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
        }
    }
}
