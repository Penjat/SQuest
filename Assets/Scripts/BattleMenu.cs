using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour {
    
    protected BattleMenuDelegate _delegate;
    public void SetUp(BattleMenuDelegate menuDelegate){
        _delegate = menuDelegate;
    }
    public virtual void SetActive(bool isActive){
        gameObject.SetActive(isActive);
    }
}


public interface BattleMenuDelegate{
    void SendMSG(BattleMSG msg);
}

public enum BattleMSG{
    LOSE, WIN
};
