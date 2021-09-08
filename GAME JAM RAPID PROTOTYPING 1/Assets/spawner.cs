using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    bool rightOrLeft;
    public int enemyAmount;
    public Transform rightSpawnPos, leftSpawnPos;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        for (int i = 0; i < Mathf.Infinity; i++)
        {
            rightOrLeft = Random.value > 0.5f;
            if (rightOrLeft)
            {
                Instantiate(enemyPrefab, rightSpawnPos.position, Quaternion.identity);
            }
            else if (!rightOrLeft)
            {
                Instantiate(enemyPrefab, leftSpawnPos.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
