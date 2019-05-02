using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoblessConstructorsPanel : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    int clickCounter;

    [SerializeField]
    CameraController cam;

    List<BuilderUnit> builders = new List<BuilderUnit>();

    public void Add(BuilderUnit unit)
    {
        if (builders.Contains(unit))
            Debug.Log("wtf");
        else
            builders.Add(unit);
        UpdateButton();
    }

    public void Remove(BuilderUnit unit)
    {
        if (!builders.Contains(unit))
            Debug.Log("wtf");
        else
            builders.Remove(unit);
        UpdateButton();
    }

    public void UpdateButton()
    {
        button.SetActive(builders.Count > 0);
    }

    public void OnClicked()
    {
        clickCounter = (clickCounter+1 >= builders.Count) ? 0 : ++clickCounter;
        SelectUnit.selectUnit.ClearSelection();
        SelectUnit.selectUnit.SelectObject(builders[clickCounter]);
        SelectUnit.selectUnit.UpdateUI();
        cam.LookTo(builders[clickCounter].transform.position);
    }
}
