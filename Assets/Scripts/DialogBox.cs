using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogBox : MonoBehaviour
{
    public Transform main;
    public CanvasGroup bg;

    private void OnEnable()
    {
        bg.alpha = 0;
        bg.DOFade(1f, 0.5f);

        main.localPosition = new Vector2(0, -Screen.height);
        main.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.1f);
    }

    public void CloseDialog()
    {
        bg.DOFade(0, 0.5f);
        main.DOLocalMoveY(-Screen.height, 0.5f).SetEase(Ease.InExpo).OnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
