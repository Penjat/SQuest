using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour, SubManagerDelegate, PlayerDelegate {

    private MainManagerDelegate _subManager;
    private Player _player;

    void Start() {
        Debug.Log("Starting game");
        DontDestroyOnLoad(this.gameObject);
        SetUpManagers();
        // ToTitle();
    }

    private void SetUpManagers(){
        _player = new Player(this);
        // _battleManager.SetUp(this);
    }



    //----------------SubManagerDelegate Methods--------------
    public void ExitBattle(){
        // _menuManager.NavigateTo(MenuManager.TITLE);
        SceneManager.LoadScene("Title");
    }
    public Player GetPlayer(){
        return _player;
    }
    public void SetSubManager(MainManagerDelegate subManager){
        _subManager = subManager;
    }
    public void StartBattle() {
        // _menuManager.NavigateTo(MenuManager.BATTLE);
        //Battle battle = new Battle();
        // _battleManager.StartBattle(battle);
        SceneManager.LoadScene("Battle");
    }
    //----------------Player Delegate methods--------------------
    public void HealthIsZero(){
        Debug.Log("player health is zero, should do something");
        if(_subManager != null){
            _subManager.PlayerDeath();
        }
    }
}

//the delegate for whatever manager is currently in charge
public interface MainManagerDelegate{
    //informs the manager of the player's death, logic depends on what manager
    void PlayerDeath();
}

//used by the sub managers to access main manager
public interface SubManagerDelegate{
    //TODO pass in data about battle results
    void ExitBattle();
    Player GetPlayer();
    void StartBattle();
    void SetSubManager(MainManagerDelegate subManager);
}
