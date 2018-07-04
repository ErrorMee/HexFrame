using System;
using UnityEngine;
using UnityEditor;

public class PathEdit
{
    const string kOpenLocalPatchPath = GemaEditorConst.Path + "/OpenLocalPatchPath";
    [MenuItem(kOpenLocalPatchPath, false, 100)]
    static void OpenLocalPatchPath()
    {
        System.Diagnostics.Process.Start(PathUtil.PatchPath);
    }

    const string kOpenRemotePatchPath = GemaEditorConst.Path + "/OpenRemotePatchPath";
    [MenuItem(kOpenRemotePatchPath, false, 101)]
    static void OpenRemotePatchRootPath()
    {
        GameSetting gameSetting = Resources.Load<GameSetting>("GameSetting");
        System.Diagnostics.Process.Start(gameSetting.GetPatchRootPath());
    }

    const string kOpenStreamingAssetsPath = GemaEditorConst.Path + "/OpenStreamingAssetsPath";
    [MenuItem(kOpenStreamingAssetsPath, false, 102)]
    static void OpenStreamingAssetsPath()
    {
        System.Diagnostics.Process.Start(Application.streamingAssetsPath + "/patchs");
    }
}