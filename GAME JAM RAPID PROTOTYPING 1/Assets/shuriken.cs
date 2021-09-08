using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed = 2.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -90 * rotateSpeed * Time.deltaTime));
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, step);
    }
}