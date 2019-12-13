using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public class Blowjob : Move {
    public Blowjob(){
        _name = "Blowjob";
        _dmg = 8;
        _areaAffect = false;
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth};
        _partsTargeted = new List<TargetType>{TargetType.Penis,TargetType.Penis};
    }
    public override string GetText(Player player, IEnemy[] targetedEnemies){
        //TODO check if has happened before
        string text = "you get down on your knees and begin sucking on the " + targetedEnemies[0].GetName()+"'s cock";
        return text;
    }
}
