using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Attack, Patrol, death }

public class flyingNinja : MonoBehaviour
{
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
    public float speedMultiplier = -1f;

    private void Awake()
    {
        CurrentState = StartState;
        transform.position = wayPointLeft.position;
        rb2d = GetComponent<Rigidbody2D>();
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

    }

    void PatrolBehaviour()
    {
        rb2d.velocity = new Vector2(patrolSpeed * speedMultiplier, 0);
    }

    void onPatrolEnter()
    {
        //speedcheck by checkin if x is bigger or smaller;
        //speedcheck by checkin if x is bigger or smaller than waypoints and then reversing speed so that it bounces inbetween
    }




    void DyingBehaviour()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Waypoint")
        {
            speedMultiplier *= -1;
            Debug.Log("BOINK");
        }
    }

}