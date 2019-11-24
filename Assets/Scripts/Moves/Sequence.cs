using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence  {
    int[] _array;
    MoveType _moveType;
    public Sequence(MoveType moveType, int[] array){
        _moveType = moveType;
        _array = array;
    }
    public int[] GetArray(){
        return _array;
    }
    public MoveType GetMoveType(){
        return _moveType;
    }
}
