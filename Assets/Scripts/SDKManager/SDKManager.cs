using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.sharesdk.unity3d;

public class SDKManager : SingletonBehaviour<SDKManager>
{
    private AndroidJavaObject jo;

    public void Init()
    {
        GLog.Log("Application.platform " + Application.platform);
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
        InitShare();
    }

    public void TestUnityCallAndorid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string time = jo.Call<string>("getNowTime");
            jo.Call("showToast", new object[] { time });
        }
    }

    public void TestAndoridCallUnity(string uObjName, string methodName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jo.Call("CallUnityFunc", uObjName, methodName);
        }
    }


    private ShareSDK ssdk;
    private void InitShare()
    {
        ssdk = gameObject.GetComponent<ShareSDK>();
        ssdk.shareHandler = OnShareResultHandler;
    }

    public void TestShare()
    {
        ShareContent content = new ShareContent();
        content.SetText("TestShare");
        content.SetTitle("test title");
        content.SetTitleUrl("http://www.mob.com");
        content.SetSite("Mob-ShareSDK");
        content.SetSiteUrl("http://www.mob.com");
        content.SetShareType(ContentType.Text);

        ssdk.ShareContent(PlatformType.QQ, content);
    }

    public void TestShareMenu()
    {
        ShareContent content = new ShareContent();
        content.SetText("TestShareMenu");
        content.SetTitle("test title");
        content.SetTitleUrl("http://www.mob.com");
        content.SetSite("Mob-ShareSDK");
        content.SetSiteUrl("http://www.mob.com");
        content.SetShareType(ContentType.Text);

        ssdk.ShowPlatformList(null, content, 100, 100);
    }

    public void TestShareView()
    {
        ShareContent content = new ShareContent();
        content.SetText("TestShareView");
        content.SetTitle("test title");
        content.SetTitleUrl("http://www.mob.com");
        content.SetSite("Mob-ShareSDK");
        content.SetSiteUrl("http://www.mob.com");
        content.SetShareType(ContentType.Text);

        ssdk.ShowShareContentEditor(PlatformType.QQ, content);
    }

    void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            GLog.Log("share successfully - share result :");
            GLog.Log(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            GLog.Log("share fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			GLog.Log ("share fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            GLog.Log("share cancel !");
        }
    }

}
