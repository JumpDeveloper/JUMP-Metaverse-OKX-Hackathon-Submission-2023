using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPanel : MonoBehaviour
{
    [SerializeField]
    private Transform gObj;

    [SerializeField]
    private GameObject avtrTile;

    [SerializeField]
    private Button male_btn, female_btn;

    private PlayerSkin m_skin;
    private List<EraClothes> m_clothes = new List<EraClothes>();

    private void Start()
    {
        male_btn.onClick.AddListener(MaleBtnClicked);
        female_btn.onClick.AddListener (FemaleBtnClicked);
    }

    internal void BeginAvtrPanel(int isMale, List<EraClothes> clothes, PlayerSkin playerSkin)
    {
        ClearChildrenObj(gObj);

        m_skin = playerSkin;
        m_clothes = clothes;
        foreach (var item in clothes)
        {
            GameObject obj = Instantiate(avtrTile, gObj);
            obj.GetComponent<AvatarTilr>().SetEraYr(item.eras[0], isMale,
                                        (isMale==1)?item.male_sprite:item.female_sprite,
                                        item.isPurchasd, item.amount);
            obj.GetComponent<AvatarTilr>().equip_btn.onClick.AddListener(() =>
            {
                    playerSkin.PutPlayerSkin(obj.GetComponent<AvatarTilr>().EraYr);
            });

            obj.GetComponent<AvatarTilr>().purhcase_btn.onClick.AddListener(() =>
            {
                playerSkin.UpdatePurchase(obj.GetComponent<AvatarTilr>().EraYr);
            });

        }
    }

    private void ClearChildrenObj(Transform t)
    {
        for(int i=0; i<t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject, 0.05f);
        }
    }

    private void MaleBtnClicked()
    {
        PlayerPrefs.SetInt(PPm.isMale, 1);
        m_skin.avtr_setGender(1);
        BeginAvtrPanel(1, m_clothes, m_skin);
    }

    private void FemaleBtnClicked()
    {
        PlayerPrefs.SetInt(PPm.isMale, 0);
        m_skin.avtr_setGender(0);
        BeginAvtrPanel(0, m_clothes, m_skin);
    }


}
