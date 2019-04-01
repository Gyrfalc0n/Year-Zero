using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlacementGrid : MonoBehaviourPunCallbacks {

    [SerializeField]
    private float cellSize;
    [SerializeField]
    GameObject detectionCell;
    [SerializeField]
    LayerMask groundLayer;
    ConstructedUnit associatedBuilding;

    BuilderUnit builder;

    List<DetectionCell> cells = new List<DetectionCell>();

    float lines;
    float columns;

    public void Init(ConstructedUnit building, BuilderUnit builder)
    {
        this.builder = builder;
        associatedBuilding = building;
        this.lines = building.lines;
        this.columns = building.columns;
        CreateGrid();
        CreateGhost();
    }

    private void Update()
    {
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        FollowMouse();

        if (AllCellsAvailable() && Input.GetMouseButtonUp(0))
        {
            Construct();
        }
    }

    private void CreateGrid()
    {
        Vector3 vec = transform.position;
        for (int i = 0; i < lines; i++, vec.x += cellSize)
        {
            vec.z = transform.position.z;
            for (int j = 0; j < columns; j++, vec.z += cellSize)
            {
                GameObject obj = Instantiate(detectionCell, vec, Quaternion.identity, transform);
                cells.Add(obj.GetComponent<DetectionCell>());
            }
        }
    }

    private void CreateGhost()
    {
        Instantiate(associatedBuilding.GetGhost(), GetCenter(), Quaternion.identity, transform);
    }

    public void FollowMouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = GridFix(SetNewCenterTo(hit.point));
        }
    }

    private Vector3 SetNewCenterTo(Vector3 newPos)
    {
        return new Vector3((newPos.x + cellSize / 2) - (lines / 2 * cellSize),
            newPos.y + 0.01f,
            (newPos.z + cellSize / 2) - (columns * cellSize / 2));
    }

    //Create Grid Effect
    private Vector3 GridFix(Vector3 pos)
    {
        float newX = Mathf.FloorToInt(pos.x / cellSize) * cellSize;
        float newZ = Mathf.FloorToInt(pos.z / cellSize) * cellSize;

        return new Vector3(newX, pos.y, newZ);
    }

    private bool AllCellsAvailable()
    {
        bool allAvailable = true;
        foreach (DetectionCell cell in cells)
        {
            if (!cell.CheckAvailability())
                allAvailable = false;
        }
        return allAvailable;
    }

    private void Construct()
    {
        GameObject obj = InstanceManager.instanceManager.InstantiateUnit(associatedBuilding.GetConstructorPath(), GetCenter(), Quaternion.identity, -1);
        obj.GetComponent<InConstructionUnit>().Init(associatedBuilding);
        PlayerManager.playerManager.Pay(associatedBuilding.costs, associatedBuilding.pop);
        builder.Build(obj.GetComponent<InConstructionUnit>());
        Destroy(this.gameObject);
    }

    private Vector3 GetCenter()
    {
        return new Vector3((transform.position.x - cellSize / 2) + (lines / 2 * cellSize),
            transform.position.y + 0.01f,
            (transform.position.z - cellSize / 2) + (columns * cellSize / 2));
    }
}
