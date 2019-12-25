using UnityEngine;
using UnityEngine.UI;
using StringMethods;

public class InfoLabelManager : MonoBehaviour{
    public TextTyper _infoLabel;

    private PlayerActionManager _playerActionManager;

    public void SetUp(PlayerActionManager playerActionManager){
        _playerActionManager = playerActionManager;
    }

    //-------------------------Logic Methods-------------------------
    public void HideTargets(){
        CheckState();
    }
    public void MoveSelected(){
        CheckState();
    }
    public void OverCategory(){

    }
    public void BlockedEnemy(IEnemy enemy){
        if(_playerActionManager.IsSelectingTarget()){
            ShowBlockedEnemy(enemy);
        }
    }
    public void MoveNotMatch(IEnemy enemy, Move move){
        if(_playerActionManager.IsSelectingTarget()){
            ShowMoveNotMatch(enemy, move);
        }
    }
    public void OverEnemy(IEnemy enemy){
        //TODO check if can target
        if(_playerActionManager.IsSelectingTarget()){
            ShowTargetEnemy(enemy);
        }
    }
    public void ExitEnemy(){
        CheckState();
    }
    public void CheckState(){
        if(_playerActionManager.IsSelectingTarget()){
            ShowMoveNoTarget();
            return;
        }
        ShowBlank();
    }
    public void EndTurn(){
        ShowBlank();
    }
    public void OverPlayer(){
        if(_playerActionManager.IsSelectingTarget()){
            Move move = _playerActionManager.GetCurMove();
            _infoLabel.SetText("use " + move.GetName().ColorFor(Entity.MOVE) + " on " + "yourself".ColorFor(Entity.PLAYER));
        }
    }


    //---------------------Show Methods-------------------------------
    public void ShowBlank(){
        _infoLabel.SetText("");
    }
    public void ShowTargetsForMove(IBodyPart bodyPart, Move move){

        ITarget[] targeted = _playerActionManager.GetTargetsFor(bodyPart);
        string moveName = move.GetName().ColorFor(Entity.MOVE);
        string enemyName = targeted[0].GetName().ColorFor(Entity.ENEMY);
        _infoLabel.SetText("using " + moveName + " on " + enemyName);
    }
    public void ShowTargetEnemy(IEnemy enemy){
        string moveName = _playerActionManager.GetCurMove().GetName().ColorFor(Entity.MOVE);
        string enemyName = enemy.GetName().ColorFor(Entity.ENEMY);
        _infoLabel.SetText("use " + moveName + " on " + enemyName);
    }
    public void ShowBlockedEnemy(IEnemy enemy){
        //string moveName = _playerActionManager.GetCurMove().GetName().ColorFor(Entity.MOVE);
        //TODO get targeted move
        string enemyName = enemy.GetName().ColorFor(Entity.ENEMY);
        _infoLabel.SetText(enemyName + " is already targeted.");
    }
    public void ShowMoveNotMatch(IEnemy enemy, Move move){
        //string moveName = _playerActionManager.GetCurMove().GetName().ColorFor(Entity.MOVE);
        //TODO get targeted move
        string enemyName = enemy.GetName().ColorFor(Entity.ENEMY);
        //TODO fix this text
        _infoLabel.SetText("mot a match");
    }
    public void ShowMoveNoTarget(){
        //a move has been selected, but is not over an enemy
        string moveName = _playerActionManager.GetCurMove().GetName().ColorFor(Entity.MOVE);
        string instructions = "\ndouble click to cancel".ColorFor(Entity.INSTRUCTIONS);
        _infoLabel.SetText("use " + moveName + " on..." + instructions);
    }
    public void ShowMsg(string msg){
        _infoLabel.SetText(msg);
        //_infoLabel.StartTyping(null,msg, 0.04f, 2.0f);
    }
}
