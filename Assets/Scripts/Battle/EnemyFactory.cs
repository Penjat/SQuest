﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory {
    public GameObject _impPrefab;
    public GameObject GetPrefab(string enemyName){
        switch(enemyName){
            case "imp":
            return _impPrefab;
        }
        //default to imp
        return _impPrefab;
    }
}

public interface IEnemyFactory{
    GameObject GetPrefab(string enemyName);
}
