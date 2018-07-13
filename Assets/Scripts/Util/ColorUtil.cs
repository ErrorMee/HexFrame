using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil
{
    /// <summary>
    /// color转换到hex 推荐ColorUtility
    /// </summary>
    /// <param name="color"></param>
    /// <returns>FFFFFFFF</returns>
    public static string ColorToHex(Color color,bool useAlpha)
    {
        if (useAlpha)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        } else
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        //int r = Mathf.RoundToInt(color.r * 255.0f);
        //int g = Mathf.RoundToInt(color.g * 255.0f);
        //int b = Mathf.RoundToInt(color.b * 255.0f);
        //int a = Mathf.RoundToInt(color.a * 255.0f);
        //string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        //return hex;
    }

    /// <summary>
    /// hex转换到color  推荐ColorUtility
    /// </summary>
    /// <param name="hex">FFFFFFFF</param>
    /// <returns></returns>
    public static Color HexToColor(string hex)
    {
        //byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        //byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        //byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        //float r = br / 255f;
        //float g = bg / 255f;
        //float b = bb / 255f;
        //float a = cc / 255f;
        //return new Color(r, g, b, a);
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    /// <summary>
    /// 颜色的富文本
    /// </summary>
    /// <param name="color"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ColorString(Color color,string source, bool useAlpha = false)
    {
        string hex = ColorToHex(color, useAlpha);
        string colorString = string.Format("<color=#{0}>{1}</color>", hex, source);
        return colorString;
    }
}

