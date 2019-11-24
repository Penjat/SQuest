using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence  {
    Note[] _noteArray;
    MoveType _moveType;
    public Sequence(MoveType moveType, Note[] noteArray){
        _moveType = moveType;
        _noteArray = noteArray;
    }
    public Note[] GetNotes(){
        return _noteArray;
    }
    public MoveType GetMoveType(){
        return _moveType;
    }
}
