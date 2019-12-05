using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory {
    public GameObject _impPrefab;
    public GameObject _goblinPrefab;
    public GameObject GetPrefab(string enemyName){
        switch(enemyName){
            case "Imp":
            return _impPrefab;

            case "Goblin":
            return _goblinPrefab;
        }
        //default to imp
        return _impPrefab;
    }
}

public interface IEnemyFactory{
    GameObject GetPrefab(string enemyName);
}
