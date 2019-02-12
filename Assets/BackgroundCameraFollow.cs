using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        GetComponent<RectTransform>().position = new Vector2(
        (x / Screen.width) * 10,
        (y / Screen.height) * 10
        );
    }
}