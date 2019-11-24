﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {
    int _duration;
    bool _isRest;
    public Note(int duration, bool isRest){
        _duration = duration;
        _isRest = isRest;
    }

    public int GetDuration(){
        return _duration;
    }
    public bool IsRest(){
        return _isRest;
    }
}
