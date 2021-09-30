using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefabEnemy;
    public Transform left, right;
    public int min, max;
    public bool flying;


    void spawnFlyEnemy(int min, int max, GameObject prefab, Transform left, Transform right)
    {
        int enemyAmount = Random.Range(min, max);
        for (int i = 0; i < enemyAmount; i++)
        {
            Vector2 newPos;
            bool rightOrLeft = (Random.value > 0.5f);
            if (rightOrLeft)
            {
                newPos.x = left.position.x;
            }
            else
            {
                newPos.x = right.position.x;
            }
            newPos.y = Random.Range(right.position.y, left.position.y);
            Instantiate(prefab, newPos, Quaternion.identity);
        }

    }

    void spawnEnemy(int min, int max, GameObject prefab, Transform left, Transform right)
    {
        int enemyAmount = Random.Range(min, max);
        for (int i = 0; i < enemyAmount; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(left.position.x, right.position.x), -2);
            Instantiate(prefab, newPos, Quaternion.identity);
        }

    }
    private void Start()
    {
        if (flying)
        {
            spawnFlyEnemy(min, max, prefabEnemy, left, right); 
        }
        else if (!flying)
        {
            spawnEnemy(min, max, prefabEnemy, left, right);
        }
    }
}