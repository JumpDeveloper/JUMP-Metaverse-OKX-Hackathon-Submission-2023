using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour
{
    [SerializeField] private Transform handle;
    [SerializeField] private Image fill;
    //[SerializeField] private TMP_Text valTxt;
    private Vector3 mousePos;

    [Header("REeturnalble data")]
    [Tooltip("Output will be given between these 2 values")]
    [SerializeField]
    private Vector2 MinMaxVal;

    [SerializeField]
    internal UnityEvent<int> OnValChanged;

    //Called from editor
    public void OnDrag(BaseEventData eventData)
    {
        //Debug.Log("dragged");
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? 360 + angle : angle;
        if (angle <= 225 || angle >= 315)
        {
            Quaternion r = Quaternion.AngleAxis(angle + 135, Vector3.forward);
            handle.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360);

            //valTxt.text = Mathf.Round((fill.fillAmount*100)/0.75f).ToString();
            int q = (int)Mathf.Round((fill.fillAmount * 100) / 0.75f);
            //print(q);
            OnValChanged.Invoke((int)(MinMaxVal.x + q));
        }
    }

    //public void OnValChan1ged(int val)
    //{
    //    print(val);
    //    valTxt.text = val.ToString();
    //}
}

