using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Window
{
    public class Screen : MonoBehaviour
    {
        public static Screen Instance;
        public Window WinResult, LoseResult, WavePassed, InfoHint, LowHealth;
        internal CanvasGroup UpgradeableBlock, SelectableBlock, InfoBlock;
        private bool _upgradableBlockHidden;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void Push(int screenType) //TODO: push screen
        {
            switch ((ScreenType)screenType)
            {
                case ScreenType.WavePassed:
                    WavePassed.Show(3, true);
                    break;
                case ScreenType.Fail:
                    LoseResult.gameObject.SetActive(true);
                    LoseResult.Show(0, false);
                    break;
                case ScreenType.Win:
                    WinResult.Show(15, false);
                    break;
                case ScreenType.GameInfo:
                    InfoHint.Show(3, false);
                    break;
                case ScreenType.LowHealth:
                    LowHealth.Show(5, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null);
            }
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
                        UpgradeableBlock.gameObject.SetActive(true);
                        DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 1f, 0.4f);
                        _upgradableBlockHidden = false;
                    }
                    else
                    {
                        DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 0f, 0.4f)
                            .OnComplete(() => UpgradeableBlock.gameObject.SetActive(false));
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
                DOTween.To(() => UpgradeableBlock.alpha, x => UpgradeableBlock.alpha = x, 0f, 0.4f)
                    .OnComplete(() => UpgradeableBlock.gameObject.SetActive(false));
        }
    }

    public enum ScreenType
    {
        WavePassed,
        Fail,
        Win,
        GameInfo,
        LowHealth
    }

    public enum UserInterfaceType
    {
        InfoBlock,
        SelectableBlock,
        UpgradeableBlock
    }
}