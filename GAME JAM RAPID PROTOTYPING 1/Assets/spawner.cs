using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    bool rightOrLeft;
    public int enemyAmountStart;
    public Transform rightSpawnPos, leftSpawnPos;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());

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
}
