using System;
using UnityEngine;

public class SliderUtil : MonoBehaviour
{
    private Action<int> slidHorizontalEvent;

    private bool startHorizontalSild = false;

    private Vector3 startPos;

    private void Awake()
    {
        
    }

    public void SetHorizontalSlid(Action<int> callBack)
    {
        slidHorizontalEvent = callBack;
    }

    void Update()
    {
        if (slidHorizontalEvent != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreen = Input.mousePosition;
                startPos = new Vector3();
                startPos.x = (mouseScreen.x - Screen.width * 0.5f);
                startPos.y = (mouseScreen.y - Screen.height * 0.5f);
                startPos.z = Time.realtimeSinceStartup;

                if (Math.Abs(startPos.y) < HexGridModel.VIEW_HEIGHT)
                {
                    startHorizontalSild = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (startHorizontalSild)
                {
                    startHorizontalSild = false;

                    Vector3 mouseScreen = Input.mousePosition;
                    Vector3 endPos = new Vector3();
                    endPos.x = (mouseScreen.x - Screen.width * 0.5f);
                    endPos.y = (mouseScreen.y - Screen.height * 0.5f);
                    endPos.z = Time.realtimeSinceStartup;

                    float time = endPos.z - startPos.z;
                    float len = (float)Math.Sqrt(Math.Pow((startPos.x - endPos.x), 2) + Math.Pow((startPos.y - endPos.y), 2));
                    if (len > HexConst.width * GameConst.PixelPerCanvas.x)
                    {
                        double angle = Math.Atan2((double)(endPos.y - startPos.y), (double)(endPos.x - startPos.x)) * 180 / Math.PI;

                        if (Math.Abs(angle) < 40)
                        {
                            slidHorizontalEvent(-1);
                        }

                        if (Math.Abs(angle) > 140)
                        {
                            slidHorizontalEvent(1);
                        }
                    }
                }
            }
        }
    }
}