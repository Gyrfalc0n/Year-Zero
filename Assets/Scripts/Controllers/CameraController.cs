using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 pos;
    private int panSpeed = 40;
    public int border;

    private void Awake()
    {
        pos = transform.position;
    }

    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - border)
        {
            pos.z += panSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= border)
        {
            pos.z -= panSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= border)
        {
            pos.x -= panSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - border)
        {
            pos.x += panSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
