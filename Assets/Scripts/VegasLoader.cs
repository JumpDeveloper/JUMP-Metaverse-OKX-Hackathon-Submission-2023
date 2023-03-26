using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class VegasLoader : MonoBehaviour
{
    [Range(0.0f, 10.0f)]

    public int mySliderFloat;

    [SerializeField]
    private List<Vegas> vegas_ = new List<Vegas>();

    [SerializeField] private TMP_Text valTxt;

    [SerializeField]
    private VideoPlayer videoPlayer;

    public static VegasLoader instance;

    private int currentYear = -9090;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Debug.LogError("More than1 vegasloder: "+gameObject.name);
            return;
        }

        
    }

    internal void LoadVegas()
    {
        int ind = -1;
        for (int i = 0; i < yr_drpdwn.options.Count; i++)
        {
            if (yr_drpdwn.options[i].text == (PlayerPrefs.GetInt(PPm.Loadyear)).ToString())
            {
                ind = i; break;
            }
        }
        if (ind != -1)
            yr_drpdwn.value = ind;
        yr_drpdwn.onValueChanged.AddListener(OnDrpdwnValChanged);
        //print(PlayerPrefs.GetInt(PPm.Loadyear));

        OnYearChanged(PlayerPrefs.GetInt(PPm.Loadyear));
        //OnYearChanged(vegas_[mySliderFloat].year);
    }

    private void OnYearChanged(int year)
    {
        if (year == currentYear)
            return;

        currentYear = year;

        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ThirdPersonController>().enabled = false;

        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += (x) =>
        { 
            videoPlayer.gameObject.SetActive(false);
            player.GetComponent<ThirdPersonController>().enabled = true;
        };

        Invoke(nameof(DelayTran), 1f);
    }

    private void DelayTran() => DelayTran2(currentYear);

    private void DelayTran2(int year)
    {
        ChangeWeather(year);

        

        vegas_.ForEach(v => v.obj.SetActive(false));
        var vega = vegas_.Find(x => x.year == year);
        vega.obj.SetActive(true);

        vegas_[0].obj.SetActive(vega.show1900); //base ground

        if(vega.show1900)
            strip.material = (vega.above1940) ? CementRoad : RedRoad;
        
        valTxt.text = year.ToString();
    }

    private void ChangeWeather(int year)
    {
        int ind = -1;
        for (int i = 0; i < weathers.Count; i++)
        {
            foreach (var item in weathers[i].eras)
            {
                if(item == year)
                {
                    ind = i; break;
                }
            }
        }

        if(ind == -1)
        {
            Debug.LogError("Era not found");
        }

        postProcessVoulme.profile = weathers[ind].profile;
        RenderSettings.fogColor = weathers[ind].fogColor;
        RenderSettings.fogDensity = weathers[ind].fogDensity;
    }

    private void Update()
    {
        //OnYearChanged(vegas_[mySliderFloat].year);
    }

    [SerializeField]
    private TMP_Dropdown yr_drpdwn;

    private void OnDrpdwnValChanged(int val)
    {
        if (era_instr != null) Destroy(era_instr);
        //print(val+":"+yr_drpdwn.options[val].text);
        OnYearChanged(int.Parse(yr_drpdwn.options[val].text));
    }

    //Circle UI
    public void OnValChan1ged(int val)
    {
        //print(val);

        int last = val % 10;
        int dir = (last>5) ? val+(10-last) : val-last;

        //valTxt.text = dir.ToString();

        OnYearChanged(dir);

        //Remove comment to use auto cloth change
        //ChangePlayerApp(dir);
    }

    private void ChangePlayerApp(int dir)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerSkin>()?.PutPlayerSkin(dir);
    }

    [SerializeField]
    private List<Weather> weathers = new List<Weather>();

    [SerializeField]
    private Material RedRoad, CementRoad;
    [SerializeField]
    private MeshRenderer strip;

    [SerializeField]
    private Volume postProcessVoulme;

    [SerializeField]
    private GameObject era_instr;
}

[Serializable]
public struct Vegas
{
    public int year;
    public GameObject obj;
    public bool show1900,
                above1940;
}

[Serializable]
public struct Weather
{
    public List<int> eras;
    public VolumeProfile profile;
    public float fogDensity;
    public Color fogColor;
}