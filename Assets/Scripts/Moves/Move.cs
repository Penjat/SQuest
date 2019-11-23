using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move {

    public string _name;
    public MoveType _primaryType;
    public HashSet<MoveType> _partsUsed;
    public bool _areaAffect;
    public int _dmg;

    public Move(){

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
