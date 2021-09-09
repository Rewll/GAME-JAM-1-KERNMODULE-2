using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float attackRange = 2.0f;
    public float distance;
    public float newpos;
    GameObject player;
    public GameObject shurikenPrefab;
    private Vector2 position;
    private bool mayrun = true;
    public bool isNear = false;
    Rigidbody2D rb2d;
    SpriteRenderer SR;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < attackRange)
        {
            isNear = true;
        }
        else
        {
            isNear = false;
        }


        float step = speed * Time.deltaTime;
        if (mayrun && isNear)
        {
            StartCoroutine(shoot());
        }


        if (isNear)
        {
            ChasePlayer();
        }

        void ChasePlayer()
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb2d.velocity = new Vector2(speed, 0);
                SR.flipX = false;
            }
            else
            {
                SR.flipX = true;
                rb2d.velocity = new Vector2(-speed, 0);
            }
        }


        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    IEnumerator shoot()
    {
        mayrun = false;
        while (true)
        {
            Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(6);
        }
    }
}