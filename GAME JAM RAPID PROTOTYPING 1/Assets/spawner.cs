using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Ninjas")]
    bool rightOrLeft;
    public int enemyAmountStart;
    public Transform rightSpawnPos, leftSpawnPos;
    public Transform circle, circle2;
    public GameObject enemyPrefab;

    [Header("FlyingNinjas")]
    public Transform wayPointLeft;
    public Transform wayPointRight;
    public int flyingNinjaAmount;
    public GameObject flyingEnemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
        SpawnStartEnemies();
        for (int i = 0; i < enemyAmountStart; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(leftSpawnPos.position.x, rightSpawnPos.position.x), -2);
            Instantiate(enemyPrefab, newPos, Quaternion.identity);
        }
        SpawnFlyingEnemies();
    }

    //Flying Enemy
    void SpawnFlyingEnemies()
    {
        for (int i = 0; i < flyingNinjaAmount; i++)
        {
            Vector2 newPos;
            rightOrLeft = (Random.value > 0.5f);
            if (rightOrLeft)
            {
                newPos.x = wayPointLeft.position.x;
            }
            else
            {
                newPos.x = wayPointRight.position.x;
            }

            newPos.y = Random.Range(wayPointRight.position.y, wayPointLeft.position.y);
            Instantiate(flyingEnemyPrefab, newPos, Quaternion.identity);
        }
    }




    //Ninja Enemy
    IEnumerator spawnEnemy()
    {
        for (int i = 0; i < Mathf.Infinity; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(leftSpawnPos.position.x, rightSpawnPos.position.x), -2);
            Instantiate(enemyPrefab, newPos, Quaternion.identity);
            yield return new WaitForSeconds(6);
        }
    }

    void SpawnStartEnemies()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(leftSpawnPos.position.x, rightSpawnPos.position.x), -2);
            Instantiate(enemyPrefab, circle.position, Quaternion.identity);
        }
        for (int i = 0; i < 4; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(leftSpawnPos.position.x, rightSpawnPos.position.x), -2);
            Instantiate(enemyPrefab, circle2.position, Quaternion.identity);
        }
    }
}
