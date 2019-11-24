using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceFactory {
    public static Sequence[] CreateSequenceArray(IDictionary<Move, IEnemy[]> actions){
        List<Sequence> sequenceList = new List<Sequence>();
        foreach(KeyValuePair<Move, IEnemy[]> action in actions){
            //TODO add data from move
            Move move = action.Key;
            sequenceList.Add(CreateSequence(move.GetPrimaryType()));
        }
        return sequenceList.ToArray();
    }
    private static Sequence CreateSequence(MoveType moveType){
        float relativeTime = 0.0f;
        int numberOfGems = 6;
        List<int> noteList = new List<int>();

        //adds up to 1 bars
        while(relativeTime < 1.0f){
            //get a random duration
            int note = GetRandNote();
            //add it to the time
            relativeTime += 1.0f/(float)note;
            noteList.Add(note);
        }
        return new Sequence(moveType,noteList.ToArray());
    }
    private static int GetRandNote(){
        int r = Random.Range(0, 3);
        switch(r){
            case 0:
            return 2;
            case 1:
            return 4;
            case 2:
            return 8;
        }
        return 4;
    }
}
