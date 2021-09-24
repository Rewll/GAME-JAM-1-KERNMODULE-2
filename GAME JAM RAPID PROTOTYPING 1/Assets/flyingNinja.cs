using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Attack, Patrol, death }

public class flyingNinja : MonoBehaviour
{
    Transform player;
    
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
    public float attackRange;

    private void Awake()
    {
        CurrentState = StartState;
        transform.position = wayPointLeft.position;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
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
        float step = attackSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        if (Vector3.Distance(transform.position, player.position) < 0.1f)
        {
            switchState(State.Patrol);
        }
    }

    void PatrolBehaviour()
    {
        rb2d.velocity = new Vector2(patrolSpeed * speedMultiplier, 0);
        checkPos();

        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            switchState(State.Attack);
        }
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