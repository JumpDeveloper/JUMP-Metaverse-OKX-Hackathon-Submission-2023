using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarTilr : MonoBehaviour
{
    internal int EraYr => eraYear;

    private int eraYear = -1,
                _isMale = -1;

    [SerializeField]
    private Image image;

    [SerializeField]
    internal Button equip_btn,
                    purhcase_btn;

    [SerializeField]
    private TMP_Text amount_txt; 

    private bool _isPurchased = false;

    internal string url;

    private void Start()
    {
        purhcase_btn.onClick.AddListener(PurchaseBtnClicked);
    }

    internal void SetEraYr(int yr, int isMale, Sprite sprite, bool isPurchased, float amount,string _url)
    {
        url = _url;
        eraYear = yr;
        image.sprite = sprite;
        _isMale = isMale;
        _isPurchased = isPurchased;
        amount_txt.text = "$"+amount.ToString();
        if(!isPurchased)
            equip_btn.gameObject.SetActive(false);
    }

    //Called from editor
    private void PurchaseBtnClicked()
    {
        _isPurchased = true;
        equip_btn.gameObject.SetActive(true);
    }
}
