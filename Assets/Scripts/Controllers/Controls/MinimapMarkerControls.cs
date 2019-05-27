using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MinimapMarkerControls : PlayerControls
{
    [SerializeField]
    Transform minimapPanel;
    [SerializeField]
    Marker markerPrefab;
    [SerializeField]
    Camera minimapCam;
    [SerializeField]
    Transform ground;

    void Start()
    {
        minimapCam.orthographicSize = ground.localScale[0] / 2;
    }

    public override void Update()
    {
        if (!CanUpdate()) return;

        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            LeftClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
    }

    public override void LeftClick()
    {
        Vector3 pos = Vector3.zero;
        bool mark = false;
        if (GetComponent<MinimapController>().MouseOnMinimap())
        { 
            pos = Input.mousePosition;
            mark = true;
        }
        else if (!GetComponent<MovementControls>().MouseOverUI())
        {
            pos = GetComponent<MinimapController>().MouseWorldSpaceToMinimap();
            mark = true;
        }
        if (mark)
        {
            CreateMarker(pos);
        }
        Cancel();
    }

    public void CreateMarker(Vector3 pos)
    {
        RPCCreateMarker(pos);
        if (!PhotonNetwork.OfflineMode)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Hashtable tmp = player.CustomProperties;
                if ((int)tmp["Team"] == InstanceManager.instanceManager.GetTeam() && player != PhotonNetwork.LocalPlayer)
                {
                    photonView.RPC("RPCCreateMarker", player, pos);
                }
            }
        }
    }

    [PunRPC]
    public void RPCCreateMarker(Vector3 pos)
    {
        Marker tmp = Instantiate(markerPrefab, minimapPanel);
        tmp.Init(pos);
    }
}
