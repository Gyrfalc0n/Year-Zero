using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementControls : PlayerControls
{
    [SerializeField]
    CameraController cam;
    [SerializeField]
    LayerMask groundLayer;

    MinimapController minimapController;

    void Start()
    {
        minimapController = GetComponent<MinimapController>();
    }

    public override void RightClick()
    {
        if (SelectUnit.selectUnit.selected.Count == 0 || !SelectUnit.selectUnit.selected[0].photonView.IsMine || SelectUnit.selectUnit.selected[0].botIndex != -1)
            return;
        if (SelectUnit.selectUnit.selected[0].GetComponent<ProductionBuilding>() != null && !MouseOverUI())
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
            if (SelectUnit.selectUnit.selected[0].GetComponent<MovableUnit>() != null)
                MoveToMousePoint();
        }
    }

    public override void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
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
            else if (minimapController.MouseOnMinimap())
            {
                if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                {
                    MoveCamera(minimapController.MinimapToWorldSpaceCoords());
                    minimapController.UpdateMinimapSquare();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    RightClick();
                }
            }
        }

        SelectUnit.selectUnit.UpdateSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetComponent<PlayerController>().InitChatPanelControls();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            GetComponent<PlayerController>().InitHelpPanelControls();
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            GetComponent<PlayerController>().InitPauseControls();
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            GetComponent<PlayerController>().InitAlliesPanelControls();
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            GetComponent<PlayerController>().InitChatMenuPanelControls();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<PlayerController>().InitSkilltreePanelControls();
        }
    }

    public void MoveToMousePoint()
    {
        RaycastHit hit;
        if (minimapController.MouseOnMinimap())
        {
            Vector3 tmp = minimapController.MinimapToWorldSpaceCoords();
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

    void MoveCamera(Vector3 vec)
    {
        cam.LookTo(vec);
    }
}
