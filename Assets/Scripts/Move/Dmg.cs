using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Dmg {
    private int _climax;
    private int _arousal;
    private HashSet<Fetish> _fetishes;

    public Dmg(int climax, int arousal){
        _climax = climax;
        _arousal = arousal;
        _fetishes = new HashSet<Fetish>();

    }
    public void AddFetish(Fetish fetish){
        _fetishes.Add(fetish);
    }

    public Dmg AddTo(Dmg dmg){
        int climax = this._climax + dmg._climax;
        int arousal = this._arousal + dmg._arousal;
        return new Dmg(climax, arousal);
    }
    public Dmg TimesBy(float amt){
        float climax = ((float)this._climax)*amt;
        float arousal =((float)this._arousal)*amt;
        return new Dmg((int)climax, (int)arousal);
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
    public bool CheckForFetish(Fetish fetish){
        return _fetishes.Contains(fetish);
    }
    public Dmg ApplyParts(List<IBodyPart> bodyParts){
        foreach(IBodyPart bodyPart in bodyParts){
            if(bodyPart.GetModifier() == BodyPartModifier.Cum){
                this.AddFetish(Fetish.Cum);
            }
        }
        return this;
    }
}
