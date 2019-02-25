 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 
 public class MoveBack : MonoBehaviour
{

    Vector3 pz;
    public Vector3 StartPos;

    public int moveModifier;

    // Use this for initialization
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        pz.z = 0;
        //transform.position = pz;
        //Debug.Log("Mouse Position: " + pz);


        StartPos = new Vector3(GetComponent<RectTransform>().sizeDelta[0] / 2 * GetComponentInParent<Canvas>().scaleFactor - 50, StartPos.y,
            GetComponent<RectTransform>().sizeDelta[1] / 2 * GetComponentInParent<Canvas>().scaleFactor + 100);

        transform.position = new Vector3(StartPos.x + ((pz.x - 0.5f )* moveModifier), StartPos.y + (pz.y * moveModifier), 0);
        //move based on the starting position and its modified value.
    }

}