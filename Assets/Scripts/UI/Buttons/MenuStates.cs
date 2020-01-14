using System;
using Game;
using UnityEngine;
using UnityEngine.UI;
using Screen = UI.Window.Screen;

namespace UI.Buttons
{
    public class MenuStates : MonoBehaviour
    {
        public Button RestartScene, WinResult, InfoHints, InfoHintsContinue;

        private void Start()
        {
            RestartScene.onClick.AddListener(() => SessionManager.Instance.Restart());
            WinResult.onClick.AddListener(() => SessionManager.Instance.Restart());
            InfoHints.onClick.AddListener(() => Screen.Instance.Push(3));
            InfoHintsContinue.onClick.AddListener(() => Screen.Instance.InfoHint.Hide(0));
        }
    }
}