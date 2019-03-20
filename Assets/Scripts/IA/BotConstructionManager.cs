using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotConstructionManager : MonoBehaviour
{
    int currentSize;
    int currentSide;
    int currentIndex;
    Vector3 lastCorner;
    [SerializeField]
    float buildingDistance;
    Vector3 lastBuildingPos;

    string[] buildingList = new string[]
    {
        "Buildings/Combat Station/Combat Station",
        "Buildings/Conquest Station/Conquest Station",
        "Buildings/Energy Farm/Energy Farm",
        "Buildings/Farm/Farm",
        "Buildings/House/House",
        "Buildings/Laboratory/Laboratory",
        "Buildings/Radar/Radar",
        "Buildings/TownHall/TownHall",
        "Buildings/Turrel/Turrel"
    };

    public void Construct(int index, BuilderUnit builder)
    {
        ConstructedUnit building = ((GameObject)Resources.Load(buildingList[index])).GetComponent<ConstructedUnit>();
        GameObject obj = InstanceManager.instanceManager.InstantiateUnit(building.GetConstructorPath(), GenerateNewPos(), Quaternion.identity);
        obj.GetComponent<InConstructionUnit>().Init(building);
        GetComponent<BotManager>().Pay(building.costs, building.pop);
        builder.Build(obj.GetComponent<InConstructionUnit>());
    }

    public void InitPos(Vector3 home)
    {
        currentSize = 1;
        currentSide = 3;
        currentIndex = 0;
        lastBuildingPos = home;
        lastCorner = home;
    }

    public Vector3 GenerateNewPos()
    {
        currentIndex++;
        if (currentIndex >= currentSize)
        {
            currentSide++;
            currentIndex = (currentSide == 1 || currentSide == 2) ? 1 : 2;       
        }
        if (currentSide >= 4)
        {
            currentIndex = 0;
            currentSide = 0;
            currentSize += 2;
            lastCorner = lastCorner - new Vector3(buildingDistance, 0, buildingDistance);
            lastBuildingPos = lastCorner - new Vector3(0, 0, buildingDistance);
        }
        print(currentIndex);

        switch (currentSide)
        {
            case 0:
                lastBuildingPos.z += buildingDistance;
                break;
            case 1:
                lastBuildingPos.x += buildingDistance;
                break;
            case 2:
                lastBuildingPos.z -= buildingDistance;
                break;
            case 3:
                lastBuildingPos.x -= buildingDistance;
                break;
        }
        return lastBuildingPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BuilderUnit tmp = GetComponent<IAManager>().GetJoblessBuilder();
            if (tmp != null)
            {
                Construct(0, tmp);
            }
        }
    }
}
