using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a body part that can be targeted by a move
public class BodyTarget {

    private TargetType _targetType;
    public BodyTarget(TargetType targetType){
        _targetType = targetType;
    }
    public TargetType GetTargetType(){
        return _targetType;
    }
}

public enum TargetType {
    Penis, Vagina
};
