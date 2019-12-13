﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a body part that can be targeted by a move
public class BodyTarget {

    private TargetType _targetType;
    private bool _isAvailable = true;
    private IBodyTargetDisplay _targetDisplay;
    private BodyTargetState _state;

    public BodyTarget(TargetType targetType){
        _targetType = targetType;
    }
    public TargetType GetTargetType(){
        return _targetType;
    }
    public bool IsAvailable(){
        return _isAvailable;
    }
    public void SetIsAvailble(bool isAvailable){
        _isAvailable = isAvailable;
    }
    public void SetDisplay(IBodyTargetDisplay targetDisplay){
        _targetDisplay = targetDisplay;
    }
}

public enum TargetType {
    Penis, Vagina
};
public enum BodyTargetState {
    Flashing, Available, Targeted
};

public interface IBodyTargetDisplay{
    void SetState(BodyTargetState state);
}
