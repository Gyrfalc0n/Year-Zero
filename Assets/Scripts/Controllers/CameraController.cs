using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 pos;
    [SerializeField]
    int panSpeed;
    [SerializeField]
    int border;

    [SerializeField]
    int limitZ;
    [SerializeField]
    int limitX;

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
            pos.z = Mathf.Clamp(pos.z, -limitZ, limitZ);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= border)
        {
            pos.z -= panSpeed * Time.deltaTime;
            pos.z = Mathf.Clamp(pos.z, -limitZ, limitZ);
        }
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= border)
        {
            pos.x -= panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - border)
        {
            pos.x += panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        }
        transform.position = pos;
    }

    public void LookTo(Transform obj)
    {

    }
}
