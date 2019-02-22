using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    [SerializeField]
    Transform spellHolder;
    [SerializeField]
    List<GameObject> mySpells = new List<GameObject>();
    public List<GameObject> spells = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject obj in mySpells)
        {
            GameObject tmp = Instantiate(obj, spellHolder);
            tmp.GetComponent<Spell>().associatedUnit = this;
            spells.Add(tmp);
        }
    }
}
