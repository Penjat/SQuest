using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJob : Move {
    public HandJob(){
        _name = "HandJob";
        _dmg = 2;
        _areaAffect = false;
        _primaryType = MoveType.Hand;
        _partsUsed = new HashSet<MoveType>{MoveType.Hand};
    }
}
