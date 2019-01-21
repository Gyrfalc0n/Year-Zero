using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    RectTransform img;

    float camTop;
    float camBottom;
    float camLeft;
    float camRight;

    void Start()
    {
        camTop = img.position.y + img.sizeDelta[1] / 2;
        camBottom = img.position.y - img.sizeDelta[1] / 2;
        camLeft = img.position.x - img.sizeDelta[0] / 2;
        camRight = img.position.x + img.sizeDelta[0] / 2;
    }

    void Update()
    {
        //print(MouseOnMinimap());
    }

    bool MouseOnMinimap()
    {
        return (Input.mousePosition.x >= camLeft && Input.mousePosition.x <= camRight &&
            Input.mousePosition.y >= camBottom && Input.mousePosition.y <= camTop);
    }
}
