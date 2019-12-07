using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnStage {
    BattleStart,//Only happens at begining of battle
    PlayerTurn, //player is choosing their moves
    PlayerAction, //after the player ends turn, mini game
    ResolvePlayerActions,//after mini game, applies the moves to enemies
    EnemyTurn }; //Enemy doing their moves

public class TurnManager {
    TurnManagerDelegate _delegate;
    TurnStage _stage;
    public TurnManager(TurnManagerDelegate turnManagerDelegate){
        _delegate = turnManagerDelegate;
    }
    public void NextTurnEvent(){
        Debug.Log("next turn event...");
        //based on what the current stage is will move to the next one
        switch(_stage){
            case TurnStage.BattleStart:
                _stage = TurnStage.PlayerTurn;
                _delegate.StartPlayerTurn();
                return;
            case TurnStage.PlayerTurn:
                _stage = TurnStage.PlayerAction;
                _delegate.StartPlayerAction();
                return;
            case TurnStage.PlayerAction:
                _stage = TurnStage.ResolvePlayerActions;
                _delegate.ResolvePlayerActions();
                return;
            case TurnStage.ResolvePlayerActions:
                _stage = TurnStage.EnemyTurn;
                _delegate.StartEnemyTurn();
                return;
        }
    }
    public void StartBattle(){
        //called when the battle starts
        Debug.Log("Starting Battleee");
        _stage = TurnStage.BattleStart;
        _delegate.WaitBeforeTurn(1.8f);
    }
    public void EndPlayerTurn(){
        //called when player presses ready
        Debug.Log("Ending player turn");
        _delegate.WaitBeforeTurn(0.8f);
    }
    public void EndPlayerAction(){
        //called after mini game
        Debug.Log("Ending player action");
        _delegate.WaitBeforeTurn(0.5f);
        //_stage = TurnStage.EnemyTurn;
        //_delegate.StartEnemyTurn();
    }
    public void EndResolveActions(){
        Debug.Log("Ending resolving actions");
        _delegate.WaitBeforeTurn(1.2f);
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
    void ResolvePlayerActions();
    void StartEnemyTurn();
    void WaitBeforeTurn(float seconds);
}
