using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleDisplay : MonoBehaviour, ITarget, StatusBarDelegate {

    PlayerBattleDisplayDelegate _delegate;
    private List<Dmg> _dmgToDo = new List<Dmg>();
    private List<DmgWithType> _dmgList = new List<DmgWithType>();

    public DMGLabel _dmgLabel;

    private StatusBar _playerHealthBar;
    private StatusBar _playerClimaxBar;
    private StatusBar _playerArousalBar;

    public void SetUp(PlayerBattleDisplayDelegate playerBattleDisplayDelegate){
        _delegate = playerBattleDisplayDelegate;

        StatusBar playerHealthBar = GameObject.Find("Player Health").GetComponent<StatusBar>();
        StatusBar playerClimaxBar = GameObject.Find("Player Climax").GetComponent<StatusBar>();
        StatusBar playerArousalBar = GameObject.Find("Player Arousal").GetComponent<StatusBar>();
        Player player = _delegate.GetPlayer();

        _playerHealthBar = playerHealthBar;
        _playerHealthBar.SetUp(this,player.GetMaxHealth(),50.0f);
        _playerHealthBar.SetValue(player.GetCurHealth());

        _playerClimaxBar = playerClimaxBar;
        _playerClimaxBar.SetUp(this,player.GetMaxClimax(),50.0f);
        _playerClimaxBar.SetValue(player.GetCurClimax());

        _playerArousalBar = playerArousalBar;
        _playerArousalBar.SetUp(this,player.GetMaxArousal(),50.0f);
        _playerArousalBar.SetValue(player.GetCurArousal());
    }

    public void OverPlayer(){
        _delegate.OverPlayer(this);
    }
    public void ExitPlayer(){
        _delegate.ExitPlayer(this);
    }
    public void PlayerPressed(){
        _delegate.PressedPlayer(this);
    }
    public void ResolveDMG(){
        //keep track of which bars are still waiting for

        //adds all the dmg done that round together
        if(_dmgToDo.Count == 0){
            _delegate.PlayerDoneResolving();
            return;
        }
        foreach(Dmg dmg in _dmgToDo){
            DoDmg(dmg);
        }
    }
    public void DoDmg(Dmg dmg){
        Debug.Log("doing dmg --------------------------");
        Player player = _delegate.GetPlayer();

        //TODO check fetishes and modify Dmg
        Dmg modDmg = CheckForFetish(dmg);

        player.AddToCurClimax(modDmg.GetClimax());
        player.AddToCurArousal(modDmg.GetArousal());

        _playerClimaxBar.SetValueAnimated(player.GetCurClimax());
        _playerArousalBar.SetValueAnimated(player.GetCurArousal());

        if(dmg.GetClimax() != 0){
            ShowDmg(modDmg.GetClimax(), DmgType.Climax);
        }
        if(dmg.GetArousal() != 0){
            ShowDmg(modDmg.GetArousal(), DmgType.Arousal);
        }
    }
    private void ShowDmg(int dmg, DmgType dmgType=DmgType.Climax){
        //TODO pass in color or icons
        DmgWithType newDmg = new DmgWithType(dmg, dmgType);
        if(_dmgList.Count > 0){
            _dmgList.Add(newDmg);
            return;
        }
        _dmgList.Add(newDmg);
        _dmgLabel.ShowDmg(newDmg);
        StartCoroutine(CheckNextDmg());
    }
    private IEnumerator CheckNextDmg(){
        yield return new WaitForSeconds(2.0f);
        NextDmg();
    }
    private void NextDmg(){
        //remove the one we just showed
        _dmgList.RemoveAt(0);
        //check if still dmg left to show
        if(_dmgList.Count > 0){
            _dmgLabel.ShowDmg(_dmgList[0]);
            StartCoroutine(CheckNextDmg());
            return;
        }
        DoneResolving();
    }
    private void DoneResolving(){
        _dmgToDo.Clear();
        _delegate.PlayerDoneResolving();
    }
    //-------------------ITarget Methods---------------
    public void UnTarget(Move move){

    }
    public void AddToDmg(Dmg dmg){
        Debug.Log("adding to player dmg");
        _dmgToDo.Add(dmg);
    }
    public void UseMove(Move move, float result){
        Debug.Log("using the move " + move.GetName() + " on the player");
    }
    public string GetName(){
        return "Player";
    }
    public void SetTargeted(bool b, Move selectedMove=null){

    }
    public void TargetWithMove(Move move){

    }
    public void TargetWith(Move move){

    }
    public TargetResult CanTarget(Move move){
        return TargetResult.Available;
    }
    public void SetState(SelectState state){

    }
    public void StopFlashingParts(){

    }
    private Dmg CheckForFetish(Dmg dmg){
        //TODO check the player
        //default to the regualar dmg
        Dmg modDmg = dmg;
        //checks the dmg to see if it matches fetish
        if(dmg.CheckForFetish(Fetish.Cum)){
            modDmg = dmg.TimesBy(2.0f);
        }
        return modDmg;
    }

    //---------------StatusBar Delegate-----------------
    public void DoneFilling(int refNumber){

    }
}

public interface PlayerBattleDisplayDelegate{
    void OverPlayer(ITarget target);
    void ExitPlayer(ITarget target);
    void PressedPlayer(ITarget target);
    void PlayerDoneResolving();
    Player GetPlayer();
}
