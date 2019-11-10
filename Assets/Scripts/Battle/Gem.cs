using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour {
    public RectTransform _rect;
    private float _size = 60.0f;
    private float _goal;

    private bool _wasCleared = false;
    private float _accuracy;

    public bool CheckMissed(float pos){
        //check if has moved too far
        return (_goal + 40.0f + pos < 0.0f);//adding half the size of the button
    }

    public void SetPosition(float pos){
        _rect.sizeDelta = new Vector2(_size,_size);
        _goal = pos;
        _rect.anchoredPosition = new Vector2(0.0f,pos);
    }
    public void Clear(float trackPos){
        _wasCleared = true;
        gameObject.SetActive(false);
        _accuracy = _goal + trackPos;
        Debug.Log("cleared at " + _accuracy);
    }
}
