﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Game : SingletonBehaviour<Game>
{
    public Transform canvasTrans;

    public GameSetting gameSetting;

    void Awake()
    {
        if (gameSetting == null)
        {
            gameSetting = Resources.Load<GameSetting>("GameSetting");
        }
        gameSetting.SetGame();

        gameObject.AddComponent<SDKManager>();
        gameObject.AddComponent<HttpManager>();
        gameObject.AddComponent<ProtobufManager>();
        gameObject.AddComponent<PatchManager>();
        gameObject.AddComponent<AssetManager>();
        gameObject.AddComponent<UIManager>();
        gameObject.AddComponent<LuaManager>();

        GameEvent.RegisterEvent(GameEventType.PatchComplete, OnPatchComplete);
        GameEvent.RegisterEvent(GameEventType.AssetManagerReady, OnAssetManagerReady);
        GameEvent.RegisterEvent(GameEventType.LuaManagerReady, OnLuaManagerReady);

        HexGridModel.Instance.Init();
    }

    void Start()
    {
        SDKManager.Instance.Init();
        PatchManager.Instance.Init();
        ProtobufManager.Instance.Test();
    }

    private void OnDestroy()
    {
        gameSetting.CloseGame();
    }

    void OnPatchComplete(object[] param)
    {
        GameEvent.UnregisterEvent(GameEventType.PatchComplete, OnPatchComplete);

        AssetManager.Instance.Init();
    }

    void OnAssetManagerReady(object[] param)
    {
        GameEvent.UnregisterEvent(GameEventType.AssetManagerReady, OnAssetManagerReady);
        LuaManager.Instance.Init();
    }

    void OnLuaManagerReady(object[] param)
    {
        GameEvent.UnregisterEvent(GameEventType.LuaManagerReady, OnLuaManagerReady);

        UIManager.Instance.OpenUI("GroundView");
        UIManager.Instance.OpenUI("MainView");
        UIManager.Instance.OpenUI("TipsView");
        
    }
}
