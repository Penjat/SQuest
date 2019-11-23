using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowjob : Move {
    public Blowjob(){
        _name = "Blowjob";
        _dmg = 8;
        _areaAffect = false;
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth};
    }
}
