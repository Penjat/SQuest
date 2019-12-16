using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Dmg {
    public int _climax;
    public int _arousal;

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
}
