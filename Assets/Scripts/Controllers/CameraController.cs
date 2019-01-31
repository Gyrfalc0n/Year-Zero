using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 pos;
    [SerializeField]
    int panSpeed;
    [SerializeField]
    int border;

    float limitZ;
    float limitX;
    [SerializeField]
    Transform ground;

    PlayerController playerController;

    void Awake()
    {
        playerController = PlayerController.playerController;
        pos = transform.position;
        limitX = ground.localScale[0]/2 - 5;
        limitZ = ground.localScale[2]/2 - 5;
    }

    void Update()
    {
        if (!SelectUnit.selectUnit.isSelecting &&
            playerController.CameraAvailable())
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

    public void LookTo(Vector3 obj)
    {
        float modulo = Mathf.Abs(Camera.main.transform.position.y / Mathf.Sin(Mathf.Deg2Rad *Camera.main.transform.eulerAngles.x) );
        Camera.main.transform.position = new Vector3(obj.x, Camera.main.transform.position.y,
            obj.z - modulo * Mathf.Cos(Mathf.Deg2Rad * Camera.main.transform.eulerAngles.x));
        pos = Camera.main.transform.position;
    }
}
