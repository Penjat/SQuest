using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxArousal = 4.0f;
        _curArousal = 4.0f;
        
        _maxClimax = 8.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Imp";
    }
}
