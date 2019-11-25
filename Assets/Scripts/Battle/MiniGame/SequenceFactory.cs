using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceFactory {
    public static Sequence[] CreateSequenceArray(IDictionary<Move, IEnemy[]> actions, int numberOfBeats){
        //create a general array of what inputs will be for the player
        BeatEvent[] playerSequence = CreatePlayerSequence(numberOfBeats);

        //create a list of the sequences needed
        List<Sequence> sequenceList = new List<Sequence>();
        foreach(KeyValuePair<Move, IEnemy[]> action in actions){
            //TODO add data from move
            Move move = action.Key;
            sequenceList.Add(new Sequence(move.GetPrimaryType()));
        }

        //distribute the playerSequence amounst the sequence List
        foreach(BeatEvent beat in playerSequence){
            //finds all the active beats at this instance
            HashSet<int> activeBeats = FindActiveBeats(beat.GetNumNotes(), sequenceList.Count);
            for(int i=0;i<sequenceList.Count;i++){
                //add a note to each sequence
                Sequence sequence = sequenceList[i];
                int duration = beat.GetDuration();
                //set the note to be a rest or not
                bool isRest = !activeBeats.Contains(i);
                Note note = new Note(duration,isRest);
                sequence.AddNote(note);
            }
        }
        return sequenceList.ToArray();
    }
    private static HashSet<int> FindActiveBeats(int numActiveBeats, int listCount){
        //returns a set with the index of the actionInputs that will have active beats
        HashSet<int> activeBeats = new HashSet<int>();

        //create a list of all the posible numbers
        List<int> posNumbers = new List<int>();
        for(int i=0;i<listCount;i++){
            posNumbers.Add(i);
        }
        //for every beat we want, pull out a posible number
        //check to make sure there are that many moves active
        for(int i=0;i<numActiveBeats && i<listCount;i++){
            int r = Random.Range(0,posNumbers.Count);
            activeBeats.Add(posNumbers[r]);
            posNumbers.RemoveAt(r);
        }
        return activeBeats;
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
    private static BeatEvent[] CreatePlayerSequence(int numberOfBeats){
        //creates a sequence of inputs of the appropriate difficulty
        //considers how many actions a player will have to make at once
        //will later divide between the different action inputs involved

        float relativeTime = 0.0f;
        float timeLimit = (float)(numberOfBeats/4);
        List<BeatEvent> beatList = new List<BeatEvent>();

        //adds up to number of bars
        while(relativeTime < timeLimit){
            int notesAtOnce = Random.Range(1,3);
            BeatEvent beat = new BeatEvent(4, notesAtOnce);
            //add it to the time
            relativeTime += 1.0f/(float)beat.GetDuration();
            beatList.Add(beat);
        }
        //TODO maybe to array is not needed
        return beatList.ToArray();
    }
}
