using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {
    public string _name;
    public HashSet<MoveType> _partsUsed;
    public bool _areaAffect;

    public Move(string name, HashSet<MoveType> partsUsed,bool areaAffect=false){
        _name = name;
        _partsUsed = partsUsed;
        _areaAffect = areaAffect;
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
