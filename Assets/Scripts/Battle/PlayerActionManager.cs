using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager {

    Move _curMove;
    IDictionary<Move,IEnemy> _actions = new Dictionary<Move, IEnemy>();
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
        _actions.Add(_curMove, enemy);
        Debug.Log("using move " + _curMove._name + " on enemy.");
        _curMove = null;
    }
    public void CancelSelected(){
        _curMove = null;
    }
}
