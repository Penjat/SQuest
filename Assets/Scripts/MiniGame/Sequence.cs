using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence  {
    List<Note> _noteList;
    Move _move;
    public Sequence(Move move){
        _move = move;
        _noteList = new List<Note>();
    }
    public Note[] GetNotes(){
        return _noteList.ToArray();
    }
    public MoveType GetMoveType(){
        return _move.GetPrimaryType();
    }
    public Move GetMove(){
        return _move;
    }
    public void AddNote(Note note){
        _noteList.Add(note);
    }
}
