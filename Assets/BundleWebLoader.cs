using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BundleWebLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject BundledSpriteObject;

    private bool BundledSpriteObjectIsEnabled;

    public string bundleUrl = "http://localhost/assetbundles/testbundle";
    public string assetName = "BundledSpriteObject";

    IEnumerator Start()
    {
        using (WWW web = new WWW(bundleUrl))
        {
            yield return web;
            AssetBundle remoteAssetBundle = web.assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }

            gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff);
            BundledSpriteObjectIsEnabled = true;
            BundledSpriteObject.SetActive(BundledSpriteObjectIsEnabled);

            BundledSpriteObject = Instantiate(remoteAssetBundle.LoadAsset(assetName)) as GameObject;
            remoteAssetBundle.Unload(false);
        }


    }

    private void TurnOnAndOff()
    {
        BundledSpriteObjectIsEnabled = !BundledSpriteObjectIsEnabled;
        BundledSpriteObject.SetActive(BundledSpriteObjectIsEnabled);
    }
}