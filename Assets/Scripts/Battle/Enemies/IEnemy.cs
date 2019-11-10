using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    void SetDelegate(EnemyDelegate enemyDelegate);
    void ClearSelf();
    string GetName();
    void Destroy();
}
