using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public class Blowjob : Move {
    public Blowjob(){
        _name = "Blowjob";
        _dmg = new Dmg(12,0);
        _areaAffect = false;
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, IEnemy[] targetedEnemies){
        //TODO check if has happened before
        string text = "you get down on your knees and begin sucking on the " + targetedEnemies[0].GetName()+"'s cock";
        return text;
    }
    public override string GetName(IBodyPart bodyPart){
        if(bodyPart.GetModifier() == BodyPartModifier.Cum){
            return "Snow-Job";
        }
        return _name;
    }
}
