using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public abstract class Move {

    protected string _name;
    protected MoveType _primaryType;
    protected HashSet<MoveType> _partsUsed;
    protected List<TargetType> _partsTargeted;
    protected bool _areaAffect;
    protected Dmg _dmg;

    public Move(){

    }
    public bool CheckLocked(IDictionary<MoveType,Move> partsBeingUsed){
        //returns true if this move requires a part that is used already
        foreach(MoveType m in _partsUsed){
            if(partsBeingUsed.ContainsKey(m)){
                return true;
            }
        }
        return false;
    }
    public virtual string GetName(){
        return _name;
    }
    public virtual string GetText(Player player, IEnemy[] enemies){
        string text = "you use " + this.GetName() + " on the ";
        return text;
    }
    public virtual MoveType GetPrimaryType(){
        return _primaryType;
    }
    public HashSet<MoveType> GetPartsUsed(){
        return _partsUsed;
    }
    public List<TargetType> GetPartsTargeted(){
        return _partsTargeted;
    }
    public virtual bool IsAreaFX(){
        return _areaAffect;
    }
    public virtual Dmg GetDmg(){
            //TODO account for percent
            return _dmg;
    }
}
