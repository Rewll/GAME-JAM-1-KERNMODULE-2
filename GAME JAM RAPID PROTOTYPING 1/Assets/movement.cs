using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    Rigidbody2D body;

    public float runSpeed = 10.0f;
    public float jumpHeight = 10.0f;

    public int health;
    public int startHealth;
    public int damage;

    public bool IsGrounded;
    public SpriteRenderer SR;
    public LineRenderer LR;
    public Transform sword;
    public bool gameOver = false;
    public bool gameWin = false;

    public int circleAmount;
    public bool winGame = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        health = startHealth;
        Time.timeScale = 1;
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


        if (Input.GetKeyDown(KeyCode.W) && IsGrounded)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Shuriken")
        {
            health -= damage;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Time.timeScale = 0;
                gameOver = true;
            }
        }

        if (collision.transform.tag == "Circle")
        {
            Destroy(collision.gameObject);
            circleAmount++;
            if (circleAmount >= 3)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        Debug.Log("WIn");
        gameWin = true;
        Time.timeScale = 0;
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 60, 20), "HP: " + health.ToString(), 10);
        if (gameOver)
        {
            GUI.TextField(new Rect(100, 200, 300, 50), "YOUR END HAS COME", 30);
            if (GUI.Button(new Rect(100, 300, 300, 300), "RESTART"))
                SceneManager.LoadScene(0);
        }
        if (gameWin)
        {
            GUI.TextField(new Rect(100, 200, 300, 50), "YOU ARE VICTORIOUS", 30);
            if (GUI.Button(new Rect(100, 300, 100, 40), "RESTART"))
                SceneManager.LoadScene(0);
        }
    }

}