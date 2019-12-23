using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextFactory {
    //returns text describing the battle
    //keeps track of what has happened

    public BattleTextFactory(){

    }

    public string GetText(Player player, IDictionary<PlayerAction,IEnemy[]> actions){

        if(actions.Count == 0){
            return "you stand there and do nothing...";
        }
        string battleText = "";
        int i=0;
        foreach(KeyValuePair<PlayerAction,IEnemy[]> action in actions){
            Move move = action.Key.GetMove();
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
