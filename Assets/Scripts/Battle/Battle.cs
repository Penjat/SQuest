using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle {
    private string[] _enemyNames;

    public Battle(string[] enemyNames){
        _enemyNames = enemyNames;
    }
    public string[] GetEnemies(){
        return _enemyNames;
    }
}
