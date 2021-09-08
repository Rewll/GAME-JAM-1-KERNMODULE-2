using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody2D body;

    public float runSpeed = 10.0f;
    public float jumpHeight = 10.0f;
    public bool IsGrounded;
    public SpriteRenderer SR;
    public LineRenderer LR;
    public Transform sword;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded();

        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += runSpeed * Time.deltaTime;
            SR.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= runSpeed * Time.deltaTime;
            SR.flipX = true;
        }
        transform.position = pos;


        if (Input.GetKeyDown(KeyCode.E) && IsGrounded)
        {
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }

        LR.SetPosition(0, transform.position);
        LR.SetPosition(1, sword.position);

    }

    void isgrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.23f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.23f, Color.magenta);
        //Debug.Log(hit.transform.gameObject.name);
        if (hit)
        {
            IsGrounded = true;
        }
        else if (!hit)
        {
            IsGrounded = false;
        }
    }
}