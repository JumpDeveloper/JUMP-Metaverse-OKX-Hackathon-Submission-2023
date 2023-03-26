using Mirror;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private GameObject avtrPanel;

    private void Start()
    {
        mainCam = Camera.main.gameObject;
        followCam = GameObject.FindGameObjectWithTag("PlayerFollowCamera");

        avtrPanel = GameObject.FindGameObjectWithTag("AvatarPanel");
        avtrPanel.SetActive(!avtrPanel.activeInHierarchy);
    }

    internal void avtr_setGender(int _isMale)
    {
        CmdSetPlayerSkin(_isMale);
    }

    [Command]
    internal void CmdSetPlayerSkin(int IsMale)
    {
        syc_isMale = IsMale;
    }

    [SerializeField]
    private List<EraClothes> eraClothes = new List<EraClothes>();

    private int isMale = -9;

    internal void SetPlayerSkin(int IsMale)
    {
        syc_isMale = IsMale;
    }

    [SyncVar(hook = (nameof(SkinValChanged)))]
    private int syc_isMale = -3;
    [SyncVar(hook = (nameof(EraValChanged)))]
    private int syc_era = -3;

    private void EraValChanged(int oldVal, int newVal)
    {
        print("hereEra:" + newVal);
        //PutPlayerSkin(newVal);
        //isMale = newVal;

        PutPlayerSkin(newVal);    //hardcoded; avoid
    }

    private void SkinValChanged(int oldVal, int newVal)
    {
        print("here"+newVal);
        //PutPlayerSkin(newVal);
        isMale = newVal;

        if(isMale == 0)
            PutPlayerSkin(1980);    //hardcoded; avoid
        else
            PutPlayerSkin(1970);
    }

    //Called from VegasLoader
    internal void PutPlayerSkin(int year)
    {
        print(year);

        int ind = -1;

        for (int i=0;i<eraClothes.Count;i++)
        {
            foreach (var item1 in eraClothes[i].eras)
            {
                if(year == item1)
                {
                    ind = i;
                    break;
                }
            }
        }
        if (ind == -1)
            Debug.LogError("era not foudn");

        eraClothes.ForEach(x => {
            //if (isMale == 1) 
                x.male_obj.SetActive(false);
            //else 
                x.female_obj.SetActive(false);
        });

        if (isMale == 1)
        {
            eraClothes[ind].male_obj.SetActive(true);
            GetComponent<Animator>().avatar = eraClothes[ind].male_avtr;
        }
        else
        {
            eraClothes[ind].female_obj.SetActive(true);
            GetComponent<Animator>().avatar = eraClothes[ind].female_avtr;
        }
    }
    internal void Mintimage(string url)
    {
        GetComponent<CallMintNFT>().MintNFTJS(url);
    }
    #region Avatar UI

    [SerializeField]
    private GameObject avatrCam;

    private GameObject mainCam, followCam;

    private void LateUpdate()
    {
        if (!Input.GetKeyDown(KeyCode.Tab))
            return;

        var avtr = GameObject.FindGameObjectWithTag("avtr_instr");
        if(avtr != null) Destroy(avtr);

        //avtrPanel = GameObject.FindGameObjectWithTag("AvatarPanel");
        avtrPanel.SetActive(!avtrPanel.activeInHierarchy);

        if (!avtrPanel.activeInHierarchy)
        {
            avatrCam.SetActive(false);
            mainCam.SetActive(true);
            followCam.SetActive(true);

            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
            
            return;
        }

        avatrCam.SetActive(true);
        mainCam.SetActive(false);
        followCam.SetActive(false);

        GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;
        GetComponent<ThirdPersonController>().enabled = false;


        avtrPanel.GetComponent<AvatarPanel>().BeginAvtrPanel(isMale, eraClothes, this);
    }

    internal void UpdatePurchase(int yr_)
    {
        //print(yr_);

        var element = eraClothes.Find(x =>
        {
            int k = -1;
            foreach (var item in x.eras)
            {
                if (yr_ == item)
                {
                    k = item;
                    break;
                }
            }
            if(k==-1)
                return false;
            return true;
        });

        EraClothes era = new EraClothes()
        {
            eras = element.eras,
            female_obj = element.female_obj,
            female_avtr = element.female_avtr,
            female_sprite = element.female_sprite,
            male_obj = element.male_obj,
            male_avtr = element.male_avtr,
            male_sprite = element.male_sprite,
            amount = element.amount,
            isPurchasd = true
        };
        eraClothes.Remove(element);
        eraClothes.Add(era);
        //eraClothes[yr_ ] = era;
    }

    #endregion Avatar UI
}

[Serializable]
public struct EraClothes
{
    public List<int> eras;

    public GameObject female_obj;
    public Avatar female_avtr;
    public Sprite female_sprite;

    public GameObject male_obj;
    public Avatar male_avtr;
    public Sprite male_sprite;

    public bool isPurchasd;
    public float amount;

    public string female_url;
    public string male_url;
}