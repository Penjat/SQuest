using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEvent {
    int _duration;
    int _numNotes;
    public BeatEvent(int duration, int numNotes){
        _duration = duration;
        _numNotes = numNotes;
    }
    public int GetDuration(){
        return _duration;
    }
    public int GetNumNotes(){
        return _numNotes;
    }
}
