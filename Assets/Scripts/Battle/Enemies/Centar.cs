using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centar : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxClimax = 20.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Centar";
    }
}
