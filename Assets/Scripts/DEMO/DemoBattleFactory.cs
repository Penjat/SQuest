using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBattleFactory {
    public static Battle GetDemoBattle(Player player, int battleNum){

        Battle battle;
        string[] enemyNames;

        switch(battleNum){

            //------------Battle 1---(snake w/ no moves)
            case 0:
            //add the enemy data to battle
            enemyNames = new string[]{"DickSnake"};
            battle = new Battle(enemyNames);

            //TODO add sword attack
            //set up the player's moves
            // player.AddMove(new HandJob());

            return battle;

            //------------Battle 2---(Imp w/ handjob)
            case 1:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new HandJob());
            player.AddMove(new Blowjob());
            player.AddMove(new TittyFuck());
            player.AddMove(new AssistedBJ());
            player.AddMove(new DoggyStyle());
            player.AddMove(new MissionaryAnal());
            player.AddMove(new Scissor());

            return battle;

            //------------Battle 3---(Troll)
            case 2:
            //add the enemy data to battle
            enemyNames = new string[]{"Troll"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());
            //TODO add move for shake ass

            return battle;

            //------------Battle 4---(Imp and troll)
            case 3:
            //add the enemy data to battle
            enemyNames = new string[]{"Imp","Troll"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());
            player.AddMove(new HandJob());
            player.AddMove(new FlashTits());
            //TODO add move for shake ass

            return battle;

            //------------Battle 5---(Nice Guy)
            case 4:
            //add the enemy data to battle
            //TODO change to nice guy
            enemyNames = new string[]{"NiceGuy"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            // player.AddMove(new Blowjob());
            // player.AddMove(new HandJob());
            // player.AddMove(new FlashTits());

            return battle;

            //------------Battle 6---(Demons)
            case 5:
            //add the enemy data to battle
            //TODO add in demons
            enemyNames = new string[]{"Demon","Demon"};
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
