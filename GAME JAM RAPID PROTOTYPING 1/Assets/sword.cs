using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public float speed = 20f;
    public float maxSwordX;
    public float maxSwordY;
    public float maxMovement;
    public GameObject player;
    public BoxCollider2D col;
    bool isrunning;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
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

        if (Vector3.Distance(player.transform.position, pos) < maxMovement)
        {
            transform.position = pos;
        }

        

        if (Input.GetKey(KeyCode.RightControl))
        {
            if (!isrunning)
            {
                StartCoroutine(slash());
            }
        }
    }

    IEnumerator slash()
    {
        isrunning = true;
        col.enabled = true;
        transform.eulerAngles = new Vector3(0, 0, -50);
        yield return new WaitForSeconds(.6f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        col.enabled = false;
        isrunning = false;
    }
}
