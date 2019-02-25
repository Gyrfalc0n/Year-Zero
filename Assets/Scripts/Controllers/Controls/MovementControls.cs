using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementControls : PlayerControls
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
    CameraController cam;
    [SerializeField]
    LayerMask groundLayer;
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

    void Start()
    {
        scaleX = ground.localScale.x / (img.rect.width * cnvs.scaleFactor);
        scaleZ = ground.localScale.z / (img.rect.height * cnvs.scaleFactor);
        camTop = img.position.y + img.rect.height * cnvs.scaleFactor / 2;
        camBottom = img.position.y - img.rect.height * cnvs.scaleFactor / 2;
        camLeft = img.position.x - img.rect.width * cnvs.scaleFactor / 2;
        camRight = img.position.x + img.rect.width * cnvs.scaleFactor / 2;

        SetSquareSize();
    }

    public override void RightClick()
    {
        if (SelectUnit.selectUnit.selected[0].GetComponent<ProductionBuilding>() != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
            {
                SelectUnit.selectUnit.selected[0].GetComponent<ProductionBuilding>().MoveBanner(hit.point);
            }
        }
        else if (SelectUnit.selectUnit.selected[0].GetComponent<InConstructionUnit>() != null)
        {
            return;
        }
        else if (!SelectUnit.selectUnit.InstantSelect())
        {
            MoveToMousePoint();
        }
    }

    public override void Update()
    {
        if (active)
        {
            UpdateMinimapSquare();
            if (!MouseOverUI())
            {
                if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                {
                    LeftClick();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    RightClick();
                }
                
            }
            else if (MouseOnMinimap())
            {
                if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                {
                    MoveCamera(MinimapToWorldSpaceCoords());
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    RightClick();
                }
            }
            SelectUnit.selectUnit.UpdateSelection();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GetComponent<PlayerController>().InitChatPanelControls();
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                //GetComponent<PlayerController>().InitQuestControls();
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                GetComponent<PlayerController>().InitPauseControls();
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                GetComponent<PlayerController>().InitAlliesPanelControls();
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                GetComponent<PlayerController>().InitChatMenuPanelControls();
            }
        }
    }

    public void MoveToMousePoint()
    {
        RaycastHit hit;
        if (MouseOnMinimap())
        {
            Vector3 tmp = MinimapToWorldSpaceCoords();
            tmp.y = 5;
            Ray ray = new Ray(tmp, Vector3.down);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                GetComponent<FormationSystem>().MoveSelection(hit.point);
            }
        }
        else if (!MouseOverUI())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
            {
                GetComponent<FormationSystem>().MoveSelection(hit.point);
            }
        }
    }

    public void StopSelection()
    {
        foreach (SelectableObj unit in SelectUnit.selectUnit.selected)
        {
            if (unit.GetComponent<MovableUnit>() != null)
            {
                unit.GetComponent<MovableUnit>().ResetAction();
            }
        }
    }


    public bool MouseOnMinimap()
    {
        Vector3 vec = Input.mousePosition;
        return (vec[0] >= camLeft && vec[0] <= camRight &&
            vec[1] >= camBottom && vec[1] <= camTop);
    }

    Vector3 MinimapToWorldSpaceCoords()
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
            x = (hit.point.x / scaleX) + img.rect.width * cnvs.scaleFactor / 2 + camLeft;
            z = (hit.point.z / scaleZ) + img.rect.height * cnvs.scaleFactor / 2 + camBottom;
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
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,1,0)), out hit, Mathf.Infinity, fakeGroundLayer))
        {
            top = hit.point.z;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(1, 0, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
            {
                right = hit.point.x;
                bottom = hit.point.z;

                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0, 0, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
                {
                    left = hit.point.x;

                    mainCamHeight = (top - bottom) / scaleZ;
                    mainCamWidth = (right - left) / scaleX;
                }
            }
        }
        minimapSquare.sizeDelta = new Vector2(mainCamWidth, mainCamHeight);
    }

    void UpdateMinimapSquare()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, Mathf.Infinity, fakeGroundLayer))
        {
            minimapSquare.position = new Vector3(hit.point.x / scaleX + img.rect.width * cnvs.scaleFactor / 2 + camLeft, 
                hit.point.z / scaleZ + img.rect.height * cnvs.scaleFactor / 2 + camBottom, 0);
        }
    }

    void MoveCamera(Vector3 vec)
    {
        cam.LookTo(vec);
    }
}
