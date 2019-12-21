using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    PlayerDelegate _delegate;

    private List<Move> _moves;

    private int _maxHealth = 100;
    private int _curHealth = 100;

    private int _maxClimax = 100;
    private int _curClimax = 0;

    public Player(PlayerDelegate playerDelegate){
        _delegate = playerDelegate;
        _moves = new List<Move>();

    }
    public void AddMove(Move move){
        _moves.Add(move);
    }
    public void ClearMoves(){
        //removes all the player's moves
        _moves.Clear();
    }

    public List<Move> GetMoves(){
        return _moves;
    }
    public void TakeDmg(int dmg){
        _curHealth -= dmg;
        if(_curHealth < 0){
            _curHealth = 0;
            _delegate.HealthIsZero();
        }
    }
    public int GetMaxHealth(){
        return _maxHealth;
    }
    public int GetCurHealth(){
        return _curHealth;
    }
    public int GetMaxClimax(){
        return _maxClimax;
    }
    public int GetCurClimax(){
        return _curClimax;
    }
    public string BreastDescription(){
        return "C Cup Breasts";
    }

}

public interface PlayerDelegate{
    void HealthIsZero();
}
