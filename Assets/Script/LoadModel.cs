using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class LoadModel : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DownLoadAssetBumndleFromServer());

    }

    private IEnumerator DownLoadAssetBumndleFromServer()
    {
        GameObject go = null;

        string url = "https://drive.usercontent.google.com/u/0/uc?id=1sYXGPJPUzq1QTmn_Zi5sSzM5bnS1XojG&export=download";

        using (UnityWebRequest ww = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return ww.SendWebRequest();
            if (ww.result == UnityWebRequest.Result.ConnectionError || ww.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the request at: " + url + " " + ww.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(ww);
                go = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            ww.Dispose();
        }
        InstantiateGameObjetcFromAssetBundle(go);
    }

    private void InstantiateGameObjetcFromAssetBundle(GameObject go)
    {
        if (go != null)
        {
            GameObject instanceGo = Instantiate(go);
            instanceGo.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Your asset bundle go is null");
        }

    }

 
}
