using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipBar : MonoBehaviour {

    public Text popText;

    public Image image;

    private void Awake()
    {
        image.rectTransform.localScale = new Vector3();
    }

    // Use this for initialization
    void Start () {
        image.rectTransform.DOScale(1, 0.3f).SetAutoKill(true).SetEase(Ease.InOutBack);
        image.rectTransform.DOScale(0.1f, 0.3f).SetAutoKill(true).SetDelay(2.5f).onComplete = OnTipEnd;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SetText(string str)
    {
        popText.text = str;
        image.rectTransform.sizeDelta = new Vector2(popText.preferredWidth + 100, popText.preferredHeight + 20);
    }

    public void MoveUp()
    {
        transform.localPosition = new Vector2(0, image.rectTransform.sizeDelta.y + 16);
    }

    public void OnTipEnd()
    {
        DestroyObject(gameObject);
    }
}
