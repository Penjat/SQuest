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
}
