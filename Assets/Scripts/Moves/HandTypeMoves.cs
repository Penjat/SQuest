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
        _partsTargeted = new HashSet<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, IEnemy[] targetedEnemies){
        string text = "you grab the " + targetedEnemies[0].GetName()+"'s hard cock and begin jerking";
        return text;
    }
}
