using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject prefabRopeSegs;
    public int numlinks;

    private void Start()
    {
        generateRope();
    }

    void generateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < numlinks; i++)
        {
            GameObject newSegment = Instantiate(prefabRopeSegs);
            newSegment.transform.parent = transform;  
            newSegment.transform.position = transform.position;
            HingeJoint2D hj = newSegment.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSegment.GetComponent<Rigidbody2D>();

        }
    }
}