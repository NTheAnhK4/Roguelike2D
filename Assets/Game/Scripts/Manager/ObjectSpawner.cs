using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [SerializeField] private Transform objectHolder;
    public List<Transform> FoodPrefabList;
    public List<Transform> WallPrefabList;
    public List<Transform> EnemyPrefabList;
    protected override void Awake()
    {
        base.Awake();
        if (objectHolder == null)
        {
            objectHolder = GameObject.Find("Object Holder")?.transform;
            if (objectHolder == null) objectHolder = new GameObject("Object Holder").transform;
        }
    }

    private CellObject SpawnObject(List<Transform> prefabList, Vector3 objectPosition)
    {
        int randomIndex = Random.Range(0, prefabList.Count);
        Transform objectPrefab = prefabList[randomIndex];
        CellObject newObject = PoolingManager.Spawn(objectPrefab.gameObject, objectPosition, default, objectHolder).GetComponent<CellObject>();
        return newObject;
    }

    public CellObject GenerateEnemy(Vector3 enemyPosition)
    {
        CellObject newEnemy = SpawnObject(EnemyPrefabList, enemyPosition);
        return newEnemy;
    }
    public CellObject GenerateFood(Vector3 foodPosition)
    {
        CellObject newFood = SpawnObject(FoodPrefabList, foodPosition);
        return newFood;
    }

    public CellObject GenerateWall(Vector3 wallPosition)
    {
        CellObject newWall = SpawnObject(WallPrefabList, wallPosition);
        return newWall;
    }
}
