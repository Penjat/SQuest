using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallow : Move {
    public Swallow(){
        _name = "Swallow";
        _dmg = new Dmg(0,0);
        _areaAffect = false;
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
        _isSpecial = true;s
    }
    public override string GetText(Player player, IEnemy[] targetedEnemies){
        string text = "you and the " + targetedEnemies[0].GetName() + " begin scissoring your vaginas together";
        return text;
    }
    public override bool ShouldAppear(IBodyPart bodyPart){
        //checks the primary body to see if the move should appear
        //only show if mouth is creampied
        return bodyPart.GetModifier() == BodyPartModifier.Cum;
    }
}
