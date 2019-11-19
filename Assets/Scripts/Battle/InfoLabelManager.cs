using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StringMethods;

public class InfoLabelManager : MonoBehaviour{
    public Text _infoLabel;

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


    //---------------------Show Methods-------------------------------
    public void ShowBlank(){
        _infoLabel.text = "";
    }
    public void ShowTargetsForMove(Move move){
        IEnemy[] targeted = _playerActionManager.GetTargetsFor(move);
        string moveName = move._name.ColorFor(Entity.MOVE);
        string enemyName = targeted[0].GetName().ColorFor(Entity.ENEMY);
        _infoLabel.text = "using " + moveName + " on " + enemyName;
    }
    public void ShowTargetEnemy(IEnemy enemy){
        string moveName = _playerActionManager.GetCurMove()._name.ColorFor(Entity.MOVE);
        string enemyName = enemy.GetName().ColorFor(Entity.ENEMY);
        _infoLabel.text = "use " + moveName + " on " + enemyName;
    }
    public void ShowMoveNoTarget(){
        //a move has been selected, but is not over an enemy
        string moveName = _playerActionManager.GetCurMove()._name.ColorFor(Entity.MOVE);
        _infoLabel.text = "use " + moveName + " on...";
    }
}
