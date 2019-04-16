using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerTools : MonoBehaviour
{
    public static int GetTeamOf(SelectableObj unit)
    {
        int res;
        if (PhotonNetwork.OfflineMode)
        {
            if (unit.botIndex == -1)
            {
                res = InstanceManager.instanceManager.GetTeam();
            }
            else if (unit.botIndex == -2)
            {
                res = -2;
            }
            else
            {
                res = InstanceManager.instanceManager.GetBot(unit.botIndex).GetTeam();
            }
        }
        else
        {
            if (unit.botIndex == -1)
            {
                res = (int)unit.photonView.Owner.CustomProperties["Team"];
            }
            else if (unit.botIndex == -2)
            {
                res = -2;
            }
            else
            {
                res = (int)PhotonNetwork.MasterClient.CustomProperties["Team" + unit.botIndex];
            }
        }
        return res;
    }

    public static string GetHolderOf(SelectableObj unit)
    {
        string res = "Holder";
        if (PhotonNetwork.OfflineMode)
        {
            if (unit.botIndex == -1)
            {
                res += "Player";
            }
            else
            {
                res += unit.botIndex.ToString();
            }
        }
        else
        {
            if (unit.botIndex == -1)
            {
                res += unit.photonView.Owner.NickName;
            }
            else
            {
                res += unit.botIndex.ToString();
            }
        }
        return res;
    }
}
