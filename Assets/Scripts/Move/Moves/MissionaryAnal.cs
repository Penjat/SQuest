using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionaryAnal : Move {
    public MissionaryAnal(){
        _name = "Missionary Anal";
        _dmg = new Dmg(2,3);
        _areaAffect = false;
        _primaryType = MoveType.Ass;
        _partsUsed = new HashSet<MoveType>{MoveType.Ass};
        _partsTargeted = new List<TargetType>{TargetType.Penis};
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "the " + targetedEnemies[0].GetName() + " begins fucking you up the ass";
        return text;
    }
}
