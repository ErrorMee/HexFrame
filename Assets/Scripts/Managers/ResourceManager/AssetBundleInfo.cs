using System;
using UnityEngine;
using System.Collections.Generic;

//http://blog.csdn.net/u012529088/article/details/54984479?utm_source=itdadao&utm_medium=referral

public class AssetBundleInfo
{
    //记录未完全释放的AB数量
    public static int unloadFalseCount = 0;

    //ab引用
    public AssetBundle ab;
    //ab中所有资源数量
    private int assetCount = 0;
    //已经取用的数量
    private int takedAssetCount = 0;
    //已取用资源列表(不重复项的无序列表 插入元素的操作非常快)
    private HashSet<string> takeHash = new HashSet<string>();
    //是否自动释放
    public bool isAutoUnload = true;
    //释放时是否释放所有
    private bool isAllUnload = false;

    /// <summary>
    /// 加载AssetBundle
    /// </summary>
    /// <param name="abRequest"></param>
    public void LoadAB(AssetBundleCreateRequest abRequest)
    {
        //UI内存路线：AB 引用 5
        ab = abRequest.assetBundle;
        assetCount = ab.GetAllAssetNames().Length;
    }

    /// <summary>
    /// 加载Asset
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public AssetBundleRequest LoadAssetAsync(string assetName)
    {
        return ab.LoadAssetAsync(assetName);
    }

    /// <summary>
    /// 取用Asset
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="asset"></param>
    public void TakeAsset(string assetName)
    {
        if (!takeHash.Contains(assetName))
        {
            takeHash.Add(assetName);
            takedAssetCount++;

            if (isAutoUnload && (assetCount == takedAssetCount))
            {
                UnloadAB(isAllUnload);
            }
        }
    }

    /// <summary>
    /// 放回Asset
    /// </summary>
    /// <param name="assetName"></param>
    public void BackAsset(string assetName)
    {
        if (takeHash.Contains(assetName))
        {
            takeHash.Remove(assetName);
            takedAssetCount--;
        }
    }

    /// <summary>
    /// 卸载AB
    /// </summary>
    /// <param name="isAll"></param>
    public void UnloadAB(bool isAll)
    {
        takeHash.Clear();
        ab.Unload(isAll);
        ab = null;
        if (isAll == false)
        {
            unloadFalseCount++;
        }
    }
}