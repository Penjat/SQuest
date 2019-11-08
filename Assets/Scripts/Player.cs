using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private List<Move> _moves;

    public Player(){
        _moves = new List<Move>();
        AddMove(new Move("move 1", new []{MoveType.Hand}));
        AddMove(new Move("move 2", new []{MoveType.Mouth}));
        AddMove(new Move("move 6", new []{MoveType.Ass, MoveType.Hand }));

    }
    public void AddMove(Move move){
        _moves.Add(move);
    }

    public List<Move> GetMoves(){
        return _moves;
    }
}
