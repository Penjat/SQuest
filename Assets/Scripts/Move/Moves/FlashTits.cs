using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTits : Move {
    public FlashTits(){
        _name = "Flash Tits";
        _dmg = new Dmg(0,6);
        _areaAffect = false;
        _primaryType = MoveType.Breasts;
        _partsUsed = new HashSet<MoveType>{MoveType.Breasts};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
        _useType = UseType.OneTime;
    }
}
