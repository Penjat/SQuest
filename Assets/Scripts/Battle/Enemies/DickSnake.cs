using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DickSnake : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxClimax = 2.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Dick-Snake";
    }
}
