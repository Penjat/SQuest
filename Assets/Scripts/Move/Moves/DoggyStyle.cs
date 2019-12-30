using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggyStyle : Move {
    public DoggyStyle(){
        _name = "Doggy Style";
        _dmg = new Dmg(2,3);
        _primaryType = MoveType.Ass;
        _partsUsed = new HashSet<MoveType>{MoveType.Ass,MoveType.Hand};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you get on all fours and take it from behind";
        return text;
    }
}
