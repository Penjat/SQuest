﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//-------
//Manages the move the player will use this round
//-------

public class PlayerActionManager {

    private PlayerAction _curAction;
    private IDictionary<PlayerAction,ITarget[]> _actions = new Dictionary<PlayerAction, ITarget[]>();
    private IDictionary<IBodyPart,Move> _usedParts = new Dictionary<IBodyPart, Move>();
    private IDictionary<Move,float> _actionResults;
    //TODO posibly have IEnemy[] for multiple targets

    private Player _player;

    public PlayerActionManager(Player player){
        _player = player;

    }
    public void SelectMove(Move move, IBodyPart categoryBodyPart, PlayerBattleDisplay playerDisplay, List<IEnemy> enemies){
        //selected a move from a category

        PlayerAction newAction = CreateAction(move, categoryBodyPart);
        //check if a target needs to be selected
        switch(move.GetSelectType()){
            //for selecting a single enemy target
            case SelectType.SingleTarget:
            _curAction = newAction;
            break;

            //automatically target the player
            case SelectType.PlayerOnly:
            ITarget[] playerOnly = new ITarget[]{playerDisplay};
            UseMoveOn(playerOnly, newAction);
            break;

            //automatically select all enemies
            case SelectType.AllEnemies:
            //TODO select all enemies
            break;

        }


    }
    private PlayerAction CreateAction(Move move, IBodyPart categoryBodyPart){
        //creates a PlayerAction with the move and a list of the player's body parts that will be used

        //create a collection of body parts for the move
        List<IBodyPart> bodyParts = new List<IBodyPart>();
        foreach(MoveType moveType in move.GetPartsUsed()){
            //make sure the body part we clicked on is used
            if(moveType == move.GetPrimaryType() && !bodyParts.Contains(categoryBodyPart)){
                bodyParts.Add(categoryBodyPart);
                break;
            }
            //find the  body part that is not in used parts and matches the move type
            IBodyPart bodyPart = _player.GetBodyParts().First(x => x.GetMoveType() == moveType && !_usedParts.ContainsKey(x));
            bodyParts.Add(bodyPart);
        }

        return new PlayerAction(move, bodyParts.ToList());
    }
    public void SelectTargets(ITarget[] targets){
        //sets the move to be used on the seleceted enemy
        //TODO check if valid target
        if(_curAction == null){
            return;
        }
        UseMoveOn(targets, _curAction);
        _curAction = null;
    }
    public void CancelSelected(){
        _curAction = null;
    }
    private void UseMoveOn(ITarget[] targets, PlayerAction playerAction){
        //TODO change for area fx
        foreach(ITarget target in targets){
            target.TargetWith(playerAction.GetMove());
        }
        _actions.Add(playerAction, targets);
        AddToUsedParts(playerAction.GetMove());
    }
    public void CancelMoveType(IBodyPart bodyPart){
        Move move = _usedParts[bodyPart];
        PlayerAction action = _actions.Select(x => x.Key).First(x => x.GetParts().Contains(bodyPart));
        ITarget[] targets = _actions[action];

        UnTargetAll(targets, move);
        RemoveFromUsedParts(action);
        _actions.Remove(action);
        Debug.Log("removing move " + move.GetName());
        return;
    }
    private void UnTargetAll(ITarget[] targets, Move move){
        //untargets all the enemies from the move
        foreach(ITarget target in targets){
            target.UnTarget(move);
        }
    }
    public void AddToUsedParts(Move move){

        foreach(MoveType moveType in move.GetPartsUsed()){
            //find the first available body part of the same type
            IBodyPart bodyPart = _player.GetBodyParts().First(x => x.GetMoveType() == moveType && !_usedParts.ContainsKey(x));
            _usedParts.Add(bodyPart,move);
        }
    }
    public void RemoveFromUsedParts(PlayerAction action){
        foreach(IBodyPart bodyPart in action.GetParts()){
            _usedParts.Remove(bodyPart);
        }
    }
    public IDictionary<IBodyPart,Move> GetUsedParts(){
        return _usedParts;
    }
    public void ClearUsedParts(){
        _usedParts.Clear();
        _actions.Clear();
    }
    public void UseMoves(){
        foreach(KeyValuePair<PlayerAction, ITarget[]> action in _actions){
            ITarget[] targets = action.Value;
            Move move = action.Key.GetMove();
            List<IBodyPart> bodyParts = action.Key.GetParts();
            float percent = FindPercent(_actionResults,move);
            //TODO fix for Percent
            Dmg dmg = move.GetDmg();
            dmg = dmg.ApplyParts(bodyParts);
            foreach(ITarget target in targets){
                target.AddToDmg(dmg);
                target.UseMove(move, percent);
                action.Key.ApplySpecialEffects(target);
                move.CountUse();
            }
        }
    }
    private float FindPercent(IDictionary<Move,float> results, Move move){
        //trys to find the results for the given move
        if(results.ContainsKey(move)){
            float percent = results[move];
            return percent;
        }
        //if cant find the move for some reason
        //just return 100%
        Debug.Log("WARNING could not find key");
        return 100.0f;

    }
    public void GemCleared(MoveType moveType, float accuracy){
        //there will be many modifiers to add here
        if(moveType == MoveType.Hand){
            //hand jobs dont do dmg to player
            return;
        }
        //TODO calculate this properly
        if(accuracy > 99.0f){
            //TODO have player take dmg for missed
            //PlayerTakeDmg(1);
        }
    }
    public ITarget[] GetTargetsFor(IBodyPart bodyPart){
        PlayerAction action = _actions.First(x => x.Key.GetParts().Contains(bodyPart)).Key;
        return _actions[action];
    }
    public bool IsSelectingTarget(){
        return _curAction != null;
    }
    public Move GetCurMove(){
        return _curAction.GetMove();
    }
    public IDictionary<PlayerAction, ITarget[]> GetActions(){
        return _actions;
    }
    public void SetResults(IDictionary<Move,float> results){
        _actionResults = results;
    }
}
