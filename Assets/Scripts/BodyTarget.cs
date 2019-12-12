using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a body part that can be targeted by a move
public class BodyTarget {

    private TargetType _targetType;
    private bool _isAvailable = false;

    public BodyTarget(TargetType targetType){
        _targetType = targetType;
    }
    public TargetType GetTargetType(){
        return _targetType;
    }
    public bool IsAvailable(){
        return _isAvailable;
    }
}

public enum TargetType {
    Penis, Vagina
};
