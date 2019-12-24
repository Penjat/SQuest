using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : Move {
    public Scissor(){
        _name = "Scissor";
        _dmg = new Dmg(2,3);
        _areaAffect = false;
        _primaryType = MoveType.Vagina;
        _partsUsed = new HashSet<MoveType>{MoveType.Vagina};
        _partsTargeted = new List<TargetType>{TargetType.Vagina};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you and the " + targetedEnemies[0].GetName() + " begin scissoring your vaginas together";
        return text;
    }
}
