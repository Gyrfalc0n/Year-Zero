using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerTools : MonoBehaviour
{
    public static bool IsMine(SelectableObj unit)
    {
        return unit.photonView.IsMine && unit.botIndex == -1;
    }

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

    public static Color32 GetColorOf(SelectableObj unit)
    {
        int res;
        if (PhotonNetwork.OfflineMode)
        {
            if (unit.botIndex == -1)
            {
                return InstanceManager.instanceManager.GetColor();
            }
            else if (unit.botIndex == -2)
            {
                res = 1;
            }
            else
            {
                return InstanceManager.instanceManager.GetBot(unit.botIndex).GetColor();
            }
        }
        else
        {
            if (unit.botIndex == -1)
            {
                res = (int)unit.photonView.Owner.CustomProperties["Color"];
            }
            else if (unit.botIndex == -2)
            {
                res = -2;
            }
            else
            {
                res = (int)PhotonNetwork.MasterClient.CustomProperties["Color" + unit.botIndex];
            }
        }
        return Int2Color(res);
    }

    public static Color32 Int2Color(int val)
    {
        Color32 res;
        if (val == 0)
        {
            res = new Color32(255, 0, 0, 255);
        }
        else if (val == 1)
        {
            res = new Color32(0, 255, 0, 255);
        }
        else
        {
            res = new Color32(0, 0, 255, 255);
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
