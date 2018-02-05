using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimplePageList : MonoBehaviour
{

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/SimplePageList", false, 1)]
    static void CreateSimplePageList(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("SimplePageList");
        GameObject select = menuCommand.context as GameObject;
        if (select != null)
        {
            GameObjectUtility.SetParentAndAlign(go, select);
        }
        else
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas)
            {
                GameObjectUtility.SetParentAndAlign(go, canvas.gameObject);
            }
        }
        Undo.RegisterCreatedObjectUndo(go, "Created " + go.name);
        RectTransform rectTrans = go.AddComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector3(200, 200);
        rectTrans.pivot = new Vector2(0, 0.5f);

        //list
        RectTransform list = (new GameObject("List")).AddComponent<RectTransform>();
        GameObjectUtility.SetParentAndAlign(list.gameObject, go);
        GridLayoutGroup grid = list.gameObject.AddComponent<GridLayoutGroup>();
        ContentSizeFitter csf = list.gameObject.AddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        //item
        RectTransform item = (new GameObject("item")).AddComponent<RectTransform>();
        item.gameObject.AddComponent<Image>();
        GameObjectUtility.SetParentAndAlign(item.gameObject, list.gameObject);

    }
#endif

}
