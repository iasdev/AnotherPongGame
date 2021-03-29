using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public float speed;
    public float topDownLimit = 5.5f;

    private int halfScreenPosX;
    private Vector3 originalLeftPadPosition;
    private Vector3 originalRightPadPosition;

    void Start() {
        halfScreenPosX = (int)Screen.width / 2;
        originalLeftPadPosition = GameObject.FindGameObjectWithTag("LeftPad").transform.position;
        originalRightPadPosition = GameObject.FindGameObjectWithTag("RightPad").transform.position;
    }

    void Update()
    {
        HandleDesktopMovement();
        HandleMobileMovement();

        if (transform.position.z > topDownLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topDownLimit);
        }
        else if (transform.position.z < -topDownLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -topDownLimit);
        }
    }

    private void HandleDesktopMovement()
    {
        if ((CompareTag("LeftPad") && Input.GetKey(KeyCode.W)) || (CompareTag("RightPad") && Input.GetKey(KeyCode.UpArrow))) {
            transform.Translate(Vector3.forward * 1 * speed * Time.deltaTime);
        } else if ((CompareTag("LeftPad") && Input.GetKey(KeyCode.S)) || (CompareTag("RightPad") && Input.GetKey(KeyCode.DownArrow))) {
            transform.Translate(Vector3.back * 1 * speed * Time.deltaTime);
        }
    }

    private void HandleMobileMovement()
    {
        Touch[] touches = Input.touches;

        foreach (Touch touch in touches)
        {
            bool isLeftScreenPart = touch.position.x < halfScreenPosX;
            
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 currentTap = new Vector3(touch.position.x, touch.position.y, 1);
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentTap);

                if (CompareTag("LeftPad") && isLeftScreenPart)
                {
                    transform.position = new Vector3(originalLeftPadPosition.x, originalLeftPadPosition.y, currentPosition.z);
                }
                else if (CompareTag("RightPad") && !isLeftScreenPart)
                {
                    transform.position = new Vector3(originalRightPadPosition.x, originalLeftPadPosition.y, currentPosition.z);
                }
            }
        }
    }
}
