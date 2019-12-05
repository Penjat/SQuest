using System.Collections;
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

        switch(battleNum){
            case 0:
            enemyNames = new string[]{"imp","imp"};
            battle = new Battle(enemyNames);
            _delegate.StartBattle(battle);
            break;

            case 1:
            enemyNames = new string[]{"imp","imp","imp"};
            battle = new Battle(enemyNames);
            _delegate.StartBattle(battle);
            break;
        }

    }
}
