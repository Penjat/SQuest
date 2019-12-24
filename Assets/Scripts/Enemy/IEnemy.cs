using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : ITarget {
    void SetUp(EnemyDelegate enemyDelegate);
    void ClearSelf();
    string GetName();
    void Destroy();
    void DoDmg(Dmg dmg);
    
    void SetDelay(double delay);
    void SetState(SelectState state);
    void ResolveDMG();

    void TakeTurn();
    void UseMove(Move move, float result);
    void TargetWith(Move move);

    void ClearTargets();
    void TargetWithMove(Move move);
    void StopFlashingParts();
    TargetResult CanTarget(Move move);
    MoveType GetMoveTypeClimax();
}
