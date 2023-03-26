using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var col = FindObjectsByType<MeshRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var item in col)
        {
            if (item.gameObject.tag != "Player")
            {
                if (item.GetComponent<Collider>() == null)
                    item.gameObject.AddComponent<MeshCollider>();
            }
        }

    }
}
