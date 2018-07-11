using System;
using UnityEngine;

public enum EnviromentEnum
{
    /// <summary>
    /// 产品环境
    /// </summary>
    Product = 1,

    /// <summary>
    /// 开发环境
    /// </summary>
    Devlopment = 2,
}

[Serializable]
public class GameSetting : ScriptableObject
{
    public EnviromentEnum environment = EnviromentEnum.Devlopment;

    [Header("---------- Patch ----------")]

    /// <summary>
    /// 是否开启热更
    /// </summary>
    public bool patchOpen = false;

    /// <summary>
    /// 是否使用模拟地址进行热更
    /// </summary>
    public bool patchPathSimulation = true;

    /// <summary>
    /// 热更地址(本地模拟)
    /// </summary>
    [TextArea(2,3)]
    public string patchRootPathSimulation;

    /// <summary>
    /// 热更地址(服务器)
    /// </summary>
    [TextArea(2, 3)]
    public string patchRootPathWeb;

    [Space(10)]
    [Header("---------- Load ----------")]

    /// <summary>
    /// 编辑器环境加载资源是否使用生成的bundle 
    /// false时直接加载原始资源
    /// </summary>
    [Tooltip("编辑器环境加载资源是否使用bundle文件")]
    public bool editorLoadModeIsBundle = true;

    [Space(10)]
    [Header("---------- Other ----------")]

    /// <summary>
    /// 游戏帧率
    /// </summary>
    [ContextMenuItem("Reset", "ResetFPS")]
    [Range(30, 100)]
    public int fps = 45;
    void ResetFPS()
    {
        fps = 45;
    }

    /// <summary>
    /// 热更地址(服务器)
    /// </summary>
    public bool writeLog = true;

    /// <summary>
    /// 设置游戏
    /// </summary>
    public void SetGame()
    {
        Input.multiTouchEnabled = false;
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        //设置加载线程等级
        //Application.backgroundLoadingPriority = ThreadPriority.High;
        //设置UWR缓存压缩
        //Caching.compressionEnabled = true;
        
        if (writeLog)
        {
            GLog.SetWriteLog();
        }
    }

    public void CloseGame()
    {
        GLog.CloseGame();
    }

    private void OnEnable()
    {
        patchRootPathSimulation = Application.dataPath + "/../SimulationWebAddress/patchs/";
    }

    public string GetPatchRootPath()
    {
        if (patchPathSimulation)
        {
            return patchRootPathSimulation;
        }
        return patchRootPathWeb;
    }

}

public static class GameConst
{
    /// <summary>
    /// UGUI 尺寸
    /// </summary>
    public static Vector2 ReferenceResoulution = new Vector2(1080, 1920);

    /// <summary>
    /// 屏幕，画布尺寸比
    /// </summary>
    public static Vector2 PixelPerCanvas = new Vector2(Screen.width / ReferenceResoulution.x, Screen.height / ReferenceResoulution.y);
}