using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistedBJ : Move {
    public AssistedBJ(){
        _name = "Assisted Blowjob";
        _dmg = new Dmg(2,3);
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth,MoveType.Hand};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you use your hands and mouth to pleasure the " + targetedEnemies[0].GetName();
        return text;
    }
}
