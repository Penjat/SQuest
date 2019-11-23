using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum FillDir{
    UP,DOWN
};

public class StatusBar : MonoBehaviour {

    StatusBarDelegate _delegate;

    public RectTransform _topBar;
    public RectTransform _bottomBar;

    private FillDir _fillDir;

    float _maxValue;
    float _curValue = 0.0f;
    float _fillRate;
    float _toValue;
    bool _isChanging;

    void Update(){
        //Fill the meter
        if(_isChanging){
            switch(_fillDir){
                //filling UP
                case FillDir.UP:
                _curValue += _fillRate*Time.deltaTime;
                if(_curValue >= _toValue){
                    DoneFilling();
                    return;
                }
                break;

                //filling done
                case FillDir.DOWN:
                _curValue -= _fillRate*Time.deltaTime;
                if(_curValue <= _toValue){
                    DoneFilling();
                    return;
                }
                break;
            }
            SetValue(_curValue);
        }

    }
    public void SetUp(StatusBarDelegate statusBarDelegate, float maxValue, float fillRate = 2.0f){
        _delegate = statusBarDelegate;
        _maxValue = maxValue;
        _fillRate = fillRate;
    }
    public void SetValueAnimated(int value){
        SetValueAnimated((float)value);
    }
    public void SetValueAnimated(float value){
        Debug.Log("starting. value = " + value + " , curValue = " + _curValue);
        _toValue = value;
        if(_toValue > _maxValue){
            _toValue = _maxValue;
        }else if(_toValue < 0){
            _toValue = 0;
        }

        _isChanging = true;
        if(_toValue > _curValue){
            //Should fill up
            Debug.Log("should go up");
            _fillDir = FillDir.UP;
        }else if(_toValue < _curValue){
            //should fill down
            Debug.Log("should go down");
            _fillDir = FillDir.DOWN;
        }else{
            //does not need to fill
            DoneFilling();
        }

    }
    public void SetValue(int value){
        SetValue((float)value);
    }
    public void SetValue(float value){
        //don't divide by 0
        if(_maxValue == 0.0f){
            return;
        }
        _curValue = value;
        float ratio = _curValue/_maxValue;

        //don't go past 100%
        if(ratio > 1){
            ratio = 1;
        }
        _topBar.localScale = new Vector2(ratio,1.0f);
    }
    private void DoneFilling(){
        Debug.Log("Done");
        _isChanging = false;
        _curValue = _toValue;

        //optional Delegate
        if(_delegate != null){
            _delegate.DoneFilling();
        }
    }
}

public interface StatusBarDelegate{
    //TODO pass in int for orginization
    void DoneFilling();
}
