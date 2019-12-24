using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextFactory {
    //returns text describing the battle
    //keeps track of what has happened

    public BattleTextFactory(){

    }

    public string GetText(Player player, IDictionary<PlayerAction,ITarget[]> actions){

        if(actions.Count == 0){
            return "you stand there and do nothing...";
        }
        string battleText = "";
        int i=0;
        foreach(KeyValuePair<PlayerAction,ITarget[]> action in actions){
            Move move = action.Key.GetMove();
            ITarget[] targets = action.Value;
            battleText += move.GetText(player,targets);
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
