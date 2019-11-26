using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEvent {
    int _duration;
    int _numNotes;
    int _beatPos;//position in 16th notes
    public BeatEvent(int duration, int numNotes, int beatPos){
        _duration = duration;
        _numNotes = numNotes;
        _beatPos = beatPos;
    }
    public int GetDuration(){
        return _duration;
    }
    public int GetNumNotes(){
        return _numNotes;
    }
    public int GetBeatPos(){
        return _beatPos;
    }
}
