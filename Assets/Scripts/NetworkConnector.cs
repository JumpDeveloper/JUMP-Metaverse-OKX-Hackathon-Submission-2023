using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsHeadlessMode())
        {
            NetworkManager.singleton.StartClient();
            print("connected to client");
        }
    }

    public bool IsHeadlessMode()
    {
        return UnityEngine.SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
    }
}
