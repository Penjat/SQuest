using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextFactory {
    //returns text describing the battle
    //keeps track of what has happened

    public BattleTextFactory(){

    }

    public string GetText(Player player, IDictionary<Move,IEnemy[]> actions){
        string battleText = "";
        foreach(KeyValuePair<Move,IEnemy[]> action in actions){
            Move move = action.Key;
            IEnemy[] targetedEnemies = action.Value;
            battleText += move.GetName(); 
        }
        return battleText;
    }

}
