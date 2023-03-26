using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalPlayerBehavior : MonoBehaviour
{
    private void Start()
    {
        VegasLoader.instance.LoadVegas();
        // gameObject.name = GetComponent<NetworkIdentity>().netId.ToString();
       // gameObject.tag = (isLocalPlayer) ? "Player" : "Enemy";
    }

    #region Player Skinning

    [SyncVar(hook = (nameof(SkinValChanged)))]
    private int syc_isMale = 3;

    private void SkinValChanged(int oldVal, int newVal)
    {
        //print("here"+newVal);
        PutPlayerSkin(newVal);
    }

    [Header("Player Skin")]
    [SerializeField]
    private GameObject female_obj;
    [SerializeField]
    private GameObject male_obj;
    [SerializeField]
    private Avatar female_avtr, male_avtr;

    private void PutPlayerSkin(int IsMale)
    {
        if (IsMale == 0)
        {
            female_obj.SetActive(true);
            male_obj.SetActive(false);
            GetComponent<Animator>().avatar = female_avtr;
        }
        else
        {
            female_obj.SetActive(false);
            male_obj.SetActive(true);
            GetComponent<Animator>().avatar = male_avtr;
        }
    }

    internal void SetPlayerSkin(int IsMale)
    {
        //print(IsMale);
        syc_isMale = IsMale;
        //PutPlayerSkin(IsMale);
        //Cmd_SetPlayerSKin(IsMale);
    }

    #endregion Player Skinning

    #region Name Bubble

    [Header("Name Bubble")]
    [SerializeField]
    private GameObject nameBubble;

    [SyncVar(hook = nameof(DisplayNameChanged))]
    private string displayName;

    [Command(requiresAuthority = false)]
    private void Cmd_SetDisplayName(string dName)
    {
        displayName = dName;
    }
    private void DisplayNameChanged(string oldVal, string newVal)
    {
        nameBubble.GetComponent<TMP_Text>().text = newVal;
    }

    private void Update()
    {
        if (nameBubble != null && !string.IsNullOrEmpty(displayName))
        {
            if (Camera.main == null)
                return;

            nameBubble.transform.LookAt(Camera.main.transform);
            nameBubble.transform.localEulerAngles = new Vector3(nameBubble.transform.localEulerAngles.x,
                                                            nameBubble.transform.localEulerAngles.y + 180,
                                                            nameBubble.transform.localEulerAngles.z);

        }

        //if (!isLocalPlayer)
        //    return;
            //if(transform.position.y < 0 || transform.position.y > 10)
            //{
            //    transform.position = new Vector3(transform.position.x,
            //                                    0,
            //                                        transform.position.z);
            //}

            //print(transform.position.x);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -400, 400),
                                                Mathf.Clamp(transform.position.y, 0, 10),
                                                Mathf.Clamp(transform.position.z, -400, 500));
        
    }

    #endregion Name Bubble

    //public override void OnStartLocalPlayer()
    //{
    //    base.OnStartLocalPlayer();

    //    Cmd_SetDisplayName(PlayerPrefs.GetString(PPm.Username));

    //    VegasLoader.instance.LoadVegas();
    //}
}
