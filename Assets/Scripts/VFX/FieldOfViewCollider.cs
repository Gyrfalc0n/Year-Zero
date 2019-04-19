using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FieldOfViewCollider : MonoBehaviour
{
    public void SetRange(Vector3 vec)
    {
        transform.localScale = vec;
    }

    public Vector3 GetRange()
    {
        return transform.localScale;
    }

    private void Start()
    {
        Vector3 tmp = GetRange();
        if (GetComponentInParent<Radar>() != null)
        {
            tmp.x = SkilltreeManager.manager.radarRange;
            tmp.z = SkilltreeManager.manager.radarRange;
            SetRange(tmp);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (GetComponentInParent<SelectableObj>().botIndex == -1 && collision.GetComponent<SelectableObj>() != null)
        {
            if (MultiplayerTools.GetTeamOf(collision.GetComponent<SelectableObj>()) != InstanceManager.instanceManager.GetTeam())
                collision.GetComponent<SelectableObj>().UnHide();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<SelectableObj>() != null && collision.GetComponent<SelectableObj>() != null)
        {
            if (MultiplayerTools.GetTeamOf(collision.GetComponent<SelectableObj>()) != InstanceManager.instanceManager.GetTeam())
                collision.GetComponent<SelectableObj>().Hide();
        }
    }
}
