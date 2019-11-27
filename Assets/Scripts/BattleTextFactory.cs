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
        int i=0;
        foreach(KeyValuePair<Move,IEnemy[]> action in actions){
            Move move = action.Key;
            IEnemy[] targetedEnemies = action.Value;
            battleText += move.GetText(player,targetedEnemies);
            if(i<actions.Count-1){
                battleText += "\n at the same time, ";
            }else{
                battleText += "...";
            }
            i++;
        }
        return battleText;
    }

}
