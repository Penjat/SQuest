using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBattleFactory {
    public static Battle GetDemoBattle(Player player, int battleNum){

        Battle battle;
        string[] enemyNames;

        switch(battleNum){
            case 0:
            //add the enemy data to battle
            enemyNames = new string[]{"DickSnake"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new HandJob());

            return battle;

            case 1:
            //add the enemy data to battle
            enemyNames = new string[]{"Frog","Harpy","Frog"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;

            case 2:
            //add the enemy data to battle
            enemyNames = new string[]{"Harpy"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;

            case 3:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp","Harpy","Troll"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;

            case 4:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp","Harpy","Troll"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;

            case 5:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp","Harpy"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;

            default:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp","Imp","Imp"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());

            return battle;
        }
    }
}
