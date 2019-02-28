using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpellZone : MonoBehaviour
{
    [SerializeField]
    Transform spellholder;
    DestroyerSupportSpell spell;

    List<MovableUnit> units = new List<MovableUnit>();

    bool wasActivated = false;

    float lifeBoost = 1;
    float atkBoost = 1;

    private void Start()
    {
        foreach (Transform spell in spellholder)
        {
            if (spell.GetComponent<DestroyerSupportSpell>() != null)
            {
                this.spell = spell.GetComponent<DestroyerSupportSpell>();
            }
        }
    }

    private void Update()
    {
        if (wasActivated && !spell.Activated())
        {
            wasActivated = false;
            for (int i = units.Count-1; i>=0; i--)
            {
                units[i].RemoveBoost();
            }
        }
        if (!wasActivated && spell.Activated())
        {
            wasActivated = true;
            for (int i = units.Count - 1; i >= 0; i--)
            {
                units[i].AddBoost(atkBoost, lifeBoost);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovableUnit>() != null && !InstanceManager.instanceManager.IsEnemy(other.GetComponent<MovableUnit>().photonView.Owner) && !units.Contains(other.GetComponent<MovableUnit>()))
        {
            units.Add(other.GetComponent<MovableUnit>());
            if (spell.Activated())
                other.GetComponent<MovableUnit>().AddBoost(atkBoost, lifeBoost);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovableUnit>() != null && units.Contains(other.GetComponent<MovableUnit>()))
        {
            other.GetComponent<MovableUnit>().RemoveBoost();
            units.Remove(other.GetComponent<MovableUnit>());
        }
    }
}
