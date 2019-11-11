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
    public bool CheckLocked(HashSet<MoveType> partsBeingUsed){
        //returns true if this move requires a part that is used already
        foreach(MoveType m in _partsUsed){
            if(partsBeingUsed.Contains(m)){
                return true;
            }
        }
        return false;
    }
}
