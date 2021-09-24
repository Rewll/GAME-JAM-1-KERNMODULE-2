using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Attack, Patrol, death }

public class flyingNinja : MonoBehaviour
{
    Transform player;
    public float playerDist;

    private Rigidbody2D rb2d;
    [Header("State Machine")]
    [SerializeField]
    public State StartState;
    [Space]
    public State CurrentState;

    [Header("Patrol")]
    public Transform wayPointLeft;
    public Transform wayPointRight;
    public float patrolSpeed = 2f;
    public float speedMultiplier = 1f;

    [Header("Attack")]
    public float attackSpeed = 2f;
    private float attackSpeedMultiplier = 0.1f;
    public float attackRange;
    public float patrolRange;
    Vector3 attackDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = AudioManager.Instance.Player.transform;
        wayPointLeft = AudioManager.Instance.wayPointLeft;
        wayPointRight = AudioManager.Instance.wayPointRight;

        CurrentState = StartState;
    }

    private void Update()
    {
        playerDist = Vector2.Distance(transform.position, player.transform.position);
    }

    private void FixedUpdate()
    {
        CheckState();
    }

    void CheckState()
    {
        switch (CurrentState)
        {
            case State.Attack: AttackBehaviour(); break;
            case State.Patrol: PatrolBehaviour(); break;
            case State.death: DyingBehaviour(); break;
        }
    }


    void switchState(State newState)
    {
        switch (newState) 
        {
            case State.Attack:
                onAttackEnter();
                break;

            case State.Patrol:
                onPatrolEnter();
                break;

            case State.death:
                break;
        }
        CurrentState = newState;
    }

    void AttackBehaviour()
    {
        rb2d.AddForce(attackDirection * attackSpeed * attackSpeedMultiplier, ForceMode2D.Impulse);
        if (playerDist > patrolRange)
        {
            switchState(State.Patrol);
        }
    }

    void PatrolBehaviour()
    {
        rb2d.velocity = new Vector2(patrolSpeed * speedMultiplier, 0);
        checkPos();

        if (playerDist < attackRange)
        {
            switchState(State.Attack);
        }
    }

    void onAttackEnter()
    {
        attackDirection = player.position - transform.position;
    }


    void onPatrolEnter()
    {

    }

    void checkPos()
    {
        if (transform.position.x < wayPointLeft.transform.position.x)
        {
            speedMultiplier = 1;
        }
        else if (transform.position.x > wayPointRight.transform.position.x)
        {
            speedMultiplier = -1;
        }
    }


    void DyingBehaviour()
    {

    }
}