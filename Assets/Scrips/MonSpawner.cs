using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MonSpawner : MonoBehaviour
{
    public Monster[] monsterArr;
    public Transform[] spawnPoints;

    private List<Monster> monsters = new List<Monster>();

    public float spawnInterval = 2f;
    private float spawnTime = 0f;

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > spawnInterval)
        {
            CreateMonster();
            spawnTime = 0f;
        }
    }

    public void CreateMonster()
    {
        var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var newMonster = monsterArr[Random.Range(0, monsterArr.Length)];
        var monster = Instantiate(newMonster, point.position, point.rotation);

        monsters.Add(monster);

        monster.OnDeath += () => monsters.Remove(monster);
        monster.OnDeath += () => Destroy(monster.gameObject, 3f);
    }

    
}
