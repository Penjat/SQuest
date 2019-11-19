using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        _infoLabel.text = "using " + move._name + " on " + targeted[0].GetName();
    }
    public void ShowTargetEnemy(IEnemy enemy){
        _infoLabel.text = "use <color=white>" + _playerActionManager.GetCurMove()._name + "</color> on <color=red>" + enemy.GetName() + "</color>";
    }
    public void ShowMoveNoTarget(){
        //a move has been selected, but is not over an enemy
        Move move = _playerActionManager.GetCurMove();
        _infoLabel.text = "use " + move._name + " on...";
    }
}
