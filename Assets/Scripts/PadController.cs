using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public GameManager gameManager;
    
    public float speed;
    public float topDownLimit = 5.5f;

    private Camera _camera;
    private int _halfScreenPosX;
    private Vector3 _originalLeftPadPosition;
    private Vector3 _originalRightPadPosition;

    private void Start()
    {
        _camera = Camera.main;
        _halfScreenPosX = Screen.width / 2;
        _originalLeftPadPosition = GameObject.FindGameObjectWithTag("LeftPad").transform.position;
        _originalRightPadPosition = GameObject.FindGameObjectWithTag("RightPad").transform.position;
    }

    private void Update()
    {
        if (gameManager.IsGameStarted())
        {
            HandleDesktopMovement();
            HandleMobileMovement();

            var transformPosition = transform.position;
            if (transformPosition.z > topDownLimit)
            {
                transform.position = new Vector3(transformPosition.x, transformPosition.y, topDownLimit);
            }
            else if (transformPosition.z < -topDownLimit)
            {
                transform.position = new Vector3(transformPosition.x, transformPosition.y, -topDownLimit);
            }
        }
    }

    private void HandleDesktopMovement()
    {
        if ((CompareTag("LeftPad") && Input.GetKey(KeyCode.W)) || (CompareTag("RightPad") && Input.GetKey(KeyCode.UpArrow))) {
            transform.Translate(Vector3.forward * (1 * speed * Time.deltaTime));
        } else if ((CompareTag("LeftPad") && Input.GetKey(KeyCode.S)) || (CompareTag("RightPad") && Input.GetKey(KeyCode.DownArrow))) {
            transform.Translate(Vector3.back * (1 * speed * Time.deltaTime));
        }
    }

    private void HandleMobileMovement()
    {
        var touches = Input.touches;

        foreach (var touch in touches)
        {
            var isLeftScreenPart = touch.position.x < _halfScreenPosX;
            
            if (touch.phase == TouchPhase.Moved)
            {
                var currentTap = new Vector3(touch.position.x, touch.position.y, 1);
                var currentPosition = _camera.ScreenToWorldPoint(currentTap);

                if (CompareTag("LeftPad") && isLeftScreenPart)
                {
                    transform.position = new Vector3(_originalLeftPadPosition.x, _originalLeftPadPosition.y, currentPosition.z);
                }
                else if (CompareTag("RightPad") && !isLeftScreenPart)
                {
                    transform.position = new Vector3(_originalRightPadPosition.x, _originalLeftPadPosition.y, currentPosition.z);
                }
            }
        }
    }
}
