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
    private bool anyPlayerWin = false;

    public float waitTimeForNextGame = 3;
    private float remainingWaitTime = 0;

    public AudioClip ballCollision;
    public AudioClip goal;
    public AudioClip endGame;
    private AudioSource audioSource;

    public string maxScoreToWin;
    public TextMesh leftScore;
    public TextMesh rightScore;
    public TextMesh messages;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TopSide"))
        {
            zMovement = Vector3.back;
        }
        else if (other.CompareTag("DownSide"))
        {
            zMovement = Vector3.forward;
        }
        else if (other.CompareTag("RightPad"))
        {
            xMovement = Vector3.left;
            OnAnyPadTouch();
        }
        else if (other.CompareTag("LeftPad"))
        {
            xMovement = Vector3.right;
            OnAnyPadTouch();
        }
        else if (other.CompareTag("GoalBand"))
        {
            transform.position = Vector3.zero;
            zMovement = Random.value > 0.5 ? Vector3.back : Vector3.forward;
            xMovement = Random.value > 0.5 ? Vector3.right : Vector3.left;
            speed = originalSpeed;
            remainingWaitTime = waitTimeForNextGame;

            TextMesh score = other.gameObject.name == "LeftGoalBand" ? leftScore : rightScore;
            score.text = (int.Parse(score.text) + 1).ToString();

            if (leftScore.text == maxScoreToWin)
            {
                OnPlayerWin("Verde");
            }
            else if (rightScore.text == maxScoreToWin)
            {
                OnPlayerWin("Azul");
            }
            else
            {
                audioSource.clip = goal;
                audioSource.Play();
            }
        }
    }

    private void OnAnyPadTouch()
    {
        speed += speedIncrement;
        audioSource.clip = ballCollision;
        audioSource.Play();
    }

    private void OnPlayerWin(string player)
    {
        anyPlayerWin = true;
        audioSource.clip = endGame;
        audioSource.Play();
        messages.text = "Ha ganado el jugador " + player;
    }

    void Start()
    {
        originalSpeed = speed;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingWaitTime > 0)
        {
            remainingWaitTime -= Time.deltaTime;
        }
        else
        {
            if (anyPlayerWin)
            {
                anyPlayerWin = false;
                messages.text = "";
                leftScore.text = "0";
                rightScore.text = "0";
            }

            transform.Translate(zMovement * speed * Time.deltaTime);
            transform.Translate(xMovement * speed * Time.deltaTime);
        }
    }
}
