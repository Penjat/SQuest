using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence  {
    List<Note> _noteList;
    MoveType _moveType;
    public Sequence(MoveType moveType){
        _moveType = moveType;
        _noteList = new List<Note>();
    }
    public Note[] GetNotes(){
        return _noteList.ToArray();
    }
    public MoveType GetMoveType(){
        return _moveType;
    }
    public void AddNote(Note note){
        _noteList.Add(note);
    }
}
