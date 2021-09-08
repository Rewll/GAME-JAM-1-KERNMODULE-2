using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2.0f;
    GameObject player;
    public GameObject shurikenPrefab;
    private Vector2 position;
    private bool mayrun = true;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (mayrun)
        {
            StartCoroutine(shoot());
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Sword")
        {
            Debug.Log("DOEI");
            Destroy(gameObject);
        }
    }

    IEnumerator shoot()
    {
        mayrun = false;
        while (true)
        {
            Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(15);
        }
    }
}