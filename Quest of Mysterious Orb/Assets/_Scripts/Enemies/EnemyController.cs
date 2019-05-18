﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ExecutableController, IUpdatable, IEnableable, IDisaable, ILateUpdatable, IAwakable
{
    private List<EnemyObject> enemiesObject;
    public List<EnemyObject> EnemiesObject { get => enemiesObject; set => enemiesObject = value; }

    [SerializeField]
    private List<EnemyObject> enemiesToSpawn;

    [SerializeField]
    private List<GameObject> patrolToSpawn;

    public void OnIAwake()
    { 
        enemiesObject = new List<EnemyObject>();
        for(int i = 0 ; i < enemiesToSpawn.Count; ++i) {
            MyObjectPoolManager.Instance.CreatePoolIfNotExists(enemiesToSpawn[i].gameObject, 20, 50);
        } 
        for(int i = 0 ; i < patrolToSpawn.Count; ++i) {
            MyObjectPoolManager.Instance.CreatePoolIfNotExists(patrolToSpawn[i].gameObject, 20, 50);
        } 
    }

    public void OnIDisable()
    {
        if(enemiesObject.Count != 0) {
            for(int i = 0; i < enemiesObject.Count; ++i) {
                    (enemiesObject[i] as IDisaable).OnIDisable();
                }
        }
    }

    public void OnIEnable()
    {
        if(enemiesObject.Count != 0) {
            for(int i = 0; i < enemiesObject.Count; ++i) {
                (enemiesObject[i] as IEnableable).OnIEnable();
            }
        }
    }

    public void OnILateUpdate()
    {
        if(enemiesObject.Count != 0) {
            for(int i = 0; i < enemiesObject.Count; ++i) {
                (enemiesObject[i] as ILateUpdatable).OnILateUpdate();
            }
        }
    }

    public void OnIUpdate()
    {
        if(enemiesObject.Count != 0) {
            for(int i = 0; i < enemiesObject.Count; ++i) {
                (enemiesObject[i] as IUpdatable).OnIUpdate();
            }
        }
    }
}
