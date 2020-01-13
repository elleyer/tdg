using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Window
{
    public class Screen : MonoBehaviour
    {
        public static Screen Instance = null;
        internal CanvasGroup InfoBlock, SelectableBlock, UpgradeableBlock;
        private bool _upgradableBlockHidden;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void Push(ScreenType screenType) //TODO: push screen at UI
        {

        }

        public void HandleUserInterface(UserInterfaceType type)
        {
            Debug.Log("Handle");
            switch (type)
            {
                case UserInterfaceType.InfoBlock:
                    break;
                case UserInterfaceType.SelectableBlock:
                    break;
                case UserInterfaceType.UpgradeableBlock:
                    Debug.Log(UpgradeableBlock.alpha);
                    if (UpgradeableBlock.alpha < 1)
                    {
                        DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 1f, 1f)
                            .OnComplete(() => Debug.Log("Done"));
                        _upgradableBlockHidden = false;
                    }
                    else
                    {
                        DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 0f, 1f)
                            .OnComplete(() => Debug.Log("Done"));
                        _upgradableBlockHidden = true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1))
                return;
            if(!_upgradableBlockHidden)
                DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 0f, 1f)
                    .OnComplete(() => Debug.Log("Done"));
        }
    }

    public enum ScreenType
    {
        WavePassed,
        Fail,
        Win
    }

    public enum UserInterfaceType
    {
        InfoBlock,
        SelectableBlock,
        UpgradeableBlock
    }
}