using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {
    int _duration;
    bool _isRest;
    int _beatPos;
    public Note(int duration, bool isRest, int beatPos){
        _duration = duration;
        _isRest = isRest;
        _beatPos = beatPos;
    }

    public int GetDuration(){
        return _duration;
    }
    public bool IsRest(){
        return _isRest;
    }
    public int GetBeatPos(){
        return _beatPos;
    }
}
