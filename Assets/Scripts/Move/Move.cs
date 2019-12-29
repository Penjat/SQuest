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
    protected bool _isSpecial = false;

    public Move(){

    }
    public bool CheckLocked(List<MoveType> partsAvailable){
        //returns true if it cannot find the parts it needs

        //create a new list so as not to affect the old one
        List<MoveType> available = new List<MoveType>(partsAvailable);
        foreach(MoveType m in _partsUsed){
            if(!available.Contains(m)){
                return true;
            }else{
                //remove it so is not counted twice
                available.Remove(m);
            }
        }
        //all the parts were found
        return false;
    }
    public bool GetIsSpecial(){
        return _isSpecial;
    }
    public virtual string GetName(){
        return _name;
    }
    public virtual string GetName(IBodyPart bodyPart){
        return _name;
    }
    public virtual string GetText(Player player, ITarget[] enemies){
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
    public virtual bool ShouldAppear(IBodyPart bodyPart){
        //checks the primary body to see if the move should appear
        return true;
    }
    public virtual void ApplyEffects(ITarget target, List<IBodyPart> bodyPartsUsed){
        //Do nothing by default
    }
}
