using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Window
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Show(float time)
        {
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, 1f)
                .OnComplete(() => Hide(time));
        }

        private void Hide(float time)
        {
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, 1f).SetDelay(time);
        }

        public void Restart()
        {

        }
    }
}