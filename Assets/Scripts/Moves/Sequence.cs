using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence  {
    int[] _array;
    public Sequence(int[] array){
        _array = array;
    }
    public int[] GetSequence(){
        return _array;
    }
}
