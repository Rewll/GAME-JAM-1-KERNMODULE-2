using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public float speed = 20f;
    public float sliceTime = .2f;
    public float sliceAngle;
    public float maxSwordX;
    public float maxSwordY;
    public float maxMovement;
    public GameObject Player;
    public BoxCollider2D col;
    bool isrunning;
    public SpriteRenderer SR;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        col.enabled = false;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }

        if (Vector3.Distance(Player.transform.position, pos) < maxMovement)
        {
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.Slash))
        {
            if (!isrunning)
            {
                StartCoroutine(slash());
            }
        }

        if (transform.position.x > Player.transform.position.x)
        {
            SR.flipX = true;
            sliceAngle = -50;
        }
        if (transform.position.x < Player.transform.position.x)
        {
            SR.flipX = false;
            sliceAngle = 50;

        }
    }

    IEnumerator slash()
    {
        isrunning = true;
        col.enabled = true;
        transform.eulerAngles = new Vector3(0, 0, sliceAngle);
        yield return new WaitForSeconds(sliceTime);
        transform.eulerAngles = new Vector3(0, 0, 0);
        col.enabled = false;
        isrunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Shuriken")
        {
            Debug.Log("Destroying shuriken");
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Enemy")
        {
            Debug.Log("Destroying Enemy");
            Destroy(collision.gameObject);
        }

    }

}