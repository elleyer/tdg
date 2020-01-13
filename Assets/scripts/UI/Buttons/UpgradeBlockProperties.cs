using Projectiles.Towers;
using UnityEngine;
using UnityEngine.UI;
using Utils.GameTools;

namespace UI.Buttons
{
    public class UpgradeBlockProperties : MonoBehaviour
    {
        public static UpgradeBlockProperties Instance = null;
        public Button UpgradeDamage, UpgradeCooldown, Sell, Delete;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void RemoveListeners()
        {
            UpgradeDamage.onClick.RemoveAllListeners();
            UpgradeCooldown.onClick.RemoveAllListeners();
            Sell.onClick.RemoveAllListeners();
            Delete.onClick.RemoveAllListeners();
        }

        public void UpdateProperties(Defender defender)
        {
            UpgradeDamage.onClick.AddListener(() => defender.GetComponent<HandleObject>().Upgrade(UpgradeType.Damage));
            UpgradeCooldown.onClick.AddListener(() => defender.GetComponent<HandleObject>().Upgrade(UpgradeType.Cooldown));
        }
    }
}