using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour, BattleManagerDelegate, PlayerDelegate {

    private Player _player;
    public MenuManager _menuManager;
    public BattleManager _battleManager;

    void Start() {
        Debug.Log("Starting game");
        SetUpManagers();
        ToTitle();
    }

    private void SetUpManagers(){
        _player = new Player(this);
        _battleManager.SetUp(this);
    }

    public void ToTitle(){
        _menuManager.NavigateTo(MenuManager.TITLE);
    }

    public void StartBattle() {
        _menuManager.NavigateTo(MenuManager.BATTLE);
        Battle battle = new Battle();
        _battleManager.StartBattle(battle);
    }

    //----------------BattleManagerDelegate Methods--------------
    public void ExitBattle(){
        _menuManager.NavigateTo(MenuManager.TITLE);
    }
    public Player GetPlayer(){
        return _player;
    }
    //----------------Player Delegate methods--------------------
    public void HealthIsZero(){
        Debug.Log("player health is zero, should do something");
    }
}
