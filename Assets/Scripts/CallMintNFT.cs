using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class CallMintNFT : MonoBehaviour
{
    public Text address,balance;
    public string imageurl;
    public string walletAdress;


#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void CallNFT(string wallet,string ImageURl);

    [DllImport("__Internal")]
    private static extern string CallBalance(string ownerID);

     [DllImport("__Internal")]
    private static extern string ShowString();

   // [DllImport("__Internal")]
   // private static extern string CallTokenURI(string ownerID,string tokenID);


#endif
    private const string param = "https://gateway.pinata.cloud/ipfs/"; 

    // Start is called before the first frame update
    void Start()
    {
        walletAdress = PlayerPrefs.GetString("Account");
       
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
#endif
    }

    public void MintNFTJS(string image)
    {
       // string walletid = walletAdress.ToString();

        // string imageuurl = imageurl;

#if UNITY_WEBGL && !UNITY_EDITOR
        CallNFT(walletAdress,param + image);
#endif
    }

    public void CallBalanceOwner()
    {

        StartCoroutine(CallTwice());
        balance.text = "Fetching Balance...";
    }

    IEnumerator CallTwice()
    {
        
        yield return new WaitForSeconds(1f);
#if UNITY_WEBGL && !UNITY_EDITOR
        CallBalance(walletAdress);
#endif
        yield return new WaitForSeconds(1f);

#if UNITY_WEBGL && !UNITY_EDITOR
        balance.text =  CallBalance(walletAdress);
#endif

    }

    public void GetStringJS()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       balance.text = ShowString();
#endif
    }

    public void FetchTokenURI(string n)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       // fetchURI.text = CallTokenURI(walletAdress,n);
#endif
    }
}
