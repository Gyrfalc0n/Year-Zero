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
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            selectionBoxSprite.gameObject.SetActive(false);
        }

        if (isSelecting && Input.GetMouseButton(0))
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
}
