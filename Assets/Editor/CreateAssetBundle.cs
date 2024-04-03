using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle
{
    [MenuItem("Assets/Create Assets Bundles")]
    private static void BuildAllAssetBundles()
    {
        string assetsBundleDirectoryPath = Application.dataPath + "/../AssetsBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetsBundleDirectoryPath, BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {

            Debug.LogException(e);
        }
    }
}