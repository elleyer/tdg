using Game.Resources.Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoBlockData : MonoBehaviour
    {
        public TextMeshProUGUI WalletInfo, WavesInfo, DestroyedInfo, AliveInfo, DefenderInfo;

        private void Start()
        {
            ProfileInfo.Instance.Wallet.OnOperationCreated += UpdateBalance;
        }

        private void UpdateBalance(uint value)
        {
            WalletInfo.text = $"Coins: {value.ToString()}";
        }

        public void UpdateAliveCount(int value) => AliveInfo.text = $"Alive: {value.ToString()}"; //Probably we can use enum here

        public void UpdateDestroyedCount(int value) => DestroyedInfo.text = $"Destroyed: {value.ToString()}";

        public void UpdateWavesInfo(int value) => WavesInfo.text = $"Waves passed: {value.ToString()}";

        public void UpdateDefenderInfo(int value) => DefenderInfo.text = $"Defenders: {value.ToString()}";
    }
}