using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Dictionary<int, Data.Monster> _monsterStat = new Dictionary<int, Data.Monster>();
    public Transform[] _spawnPoint;
    float _spawnTime = 0.5f;
    bool _isSpawning = false;
    [SerializeField]
    GameObject[] _spawnUnit;

    [SerializeField]
    int _maxSpawnUnit = 50;

    public int enemyCount = 0;
    int timeLevel = 0;
    int TimeLevel { get { return timeLevel; } set { timeLevel = value; if (timeLevel >= 3) _spawnTime = 0.1f; } }
    public void AddEnemyCount(int value) { enemyCount += value; }
    private void Start()
    {
        _monsterStat = Managers.Data.MonsterData;
        _spawnPoint = GetComponentsInChildren<Transform>();
        Managers.Game._OnSpawnEvent -= AddEnemyCount;
        Managers.Game._OnSpawnEvent += AddEnemyCount;
    }

    private void Update()
    {
        if ((timeLevel + 1) * 60 < Managers.GameTime)
        {
            timeLevel = (int)Managers.GameTime / 60;
            if (timeLevel <= 5)
            {
                Debug.Log($"{timeLevel}Boss Spawn!");
                SpawnBoss(timeLevel);
            }

        }
        if (!_isSpawning)
            StartCoroutine(SpawnMonster());
    }

    void SpawnBoss(int timeLevel)
    {
        GameObject Boss = null;
        if (timeLevel < 5)
        {
            int level = Managers.Game.getPlayer().GetComponent<PlayerStat>().Level;
            Boss = Managers.Game.Spawn(Define.WorldObject.Enemy, "Monster/Enemy");
            Boss.GetOrAddComponent<EnemyController>().Init(_monsterStat[timeLevel], level, Define.MonsterType.middleBoss);
        }
        else
        {
            Boss = Managers.Game.Spawn(Define.WorldObject.Enemy, "Monster/Boss");
        }
        if (Boss == null)
        {
            Debug.Log($"Boss Load Failed! level : {timeLevel}");
            return;
        }

        Boss.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;


    }


    IEnumerator SpawnMonster()
    {
        _isSpawning = true;
        if (enemyCount < _maxSpawnUnit)
        {
            int monsterType = SetRandomMonster(timeLevel);

            int level = Managers.Game.getPlayer().GetComponent<PlayerStat>().Level;
            GameObject enemy = Managers.Game.Spawn(Define.WorldObject.Enemy, "Monster/Enemy");
            enemy.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
            enemy.GetOrAddComponent<EnemyController>().Init(_monsterStat[monsterType], level, Define.MonsterType.Enemy);
        }
        yield return new WaitForSeconds(_spawnTime);
        _isSpawning = false;
    }


    int SetRandomMonster(int timeLevel)
    {
        float rand1 = Random.Range(0, 100);
        float rand2 = Random.Range(0, 100);
        int rd = 1;
        if (rand1 < 50)
        {
            if (rand2 < 90 - (20 * timeLevel))
                rd = 1;
            else
                rd = 2;
        }
        else if (rand1 < 90)
        {
            if (rand2 < 90 - (20 * timeLevel))
                rd = 3;
            else
                rd = 4;
        }
        else
            rd = 5;

        return rd;
    }

}
