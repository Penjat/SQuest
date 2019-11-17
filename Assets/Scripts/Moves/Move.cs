using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {
    public string _name;
    public HashSet<MoveType> _partsUsed;

    public Move(string name, HashSet<MoveType> partsUsed){
        _name = name;
        _partsUsed = partsUsed;
    }
    public bool CheckLocked(IDictionary<MoveType,Move> partsBeingUsed){
        //returns true if this move requires a part that is used already
        foreach(MoveType m in _partsUsed){
            if(partsBeingUsed.ContainsKey(m)){
                return true;
            }
        }
        return false;
    }
}
