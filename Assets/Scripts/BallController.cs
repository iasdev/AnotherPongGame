using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;
    
    public float speed;
    private Vector3 _zMovement = Vector3.forward;
    private Vector3 _xMovement = Vector3.right;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TopSide"))
        {
            _zMovement = Vector3.back;
        }
        else if (other.CompareTag("DownSide"))
        {
            _zMovement = Vector3.forward;
        }
        else if (other.CompareTag("RightPad"))
        {
            _xMovement = Vector3.left;
            speed = gameManager.OnPadTouch(speed);
        }
        else if (other.CompareTag("LeftPad"))
        {
            _xMovement = Vector3.right;
            speed = gameManager.OnPadTouch(speed);
        }
        else if (other.CompareTag("GoalBand"))
        {
            transform.position = Vector3.zero;
            _zMovement = Random.value > 0.5 ? Vector3.back : Vector3.forward;
            _xMovement = Random.value > 0.5 ? Vector3.right : Vector3.left;
            speed = gameManager.OnGoal(other.gameObject);
        }
    }

    private void Update()
    {
        if (gameManager.IsGameStarted())
        {
            transform.Translate(_zMovement * (speed * Time.deltaTime));
            transform.Translate(_xMovement * (speed * Time.deltaTime));
        }
    }
}
