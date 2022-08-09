using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persitenceObjPrefab;
    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;
        SpawnPersistentObject();
        hasSpawned = true;
    }

    private void SpawnPersistentObject()
    {
        GameObject persistentObject = Instantiate(persitenceObjPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
