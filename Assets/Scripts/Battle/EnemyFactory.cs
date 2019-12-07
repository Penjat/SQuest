using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory {

    //--------Enemy Prefabs--------
    public GameObject _impPrefab;
    public GameObject _goblinPrefab;
    public GameObject _trollPrefab;
    public GameObject _ogrePrefab;
    public GameObject _frogPrefab;
    public GameObject _centarPrefab;
    public GameObject _harpyPrefab;
    //-----------------------------

    public GameObject GetPrefab(string enemyName){
        switch(enemyName){
            case "Imp":
            return _impPrefab;

            case "Goblin":
            return _goblinPrefab;

            case "Ogre":
            return _ogrePrefab;

            case "Centar":
            return _centarPrefab;

            case "Harpy":
            return _harpyPrefab;

            case "Frog":
            return _frogPrefab;

            case "Troll":
            return _trollPrefab;
        }
        //default to imp
        return _impPrefab;
    }
}

public interface IEnemyFactory{
    GameObject GetPrefab(string enemyName);
}
