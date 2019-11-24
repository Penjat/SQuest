using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceFactory {
    public static Sequence[] CreateSequenceArray(IDictionary<Move, IEnemy[]> actions){
        //create a general array of what inputs will be for the player
        BeatEvent[] playerSequence = CreatePlayerSequence();

        //create a list of the sequences needed
        List<Sequence> sequenceList = new List<Sequence>();
        foreach(KeyValuePair<Move, IEnemy[]> action in actions){
            //TODO add data from move
            Move move = action.Key;
            sequenceList.Add(new Sequence(move.GetPrimaryType()));
        }

        //distribute the playerSequence amounst the sequence List
        foreach(BeatEvent beat in playerSequence){
            //choose a random sequence
            int r = Random.Range(0,sequenceList.Count);
            for(int i=0;i<sequenceList.Count;i++){
                int duration = beat.GetDuration();
                Sequence sequence = sequenceList[i];
                if(i == r){
                    Note note = new Note(duration,false);
                    sequence.AddNote(note);
                }else{
                    Note note = new Note(duration,true);
                    sequence.AddNote(note);
                }
            }
        }


        return sequenceList.ToArray();
    }

    private static int GetRandDuration(){
        int r = Random.Range(0, 2);
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
    private static BeatEvent[] CreatePlayerSequence(){
        //creates a sequence of inputs of the appropriate difficulty
        //considers how many actions a player will have to make at once
        //will later divide between the different action inputs involved

        float relativeTime = 0.0f;
        List<BeatEvent> beatList = new List<BeatEvent>();

        //adds up to 1 bars
        while(relativeTime < 1.0f){
            //get a random duration
            BeatEvent beat = new BeatEvent(4, 1);
            //add it to the time
            relativeTime += 1.0f/(float)beat.GetDuration();
            beatList.Add(beat);
        }
        //TODO maybe to array is not needed
        return beatList.ToArray();
    }
}
