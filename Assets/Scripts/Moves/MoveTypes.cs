using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType {
    Hand, Mouth, Ass, Breasts
};

public class MoveTypeHelper{
    public static MoveType FindMoveType(int i){
        switch(i){
            case 0:
            return MoveType.Hand;
            case 1:
            return MoveType.Mouth;
            case 2:
            return MoveType.Ass;
            case 3:
            return MoveType.Breasts;
            default:
            return MoveType.Hand;
        }
    }
}
