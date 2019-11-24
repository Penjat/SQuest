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
        // AddMove(new Move("Hand Job", new HashSet<MoveType>{MoveType.Hand}, 2));
        // AddMove(new Move("Blowjob", new HashSet<MoveType>{MoveType.Mouth}, 6));
        // AddMove(new Move("Kiss", new HashSet<MoveType>{MoveType.Mouth}, 1));
        // AddMove(new Move("Blowjob +Hands", new HashSet<MoveType>{MoveType.Mouth, MoveType.Hand}, 5));
        // AddMove(new Move("Doggy Style", new HashSet<MoveType>{MoveType.Ass, MoveType.Hand}, 6));
        // AddMove(new Move("Flash Tits", new HashSet<MoveType>{MoveType.Breasts}, 1, true));
        AddMove(new Blowjob());
        AddMove(new HandJob());

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
