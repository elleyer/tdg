using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Window
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Show(float time, bool autoHide)
        {
            if (Math.Abs(time) < 1 || autoHide)
                DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, 1f)
                    .OnComplete(() => _canvasGroup.interactable = true).OnComplete(() => Hide(3));
            else
                DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, 1f)
                    .OnComplete(() => Hide(time)).OnComplete(() => _canvasGroup.interactable = true);
        }

        public void Hide(float time)
        {
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, 1f).SetDelay(time).OnComplete(() => _canvasGroup.interactable = false);
        }
    }
}