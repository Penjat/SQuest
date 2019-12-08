using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMSG : MonoBehaviour {

    public void GoTOGame(){
        //TODO animate out
        Destroy(gameObject);
    }
    public void ExitGame(){
        //TODO close the app
    }
}
