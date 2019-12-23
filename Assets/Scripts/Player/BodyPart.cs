using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : IBodyPart {

    private MoveType _moveType;
    private BodyPartModifier _modifier = BodyPartModifier.None;

    public BodyPart(MoveType moveType){
        _moveType = moveType;
    }
    public MoveType GetMoveType(){
        return _moveType;
    }
    public BodyPartModifier GetModifier(){
        return _modifier;
    }
    public void SetModifier(BodyPartModifier modifier){
        _modifier = modifier;
    }
}

public interface IBodyPart {
    MoveType GetMoveType();
    BodyPartModifier GetModifier();
    void SetModifier(BodyPartModifier modifier);
}
