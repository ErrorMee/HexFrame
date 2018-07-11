using UnityEngine;
using UnityEditor;

public class BuildResources
{

    const string kBuildResources = GemaEditorConst.Resource + "/☀ BuildResources";
    /// <summary>
    ///  生成资源包
    /// </summary>
    [@MenuItem(kBuildResources, false, 2052)]
    public static void _BuildResources()
    {
        CopyLuaToStreamingAssets.OnCopyLuaToStreaming();

        BuildPipeline.BuildAssetBundles(PathUtil.StreamingassetsPath + "/patchs", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

        //打包还可以非显示
        //AssetBundleBuild[] buildMap = new AssetBundleBuild[2];

        //buildMap[0].assetBundleName = "enemybundle";
        //string[] enemyAssets = new string[2];
        //enemyAssets[0] = "Assets/Textures/char_enemy_alienShip.jpg";
        //enemyAssets[1] = "Assets/Textures/char_enemy_alienShip-damaged.jpg";
        //buildMap[0].assetNames = enemyAssets;

        //buildMap[1].assetBundleName = "herobundle";
        //string[] heroAssets = new string[1];
        //heroAssets[0] = "char_hero_beanMan";
        //buildMap[1].assetNames = heroAssets;

        //BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

        GeneratePatchFiles.bundlesPath = Application.streamingAssetsPath + "/patchs/";

        GeneratePatchFiles.GenerateFiles();

    }
	
}
