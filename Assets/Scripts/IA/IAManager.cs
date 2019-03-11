using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class IAManager : InstanceManager
{
    public void Init(int index, int race, int team, int color, int coords)
    {
        botIndex = index;
        this.race = race;
        this.race = race;
        this.race = race;
    }

    void InitStartingTroops(Vector3 coords)
    {
        //BotManager.BotManager.AddHome(InstantiateUnit(townhalls[race], new Vector3(coords.x + 2, 0.5f, coords.z + 2), Quaternion.Euler(0, 0, 0)).GetComponent<TownHall>());
        InstantiateUnit(builders[race], coords, Quaternion.Euler(0, 0, 0));
    }

    public override GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
        if (obj.GetComponent<SelectableObj>() != null)
        {
            obj.GetComponent<SelectableObj>().SetBotIndex(botIndex);
        }
        return obj;
    }

    public override void OnLeftRoom() { }

    public override void OnDisconnected(DisconnectCause cause) { }

    public override void OnMasterClientSwitched(Player newMasterClient) { }

    public override void OnPlayerLeftRoom(Player otherPlayer) { }
}
