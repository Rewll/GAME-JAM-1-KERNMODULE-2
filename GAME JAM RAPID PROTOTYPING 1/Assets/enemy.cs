using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2.0f;
    GameObject player;
    private Vector2 position;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Sword")
        {
            Debug.Log("DOEI");
            Destroy(gameObject);
        }
    }
}
