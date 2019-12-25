using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget {
    void UnTarget(Move move);
    void AddToDmg(Dmg dmg);
    void UseMove(Move move, float result);
    string GetName();
    void SetTargeted(bool b, Move selectedMove=null);
    void TargetWithMove(Move move);
    void TargetWith(Move move);
    TargetResult CanTarget(Move move);
    void SetState(SelectState state);
    void StopFlashingParts();
}
