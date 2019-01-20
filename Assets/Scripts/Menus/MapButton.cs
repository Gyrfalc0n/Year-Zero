using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour {

    public Text text;
    private CreateGameMenu parent;

    public void Selected()
    {
        parent = GetComponentInParent<CreateGameMenu>();
        parent.SelectMap(transform);
    }
}
