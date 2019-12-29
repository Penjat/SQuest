using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction {
    Move _move;
    List<IBodyPart> _usedParts;
    public PlayerAction(Move move, List<IBodyPart> usedParts){
        _move = move;
        _usedParts = usedParts;
    }
    public Move GetMove(){
        return _move;
    }
    public List<IBodyPart> GetParts(){
        return _usedParts;
    }
    public void ApplySpecialEffects(ITarget target){
        _move.ApplyEffects(target, _usedParts);
    }
}
