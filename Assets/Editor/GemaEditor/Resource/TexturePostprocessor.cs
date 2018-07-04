using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TexturePostprocessor:AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        return;
        //SetAllTexture也可以做到批量设置图片
        TextureImporter ti = this.assetImporter as TextureImporter;
        GLog.Log("OnPreprocessTexture:" + ti.assetPath);
    }
}
