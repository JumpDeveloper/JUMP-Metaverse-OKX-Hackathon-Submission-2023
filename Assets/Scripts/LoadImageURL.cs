using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageURL : MonoBehaviour
{

    //public Renderer thisrenderer;
    public string url = "https://ibb.co/jkf8Yx5";
    //private Texture _EventTexture;
    public Image _EventTexture;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f));
            _EventTexture.sprite = newSprite;
        }
    }
}