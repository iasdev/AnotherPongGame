using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    [Range(1f, 5f)]
    public float speedIncrement;
    private float originalSpeed;
    private Vector3 zMovement = Vector3.forward;
    private Vector3 xMovement = Vector3.right;

    public float waitTimeForNextGame = 3;
    private float remainingWaitTime = 0;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TopSide")) {
            zMovement = Vector3.back;
        } else if (other.CompareTag("DownSide")) {
            zMovement = Vector3.forward;
        } else if (other.CompareTag("RightPad")) {
            xMovement = Vector3.left;
            speed += speedIncrement;
        } else if (other.CompareTag("LeftPad")) {
            xMovement = Vector3.right;
            speed += speedIncrement;
        } else if (other.CompareTag("GoalBand")) {
            transform.position = Vector3.zero;
            zMovement = Random.value > 0.5 ? Vector3.back : Vector3.forward;
            xMovement = Random.value > 0.5 ? Vector3.right : Vector3.left; 
            speed = originalSpeed;
            
            remainingWaitTime = waitTimeForNextGame;
        }
    }

    void Start() {
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingWaitTime > 0) {
            remainingWaitTime -= Time.deltaTime;
        } else {
            transform.Translate(zMovement * speed * Time.deltaTime);
            transform.Translate(xMovement * speed * Time.deltaTime);
        }
    }
}
