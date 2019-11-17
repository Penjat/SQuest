using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private List<Move> _moves;

    public Player(){
        _moves = new List<Move>();
        AddMove(new Move("Hand Job", new HashSet<MoveType>{MoveType.Hand}));
        AddMove(new Move("Blowjob", new HashSet<MoveType>{MoveType.Mouth}));
        AddMove(new Move("Kiss", new HashSet<MoveType>{MoveType.Mouth}));
        AddMove(new Move("Blowjob +Hands", new HashSet<MoveType>{MoveType.Mouth, MoveType.Hand}));
        AddMove(new Move("Flash Tits", new HashSet<MoveType>{MoveType.Breasts}));

    }
    public void AddMove(Move move){
        _moves.Add(move);
    }

    public List<Move> GetMoves(){
        return _moves;
    }
}
