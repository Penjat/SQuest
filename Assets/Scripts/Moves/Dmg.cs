using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Dmg {
    private int _climax;
    private int _arousal;

    public Dmg(int climax, int arousal){
        _climax = climax;
        _arousal = arousal;
    }

    public Dmg AddTo(Dmg dmg){
        int climax = this._climax + dmg._climax;
        int arousal = this._arousal + dmg._arousal;
        return new Dmg(climax, arousal);
    }

    public static Dmg Zero(){
        return new Dmg(0,0);
    }
    public int GetClimax(){
        return _climax;
    }
    public int GetArousal(){
        return _arousal;
    }
}
