using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningSceneScripter : MonoBehaviour
{
    [SerializeField]
    private GameObject StartPanel, MAppPanel, PropertyPanel, WorldPanel, LoadingPanel;

    [SerializeField]
    private TMP_InputField username_IF;

    [SerializeField]
    private Button male_btn, female_btn;// confirm_btn;

    [SerializeField]
    private GameObject female_obj, male_obj;

    [SerializeField]
    private Avatar female_avtr, male_avtr;

    [SerializeField]
    private Animator ava_anim;

    [SerializeField]
    private string nextScnee = "1GameScene";

    [SerializeField]
    private TMP_Text walletid_txt;

    private int isMale = 0;

    private void Start()
    {
        ShowHideAllPanels(StartPanel);

        city_drpd.onValueChanged.AddListener(cityDrpdValChanged);
        yr_drpd.onValueChanged.AddListener(yr_drpdValChane);

        //confirm_btn.onClick.AddListener(ConfirmBtn_Clicked);

        username_IF.text = PlayerPrefs.GetString(PPm.Username, "");

        isMale = PlayerPrefs.GetInt(PPm.isMale, 1);
        ChangeGender(isMale);

        male_btn.onClick.AddListener(() => { if (isMale == 1) return; isMale = 1; ChangeGender(isMale); });
        female_btn.onClick.AddListener(() =>
        {
            if (isMale == 0) return; isMale = 0; ChangeGender(isMale);
        });

    }

    private void ChangeGender(int IsMale)
    {
        //0=female, 1=male
        switch (IsMale)
        {
            case 0:
                female_obj.SetActive(true);
                male_obj.SetActive(false);
                ava_anim.avatar = female_avtr;
                break;
            case 1:
                female_obj.SetActive(false);
                male_obj.SetActive(true);
                ava_anim.avatar = male_avtr;
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt(PPm.isMale, IsMale);
    }

    

    private void ShowHideAllPanels(GameObject showThis)
    {
        StartPanel.SetActive(false);
        MAppPanel.SetActive(false);
        PropertyPanel.SetActive(false);
        WorldPanel.SetActive(false);
        LoadingPanel.SetActive(false);

        showThis.SetActive(true);
    }

    //All the button calls are from editor
    public void ConnectWalletBtnClicked()
    {
        GetComponent<WebLogin>().OnLogin();
        //Assign walletidtxt herer and also in playerPrefs
        ShowHideAllPanels(MAppPanel);
    }

    public void MAppBtnClicked()
    {
        walletid_txt.text= PlayerPrefs.GetString("Account");
        ava_anim.gameObject.SetActive(true);
        ShowHideAllPanels(PropertyPanel);
    }

    public void Proprty_ConfirmBtn_Clicked()
    {
        //if (string.IsNullOrEmpty(username_IF.text))
        //    return;

        PlayerPrefs.SetString(PPm.Username, username_IF.text.ToString());

        //Debug.Log("Load scene" + username_IF.text);
        //SceneManager.LoadScene(nextScnee);
        ShowHideAllPanels(WorldPanel);
        ava_anim.gameObject.SetActive(false);
    }

    [SerializeField]
    private TMP_Dropdown city_drpd, yr_drpd;

    private string currCity = "LasVegas";
    private int currYr = 1900;

    public void Wrld_TravelBtnClicked()
    {
        ShowHideAllPanels(LoadingPanel);

        PlayerPrefs.SetString(PPm.Loadcity, currCity);
        PlayerPrefs.SetInt(PPm.Loadyear, currYr);

        SceneManager.LoadScene(nextScnee);
    }

    private void cityDrpdValChanged(int val)
    {
        currCity = city_drpd.options[val].text;
    }

    private void yr_drpdValChane(int val)
    {
        currYr = int.Parse (yr_drpd.options[val].text);
    }
}