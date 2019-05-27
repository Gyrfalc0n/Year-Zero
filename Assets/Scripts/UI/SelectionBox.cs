using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionBox : MonoBehaviour {

    [SerializeField]
    private RectTransform selectionBoxSprite;

    Vector3 startPos;
    Vector3 endPos;

    bool isSelecting;

    public void CheckBox()
    {
        if (Input.GetMouseButtonDown(0) && !MouseOverUI())
        {
            isSelecting = true;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            selectionBoxSprite.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0) && isSelecting)
        {
            if (!selectionBoxSprite.gameObject.activeInHierarchy)
            {
                selectionBoxSprite.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 center = (startPos + endPos) / 2f;
            selectionBoxSprite.position = center;

            float sizeX = Mathf.Abs(startPos.x - endPos.x);
            float sizeY = Mathf.Abs(startPos.y - endPos.y);

            selectionBoxSprite.sizeDelta = new Vector2(sizeX, sizeY);
        }
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
