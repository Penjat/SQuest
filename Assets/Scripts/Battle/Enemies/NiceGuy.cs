using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiceGuy : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxClimax = 12.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Nice Guy";
    }
}
