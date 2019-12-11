using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StringMethods;
using System;

public class DickSnake : Enemy {
    public override void SetUp(EnemyDelegate enemyDelegate){
        _maxClimax = 2.0f;
        //set stats before base setup
        base.SetUp(enemyDelegate);
    }
    public override string GetName(){
        return "Dick-Snake";
    }
    public override void TakeTurn(){
        Debug.Log("taking my turn");
        //Dont attack if was targeted last turn
        if(_usedMoves.Count > 0){
            DoneTurn();
            return;
        }
        _delegate.EnemyMsg("The " + GetName().ColorFor(Entity.ENEMY) + " begins jerking itself off...");
        _dmgToDo = 2;
         Action done = DoneTurn;
         StartCoroutine(WaitFor(1.0f, done) );
    }
}
