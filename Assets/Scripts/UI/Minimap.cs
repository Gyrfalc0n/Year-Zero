using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    RectTransform img;

    [SerializeField]
    Canvas cnvs;

    [SerializeField]
    Transform ground;

    float camTop;
    float camBottom;
    float camLeft;
    float camRight;

    float scaleX;
    float scaleZ;

    [SerializeField]
    CameraController cam;
    [SerializeField]
    LayerMask groundLayer;

    void Start()
    {
        scaleX = ground.localScale.x / (img.rect.width * cnvs.scaleFactor);
        scaleZ = ground.localScale.z / (img.rect.height * cnvs.scaleFactor);
        camTop = img.position.y + img.rect.height * cnvs.scaleFactor / 2;
        camBottom = img.position.y - img.rect.height * cnvs.scaleFactor / 2;
        camLeft = img.position.x - img.rect.width * cnvs.scaleFactor / 2;
        camRight = img.position.x + img.rect.width * cnvs.scaleFactor / 2;
    }

    void Update()
    {
        if (MouseOnMinimap() && Input.GetMouseButtonDown(0))
        {
            MoveCamera();
        }
    }

    public bool MouseOnMinimap()
    {
        Vector3 vec = Input.mousePosition;
        return (vec[0] >= camLeft && vec[0] <= camRight &&
            vec[1] >= camBottom && vec[1] <= camTop);
    }

    void MoveCamera()
    {
        float x = (Input.mousePosition.x - img.rect.width * cnvs.scaleFactor / 2 - camLeft) * scaleX;
        float z = (Input.mousePosition.y - img.rect.height * cnvs.scaleFactor / 2 - camBottom) * scaleZ;

        Vector3 vec = new Vector3(x, 0, z);
        cam.LookTo(vec);
    }
}