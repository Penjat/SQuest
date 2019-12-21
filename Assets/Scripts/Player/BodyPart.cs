using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : IBodyPart {
    private TargetType _targetType;
    public BodyPart(TargetType targetType){
        _targetType = targetType;
    }
    public TargetType GetType(){
        return targetType;
    }
}

public interface IBodyPart {
    TargetType GetType();
}
