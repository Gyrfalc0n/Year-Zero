using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FormationSystem : MonoBehaviour
{
    SelectUnit u;
    [SerializeField]
    float unitDistance;
    float stoppingDistance = 1f;
    Formation formation;
    [HideInInspector]
    protected AudioManager audioManagerF;

    void Start()
    {
        audioManagerF = FindObjectOfType<AudioManager>();
        formation = Formation.SQUARE;
        u = SelectUnit.selectUnit;
    }

    void SquareFormation(Vector3 dest)
    {
        float x = dest.x;
        for (int i = 0; i < u.selected.Count; i += 5)
        {
            u.selected[i].GetComponent<MovableUnit>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z), stoppingDistance);
            if (i + 1 < u.selected.Count)
                u.selected[i + 1].GetComponent<MovableUnit>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z + unitDistance), stoppingDistance);
            if (i + 2 < u.selected.Count)
                u.selected[i + 2].GetComponent<MovableUnit>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z - unitDistance), stoppingDistance);
            if (i + 3 < u.selected.Count)
                u.selected[i + 3].GetComponent<MovableUnit>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z + unitDistance*2), stoppingDistance);
            if (i + 4 < u.selected.Count)
                u.selected[i + 4].GetComponent<MovableUnit>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z - unitDistance*2), stoppingDistance);
            x -= unitDistance;
        }
    }

    void NoFormation(Vector3 dest)
    {
        foreach (SelectableObj unit in SelectUnit.selectUnit.selected)
        {
            if (unit.GetComponent<MovableUnit>() != null)
                unit.GetComponent<MovableUnit>().SetDestination(dest, 1);
        }
    }

    public void MoveSelection(Vector3 newPos)
    {
        audioManagerF.PlaySound("MoveCommand");
        if (formation == Formation.NO)
            NoFormation(newPos);
        else if (formation == Formation.SQUARE)
            SquareFormation(newPos);

    }

    public void ToggleFormation()
    {
        if (formation == Formation.NO)
            formation = Formation.SQUARE;
        else if (formation == Formation.SQUARE)
            formation = Formation.NO;
    }

    public enum Formation
    {
        NO,
        SQUARE
    }
}
