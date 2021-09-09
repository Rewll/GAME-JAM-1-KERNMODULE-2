using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed = 2.0f;
    public float WaitTime = 7.0f;
    Vector3 moveDirection;

    private void Start()
    {
        moveDirection = transform.position - GameObject.FindWithTag("Player").transform.position;
        StartCoroutine(stopExisting());
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -90 * rotateSpeed * Time.deltaTime));
        transform.position += -moveDirection * moveSpeed * Time.deltaTime;
    }

    IEnumerator stopExisting()
    {
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }
}