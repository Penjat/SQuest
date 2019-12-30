using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Swallow : Move {
    public Swallow(){
        _name = "Swallow";
        _dmg = new Dmg(0,2);
        _primaryType = MoveType.Mouth;
        _partsUsed = new HashSet<MoveType>{MoveType.Mouth};
        _partsTargeted = new List<TargetType>{};
        _isSpecial = true;
        _selectType = SelectType.PlayerOnly;
    }
    public override string GetText(Player player, ITarget[] targetedEnemies){
        string text = "you and the " + targetedEnemies[0].GetName() + " begin scissoring your vaginas together";
        return text;
    }
    public override bool ShouldAppear(IBodyPart bodyPart){
        //checks the primary body to see if the move should appear
        //only show if mouth is creampied
        return bodyPart.GetModifier() == BodyPartModifier.Cum;
    }
    public override void ApplyEffects(ITarget target, List<IBodyPart> bodyPartsUsed){
        IBodyPart mouth = bodyPartsUsed.First(x => x.GetMoveType() == MoveType.Mouth && x.GetModifier() == BodyPartModifier.Cum);
        mouth.SetModifier(BodyPartModifier.None);
    }
}
