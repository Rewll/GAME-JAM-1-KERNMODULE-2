using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    bool rightOrLeft;
    public int enemyAmountStart;
    public Transform rightSpawnPos, leftSpawnPos;
    public Transform circle, circle2;
    public GameObject enemyPrefab;

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
    }
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
