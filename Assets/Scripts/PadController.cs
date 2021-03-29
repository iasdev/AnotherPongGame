using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public float speed;
    public float topDownLimit = 5.5f;

    void Update()
    {
        if (CompareTag("LeftPad"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * 1 * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * 1 * speed * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * 1 * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.back * 1 * speed * Time.deltaTime);
            }
        }

        if (transform.position.z > topDownLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topDownLimit);
        }
        else if (transform.position.z < -topDownLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -topDownLimit);
        }
    }
}
