using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class movement : MonoBehaviour
{

    public int health;
    public int maxHealth;
    public int damage;

    public bool IsGrounded;
    public SpriteRenderer SR;
    public LineRenderer LR;
    public Transform sword;
    public bool gameOver = false;
    public bool gameWin = false;

    public int circleAmount;
    public bool winGame = false;

    public GameObject winScreen;
    public GameObject loseScreen;
    public HealthBar healthBar;
    public TMP_Text coinAmountText;
    [Space]

    [Header("Movement")]
    [SerializeField]
    float fJumpVelocity = 5;

    Rigidbody2D rigid;

    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;

    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        gameOver = false;
        AudioManager.Instance.Play("Game Music");
        Time.timeScale = 1;
        SR = GetComponent<SpriteRenderer>();
        LR = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //movePlayer();
        isgrounded();
        if (Input.GetKey(KeyCode.D))
        {
            SR.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            SR.flipX = true;
        }

        LR.SetPosition(0, transform.position);
        LR.SetPosition(1, sword.position);

        fGroundedRemember -= Time.deltaTime;
        if (IsGrounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * fCutJumpHeight);
            }
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, fJumpVelocity);
        }

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

    void movePlayer()
    {
        Vector2 v2GroundedBoxCheckPosition = (Vector2)transform.position + new Vector2(0, -0.01f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);



        float fHorizontalVelocity = rigid.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 5f);
        }
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 5f);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 5f);
        }

        Vector2 moveVec = new Vector2(fHorizontalVelocity, rigid.velocity.y);
        rigid.velocity = moveVec;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Shuriken")
        {
            health -= damage;
            Debug.Log("Shuriken");
            healthBar.SetHealth(health);
            AudioManager.Instance.Play("OUCH");
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                loseGame();
            }
        }

        if (collision.transform.tag == "Circle")
        {
            Destroy(collision.gameObject);
            circleAmount++;
            coinAmountText.text = circleAmount.ToString() + "/3";
            AudioManager.Instance.Play("Coin");
            if (circleAmount >= 3)
            {
                WinGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<flyingNinja>() != null)
        {
            health -= damage + 3;
            AudioManager.Instance.Play("OUCH");
            healthBar.SetHealth(health);
            if (health <= 0)
            {
                loseGame();
            }
        }
    }

 

    void WinGame()
    {
        Debug.Log("WIn");
        AudioManager.Instance.Play("Win");
        AudioManager.Instance.StopPlaying("Game Music");
        winScreen.SetActive(true);
        healthBar.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    void loseGame()
    {
        Debug.Log("Lose");
        AudioManager.Instance.Play("Lose");
        AudioManager.Instance.StopPlaying("Game Music");
        loseScreen.SetActive(true);
        healthBar.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // raoul is kinda cute UwU
}