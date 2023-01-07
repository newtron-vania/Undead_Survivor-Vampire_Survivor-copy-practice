using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    private float _timer;
    int level;
    [SerializeField]
    GameObject[] _spawnUnit;

    [SerializeField]
    int _maxSpawnUnit = 50;

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
        _timer += Time.deltaTime;
        if (level * 10 < Managers.GameTime)
            setLevel();
        if (_timer > (spawnData[level].spawnTime))
        {
            SpawnMonster();
            _timer = 0f;
        }
    }

    void SpawnMonster()
    {
        if (enemyCount < _maxSpawnUnit)
        {
            GameObject enemy = Managers.Game.Spawn(spawnData[level].Type, "Monster/Enemy");
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.transform.GetComponent<EnemyController>().Init(spawnData[level]);
        }
    }

    void setLevel()
    {
        level = Mathf.Min(Mathf.CeilToInt(Managers.GameTime / 10f), 1);
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
    public int defense;
    public int exp;
}
