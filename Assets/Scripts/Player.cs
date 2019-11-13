using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private List<Move> _moves;

    public Player(){
        _moves = new List<Move>();
        AddMove(new Move("Hand Job", new []{MoveType.Hand}));
        AddMove(new Move("Blowjob", new []{MoveType.Mouth}));
        AddMove(new Move("Kiss", new []{MoveType.Mouth}));
        AddMove(new Move("Blowjob +Hands", new []{MoveType.Mouth, MoveType.Hand}));
        AddMove(new Move("Flash Tits", new []{MoveType.Breasts}));

    }
    public void AddMove(Move move){
        _moves.Add(move);
    }

    public List<Move> GetMoves(){
        return _moves;
    }
}
