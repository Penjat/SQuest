using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {
    public string _name;
    public MoveType[] _partsUsed;

    public Move(string name, MoveType[] partsUsed){
        _name = name;
        _partsUsed = partsUsed;
    }
}
