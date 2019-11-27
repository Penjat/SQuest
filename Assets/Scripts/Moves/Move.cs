using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;

public abstract class Move {

    protected string _name;
    protected MoveType _primaryType;
    protected HashSet<MoveType> _partsUsed;
    protected bool _areaAffect;
    protected int _dmg;

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
    public virtual HashSet<MoveType> GetPartsUsed(){
        return _partsUsed;
    }
    public virtual bool IsAreaFX(){
        return _areaAffect;
    }
    public virtual int GetDmg(){
        return _dmg;
    }
}
