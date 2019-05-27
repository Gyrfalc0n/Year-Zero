using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 pos;
    Vector3 rot;
    [SerializeField]
    int panSpeed;
    [SerializeField]
    int border;

    [SerializeField]
    float rotMax;
    [SerializeField]
    float rotMin;

    [SerializeField]
    float maxHeight;
    [SerializeField]
    float minHeight;

    float limitZ;
    float limitX;
    [SerializeField]
    Transform ground;

    MinimapController minimapController;

    void Start()
    {
        minimapController = InstanceManager.instanceManager.gameObject.GetComponent<MinimapController>();
        pos = transform.position;
        rot = transform.rotation.eulerAngles;
        limitX = ground.localScale[0]/2 - 5;
        limitZ = ground.localScale[2]/2 - 5;
    }

    void Update()
    {
        if (PlayerController.playerController.CameraAvailable())
            CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            pos.z += PlayerPrefs.GetFloat("camMoveKeySpeed") * 2 * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= PlayerPrefs.GetFloat("camMoveKeySpeed") * 2 * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            pos.x -= PlayerPrefs.GetFloat("camMoveKeySpeed") * 2 * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += PlayerPrefs.GetFloat("camMoveKeySpeed") * 2 * panSpeed * Time.deltaTime;
        }

        if (PlayerPrefs.GetInt("camMoveMouse") == 1)
        {
            if (Input.mousePosition.y >= Screen.height - border)
            {
                pos.z += PlayerPrefs.GetFloat("camMoveMouseSpeed") * 2 * panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y <= border)
            {
                pos.z -= PlayerPrefs.GetFloat("camMoveMouseSpeed") * 2 * panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= border)
            {
                pos.x -= PlayerPrefs.GetFloat("camMoveMouseSpeed") * 2 * panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - border)
            {
                pos.x += PlayerPrefs.GetFloat("camMoveMouseSpeed") * 2 * panSpeed * Time.deltaTime;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.KeypadPlus))
        {
            rot.x -= 2;
            pos.y -= 0.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.KeypadMinus))
        {
            rot.x += 2;
            pos.y += 0.5f;
        }
        rot.x = Mathf.Clamp(rot.x, rotMin, rotMax);
        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);

        pos.z = Mathf.Clamp(pos.z, -limitZ, limitZ);
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);


        bool input = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) ||
    Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.KeypadMinus) ||
    PlayerPrefs.GetInt("camMoveMouse") == 1 && (Input.mousePosition.y >= Screen.height - border || Input.mousePosition.y <= border
    || Input.mousePosition.x <= border || Input.mousePosition.x >= Screen.width - border);
        if (input)
        {
            transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
            minimapController.UpdateMinimapSquare();
        }
    }

    public void LookTo(Vector3 obj)
    {
        float modulo = Mathf.Abs(Camera.main.transform.position.y / Mathf.Sin(Mathf.Deg2Rad *Camera.main.transform.eulerAngles.x) );
        Camera.main.transform.position = new Vector3(obj.x, Camera.main.transform.position.y,
            obj.z - modulo * Mathf.Cos(Mathf.Deg2Rad * Camera.main.transform.eulerAngles.x));
        pos = Camera.main.transform.position;
    }
}
