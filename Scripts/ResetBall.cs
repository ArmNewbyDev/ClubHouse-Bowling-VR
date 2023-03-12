using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    public GameObject ball;
    Rigidbody rb;
    private void Start()
    {
       rb = ball.GetComponent<Rigidbody>();
    }
    public void ResetBallPosition()
    {
        
        ball.transform.position = this.gameObject.transform.position;
        rb.velocity = Vector3.zero;
    }
}
