using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;
    [SerializeField]
    GameObject[] _spawnUnit;

    [SerializeField]
    int MaxSpawnUnit = 50;

    Queue<GameObject> _spawnUnits = new Queue<GameObject>();

    public int enemyCount = 0;
    public void AddEnemyCount(int value) { enemyCount += value; }
    private void Start()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        Managers.Game.OnSpawnEvent -= AddEnemyCount;
        Managers.Game.OnSpawnEvent += AddEnemyCount;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(Managers.GameTime / 10f), 1);

        if (timer > (spawnData[level].spawnTime))
        {
            SpawnMonster();
            timer = 0f;
        }
        enemyCount = _spawnUnits.Count;
    }

    void SpawnMonster()
    {
        if (enemyCount < MaxSpawnUnit)
        {
            GameObject enemy = Managers.Game.Spawn(spawnData[level].Type, "Monster/Enemy");
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.transform.GetComponent<EnemyController>().Init(spawnData[level]);
        }
    }


}

[System.Serializable]
public class SpawnData
{
    public Define.WorldObject Type;
    public float spawnTime;
    public int spriteType;
    public int maxHp;
    public float speed;
    public int attack;
    public int Defense;
}
