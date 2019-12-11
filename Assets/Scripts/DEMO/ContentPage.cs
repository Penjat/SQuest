using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentPage : MonoBehaviour {

    public Text _displayText;
    public GameObject _scrollView;
    public GameObject _survey;
    public GameObject _mailing;

    public void Appear(){
        //TODO do with animation
        gameObject.SetActive(true);
    }
    public void Close(){
        //TODO do with animation
        gameObject.SetActive(false);
    }
    public void SetText(string contentText){
        //sets the rich text to be displayed
        _displayText.text = contentText;
    }
}
