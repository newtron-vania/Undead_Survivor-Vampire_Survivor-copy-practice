using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Dictionary<int, Data.Monster> monsterStat = new Dictionary<int, Data.Monster>();
    public Transform[] spawnPoint;
    private float _timer;
    float _spawnTime= 0.5f;
    [SerializeField]
    GameObject[] _spawnUnit;

    [SerializeField]
    int _maxSpawnUnit = 50;

    public int enemyCount = 0;
    int timeLevel = 0;
    public void AddEnemyCount(int value) { enemyCount += value; }
    private void Start()
    {
        monsterStat = Managers.Data.MonsterData;
        spawnPoint = GetComponentsInChildren<Transform>();
        Managers.Game.OnSpawnEvent -= AddEnemyCount;
        Managers.Game.OnSpawnEvent += AddEnemyCount;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if ((timeLevel + 1) * 60 < Managers.GameTime)
        {
            if(timeLevel >= 3)
            {
                _spawnTime = 0.1f;
            }
            if(timeLevel <= 5)
            {
                timeLevel += 1;
                int level = Managers.Game.getPlayer().GetComponent<PlayerStat>().Level;
                GameObject middleBoss = Managers.Game.Spawn(Define.WorldObject.Enemy, "Monster/Enemy");
                middleBoss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                middleBoss.transform.localScale = Vector3.one * 2;
                middleBoss.transform.GetComponent<EnemyController>().Init(monsterStat[timeLevel], level, Define.MonsterType.middleBoss);
            }

        }
            
        if (_timer > _spawnTime)
        {
            SpawnMonster();
            _timer = 0f;
        }
    }

    void SpawnMonster()
    {
        if (enemyCount < _maxSpawnUnit)
        {
            int monsterType = SetRandomMonster(timeLevel);
            
            int level = Managers.Game.getPlayer().GetComponent<PlayerStat>().Level;
            GameObject enemy = Managers.Game.Spawn(Define.WorldObject.Enemy, "Monster/Enemy");
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.transform.GetComponent<EnemyController>().Init(monsterStat[monsterType], level, Define.MonsterType.Enemy);
        }
    }


    int SetRandomMonster(int timeLevel)
    {
        float rand1 = Random.Range(0, 100);
        float rand2 = Random.Range(0, 100);
        int rd = 1;
        if (rand1 < 50)
        {
            if (rand2 < 90- (20* timeLevel))
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
