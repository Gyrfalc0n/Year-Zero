using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class PlayerControls : MonoBehaviourPunCallbacks
{
    public bool isActive { get; protected set; }

    public PlayerControls Activate()
    {
        isActive = true;
        Init();
        return this;
    }

    public virtual void Init() { }

    public virtual void Update()
    {
        if (!isActive) return;

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
    }

    public virtual void LeftClick()
    {

    }

    public virtual void RightClick()
    {
        Cancel();
    }
    
    public virtual void Cancel()
    {
        isActive = false;
    }

    public bool MouseOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = raycastResultList.Count - 1; i >= 0; i--)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseThrough>() != null)
            {
                raycastResultList.RemoveAt(i);
            }
        }
        return raycastResultList.Count > 0;
    }
}
