using Game.Resources.Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoBlockData : MonoBehaviour
    {
        public TextMeshProUGUI WalletInfo, WavesInfo, DestroyedInfo, AliveInfo;

        private void Start()
        {
            ProfileInfo.Instance.Wallet.OnOperationCreated += UpdateBalance;
        }

        private void UpdateBalance(uint value)
        {
            WalletInfo.text = value.ToString();
        }

        public void UpdateAliveCount(int value) => AliveInfo.text = value.ToString();

        public void UpdateDestroyedCount(int value) => DestroyedInfo.text = value.ToString();

        public void UpdateWavesInfo(int value) => WavesInfo.text = value.ToString();
    }
}