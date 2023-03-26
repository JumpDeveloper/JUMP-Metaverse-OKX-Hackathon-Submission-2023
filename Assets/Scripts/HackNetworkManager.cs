using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<Character_ID>(CharacterIDSpawn);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        NetworkClient.Send(new Character_ID() { isMale = PlayerPrefs.GetInt(PPm.isMale) });
    }

    private void CharacterIDSpawn(NetworkConnectionToClient conn, Character_ID isMale)
    {
        GameObject obj = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (NetworkServer.AddPlayerForConnection(conn, obj))
        {
            obj.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
            obj.GetComponent<PlayerSkin>().SetPlayerSkin(isMale.isMale);
        }
    }
}

[Serializable]
public struct Character_ID : NetworkMessage
{
    public int isMale;
}