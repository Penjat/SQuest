﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour{

    SubManagerDelegate _delegate;

    void Start() {
        //Find the main manager
        _delegate = GameObject.Find("MainManager").GetComponent<SubManagerDelegate>();
    }

    public void PressedStart(int battleNum){

        string[] enemyNames;
        Battle battle;
        Player player = _delegate.GetPlayer();
        player.ClearMoves();

        switch(battleNum){
            case 0:
            //add the enemy data to battle
            enemyNames = new string[]{"imp","imp"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new HandJob());

            //start the battle
            _delegate.StartBattle(battle);
            break;

            case 1:
            //add the enemy data to battle
            enemyNames = new string[]{"imp","imp","imp"};
            battle = new Battle(enemyNames);

            //set up the player's moves
            player.AddMove(new Blowjob());

            _delegate.StartBattle(battle);
            break;
        }

    }
}
