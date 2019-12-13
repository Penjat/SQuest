﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    void SetUp(EnemyDelegate enemyDelegate);
    void ClearSelf();
    string GetName();
    void Destroy();
    void DoDmg(float dmg);
    void SetTargeted(bool b);
    void SetDelay(double delay);
    void SetState(SelectState state);
    void ResolveDMG();
    void AddToDmg(int dmg);
    void TakeTurn();
    void UseMove(Move move, float result);
    void TargetWith(Move move);
    void UnTarget(Move move);
    void ClearTargets();
    void TargetWithMove(Move move);
    void StopFlashingParts();
    TargetResult CanTarget(Move move);
}
