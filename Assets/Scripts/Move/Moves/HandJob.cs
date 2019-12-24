using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJob : Move {
    public HandJob(){
        _name = "HandJob";
        _dmg = new Dmg(2,3);
        _areaAffect = false;
        _primaryType = MoveType.Hand;
        _partsUsed = new HashSet<MoveType>{MoveType.Hand};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you grab the " + targetedEnemies[0].GetName()+"'s hard cock and begin jerking";
        return text;
    }
}
