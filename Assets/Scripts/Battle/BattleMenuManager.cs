using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuManager : MonoBehaviour, BattleMenuDelegate {

    private BattleMenuManagerDelegate _delegate;

    public MenuWinBattle _menuWinBattle;
    public MenuLoseBattle _menuLoseBattle;

    public void SetUp(BattleMenuManagerDelegate battleMenuManagerDelegate){
        Debug.Log("setting up battle menu manager");
        _delegate = battleMenuManagerDelegate;

        //set the delegates
        _menuWinBattle.SetUp(this);
        _menuLoseBattle.SetUp(this);

        //hide the menus
        _menuWinBattle.SetActive(false);
        _menuLoseBattle.SetActive(false);
    }
    public void ShowWinScreen(){
        _menuWinBattle.SetActive(true);
        _menuWinBattle.SetUp(this);
    }
    public void ShowLoseScreen(){
        _menuLoseBattle.DisplayMenu("you pass out");
    }

    //--------------Delegate methods---------------
    public void SendMSG(BattleMSG msg){
        switch(msg){
            case BattleMSG.WIN:
            _delegate.ExitBattle();
            break;
            case BattleMSG.LOSE:
            _delegate.ExitBattle();
            break;
        }
    }
}

public interface BattleMenuManagerDelegate{
    //TODO pass in win or lose
    void ExitBattle();
}
