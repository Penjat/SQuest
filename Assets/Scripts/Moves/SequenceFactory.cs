using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceFactory {
    public static Sequence[] CreateSequence(IDictionary<Move, IEnemy[]> actions){
        Sequence[] sequenceArray = new Sequence[1]{new Sequence(new int[]{4,4,8,4})};
        return sequenceArray;
    }
}
