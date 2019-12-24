using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryVaginal : Move {
    public MissionaryVaginal(){
        _name = "Missionary Vaginal";
        _dmg = new Dmg(2,3);
        _areaAffect = false;
        _primaryType = MoveType.Vagina;
        _partsUsed = new HashSet<MoveType>{MoveType.Vagina};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "the " + targetedEnemies[0].GetName() + " begins fucking you in your pussy";
        return text;
    }
}
