using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : IBodyPart {
    private MoveType _moveType;

    public BodyPart(MoveType moveType){
        _moveType = moveType;
    }
    public MoveType GetMoveType(){
        return _moveType;
    }
    public BodyPartModifier GetModifier(){
        return BodyPartModifier.Cum;
    }
}

public interface IBodyPart {
    MoveType GetMoveType();
    BodyPartModifier GetModifier();
}
