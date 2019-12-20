using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWinBattle : BattleMenu {

    public void PressedDone(){
        _delegate.SendMSG(BattleMSG.WIN);
    }
}
