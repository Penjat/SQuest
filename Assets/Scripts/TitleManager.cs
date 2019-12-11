﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour{

    SubManagerDelegate _delegate;

    public ContentPage _contentPage;


    void Start() {
        //Find the main manager
        _delegate = GameObject.Find("MainManager").GetComponent<SubManagerDelegate>();
    }

    public void PressedStart(int battleNum){

        //Get the player and clear their moves
        Player player = _delegate.GetPlayer();
        player.ClearMoves();
        Battle battle = DemoBattleFactory.GetDemoBattle(player,battleNum);
        _delegate.StartBattle(battle);
    }

    public void PressedAbout(){
        TextAsset contentText = Resources.Load("PageContent/About") as TextAsset;
        _contentPage.SetText(contentText.ToString());
        _contentPage.Appear();
    }
    public void PressedContent(){
        TextAsset contentText = Resources.Load("PageContent/Content") as TextAsset;
        _contentPage.SetText(contentText.ToString());
        _contentPage.Appear();
    }
    public void PressedStory(){
        TextAsset contentText = Resources.Load("PageContent/Story") as TextAsset;
        _contentPage.SetText(contentText.ToString());
        _contentPage.Appear();
    }
    public void PressedDevelopment(){
        TextAsset contentText = Resources.Load("PageContent/Development") as TextAsset;
        _contentPage.SetText(contentText.ToString());
        _contentPage.Appear();
    }
    public void PressedFeedback(){
        //TODO open a page with a short survey

    }
    public void PressedMailing(){
        //TODO open a box to ask for an email address

        MailSender.SendEmail();
    }
    public void PressedPatreon(){
        //TODO change to patreon link
        Application.OpenURL("https://discordapp.com/channels/653338630219235377/653338630219235380");
    }
    public void PressedDiscord(){
        Application.OpenURL("https://discordapp.com/channels/653338630219235377/653338630219235380");
    }


}
