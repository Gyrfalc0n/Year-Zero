using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FormationSystem : MonoBehaviour
{
    SelectUnit u;

    [SerializeField]
    float unitDistance;

    Formation formation;

    void Start()
    {
        formation = Formation.SQUARE;
        u = SelectUnit.selectUnit;
    }

    void SquareFormation(Vector3 dest)
    {
        float x = dest.x;
        for (int i = 0; i < u.selected.Count; i += 5)
        {
            u.selected[i].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z));
            if (i + 1 < u.selected.Count)
                u.selected[i + 1].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z + unitDistance));
            if (i + 2 < u.selected.Count)
                u.selected[i + 2].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z - unitDistance));
            if (i + 3 < u.selected.Count)
                u.selected[i + 3].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z + unitDistance*2));
            if (i + 4 < u.selected.Count)
                u.selected[i + 4].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, u.selected[i].transform.position.y, dest.z - unitDistance*2));
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
