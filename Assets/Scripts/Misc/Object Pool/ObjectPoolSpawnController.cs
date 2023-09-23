using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSpawnController : SingletonObject<ObjectPoolSpawnController>
{
    private ObjectPooling poolingManager;
    
    void Awake()
    {
        base.Awake();

        poolingManager = GetComponent<ObjectPooling>();
    }
    
    public GameObject SpawnPooledPrefab(GameObject prefab, Vector2 position, Quaternion quaternion)
    {
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(prefab);
        SetPrefabPosition(spawnedPrefab, position);
        SetPrefabRotation(spawnedPrefab, quaternion);

        return spawnedPrefab;
    }

    void SetPrefabPosition(GameObject spawnedObject, Vector2 position)
    {
        spawnedObject.transform.position = position;
    }

    void SetPrefabRotation(GameObject spawnedObject, Quaternion quaternion)
    {
        spawnedObject.transform.rotation = quaternion;
    }
}
