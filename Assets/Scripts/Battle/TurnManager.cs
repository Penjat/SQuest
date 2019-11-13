using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnStage {
    PlayerTurn, //player is choosing their moves
    PlayerAction, //after the player ends turn, mini game
    EnemyTurn }; //Enemy doing their moves

public class TurnManager {
    TurnManagerDelegate _delegate;
    TurnStage _stage;
    public TurnManager(TurnManagerDelegate turnManagerDelegate){
        _delegate = turnManagerDelegate;
    }
    public void StartBattle(){
        //called when the battle starts
        Debug.Log("Starting Battleee");
        _stage = TurnStage.PlayerTurn;
        _delegate.StartPlayerTurn();
    }
    public void EndPlayerTurn(){
        //called when player presses ready
        Debug.Log("Ending player turn");
        _stage = TurnStage.PlayerAction;
        _delegate.StartPlayerAction();
    }
    public void EndPlayerAction(){
        //called after mini game
        Debug.Log("Ending player action");
        _stage = TurnStage.EnemyTurn;
        _delegate.StartEnemyTurn();
    }
    public void EndEnemyTurn(){
        //called after AI takes turn
        Debug.Log("Ending enemy turn");
        _stage = TurnStage.PlayerTurn;
        _delegate.StartPlayerTurn();
    }
    public TurnStage GetStage(){
        return _stage;
    }
}

public interface TurnManagerDelegate {
    void StartPlayerTurn();
    void StartPlayerAction();
    void StartEnemyTurn();
}
