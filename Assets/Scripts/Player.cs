using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    PlayerDelegate _delegate;

    private List<Move> _moves;

    private int _maxHealth = 100;
    private int _curHealth = 44;

    public Player(PlayerDelegate playerDelegate){
        _delegate = playerDelegate;
        _moves = new List<Move>();
        AddMove(new Move("Hand Job", new HashSet<MoveType>{MoveType.Hand}));
        AddMove(new Move("Blowjob", new HashSet<MoveType>{MoveType.Mouth}));
        AddMove(new Move("Kiss", new HashSet<MoveType>{MoveType.Mouth}));
        AddMove(new Move("Blowjob +Hands", new HashSet<MoveType>{MoveType.Mouth, MoveType.Hand}));
        AddMove(new Move("Flash Tits", new HashSet<MoveType>{MoveType.Breasts},true));

    }
    public void AddMove(Move move){
        _moves.Add(move);
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
}

public interface PlayerDelegate{
    void HealthIsZero();
}
