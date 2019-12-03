using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoseBattle : MonoBehaviour, TextTyperDelegate {
    public TextTyper _textTyper;

    public void DisplayMenu(string loseText){
        //TODO use animation
        gameObject.SetActive(true);
        _textTyper.StartTyping(this, loseText, 0.4f, 1.5f);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
    //--------------Delegate Methiods---------------
    public void DoneTyping(){
        Debug.Log("done typing");
    }

}
