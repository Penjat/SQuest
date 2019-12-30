using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittyFuck : Move {
    public TittyFuck(){
        _name = "Titty Fuck";
        _dmg = new Dmg(2,3);
        _primaryType = MoveType.Breasts;
        _partsUsed = new HashSet<MoveType>{MoveType.Breasts,MoveType.Hand};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you use your" + player.BreastDescription() + " to pleasure the " + targetedEnemies[0].GetName()+"'s stiff prick";
        return text;
    }
}
