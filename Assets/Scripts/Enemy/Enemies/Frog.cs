using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxClimax = 6.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Frog";
    }
}
