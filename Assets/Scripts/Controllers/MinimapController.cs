using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    RectTransform img;
    [SerializeField]
    RectTransform minimapSquare;

    [SerializeField]
    Canvas cnvs;

    [SerializeField]
    Transform ground;

    [SerializeField]
    LayerMask fakeGroundLayer;

    float camTop;
    float camBottom;
    float camLeft;
    float camRight;

    float scaleX;
    float scaleZ;

    float mainCamWidth;
    float mainCamHeight;

    private void Start()
    {
        scaleX = ground.localScale.x / (img.rect.width * cnvs.scaleFactor);
        scaleZ = ground.localScale.z / (img.rect.height * cnvs.scaleFactor);
        camTop = img.position.y + img.rect.height * cnvs.scaleFactor / 2;
        camBottom = img.position.y - img.rect.height * cnvs.scaleFactor / 2;
        camLeft = img.position.x - img.rect.width * cnvs.scaleFactor / 2;
        camRight = img.position.x + img.rect.width * cnvs.scaleFactor / 2;
        SetSquareSize();
    }

    public void UpdateMinimapSquare()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
        {
            minimapSquare.position = WorldSpaceToMinimap(hit.point);
        }
    }

    public bool MouseOnMinimap()
    {
        Vector3 vec = Input.mousePosition;
        return (vec[0] >= camLeft && vec[0] <= camRight &&
            vec[1] >= camBottom && vec[1] <= camTop);
    }

    public Vector3 MinimapToWorldSpaceCoords()
    {
        float x = (Input.mousePosition.x - img.rect.width * cnvs.scaleFactor / 2 - camLeft) * scaleX;
        float z = (Input.mousePosition.y - img.rect.height * cnvs.scaleFactor / 2 - camBottom) * scaleZ;

        return new Vector3(x, 0, z);
    }

    public Vector3 MouseWorldSpaceToMinimap()
    {
        float x = 0;
        float z = 0;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Vector3 tmp = WorldSpaceToMinimap(hit.point);
            x = tmp.x;
            z = tmp.y;
        }
        else
        {
            Debug.Log("Error");
        }
        return new Vector3(x, z, 0);
    }

    public Vector3 WorldSpaceToMinimap(Vector3 pos)
    {
        float x = (pos.x / scaleX) + img.rect.width * cnvs.scaleFactor / 2 + camLeft;
        float z = (pos.z / scaleZ) + img.rect.height * cnvs.scaleFactor / 2 + camBottom;
        return new Vector3(x, z, 0);
    }

    void SetSquareSize()
    {
        float top;
        float left;
        float bottom;
        float right;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 1, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
        {
            print("a");
            top = hit.point.z;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(1, 0, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
            {
                print("b");
                right = hit.point.x;
                bottom = hit.point.z;

                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0, 0, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
                {
                    left = hit.point.x;

                    print(top);
                    print(bottom);
                    print(scaleZ);
                    mainCamHeight = (top - bottom) / scaleZ;
                    mainCamWidth = (right - left) / scaleX;
                }
            }
        }
        minimapSquare.sizeDelta = new Vector2(mainCamWidth, mainCamHeight);
    }
}
