using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a body part that can be targeted by a move
public class BodyTarget {

    private TargetType _targetType;
    private bool _isAvailable = true;
    private IBodyTargetDisplay _targetDisplay;
    // private BodyTargetState _state;

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
        //used initail to link the Image
        _targetDisplay = targetDisplay;
        _targetDisplay.SetUp(_targetType);//TODO later pass in more info
    }
    public void StartFlashing(){
        _targetDisplay.SetState(BodyTargetState.Flashing);
    }
    public void StopFlashing(){
        if(_isAvailable){
            _targetDisplay.SetState(BodyTargetState.Available);
            return;
        }
        _targetDisplay.SetState(BodyTargetState.Used);
    }
}

public enum TargetType {
    None, Penis, Vagina
};
public enum BodyTargetState {
    Flashing, Available, Used
};

public interface IBodyTargetDisplay{
    void SetState(BodyTargetState state);
    void SetUp(TargetType targetType);
}
