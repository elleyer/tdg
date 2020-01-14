using Projectiles.Towers;
using UnityEngine;
using UnityEngine.UI;
using Utils.GameTools;

namespace UI.Buttons
{
    public class UpgradeBlockProperties : MonoBehaviour
    {
        public static UpgradeBlockProperties Instance;
        public Button UpgradeDamage, UpgradeCooldown, Sell, Delete;
        public Text DamagePrice, CooldownPrice, SellPrice;
        public Defender Targer;

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
            var handleObject = defender.GetComponent<HandleObject>();

            DamagePrice.text = defender.Damage < defender.MaxDamage ? handleObject.DamagePrice.ToString("F0") : "Maxed";
            CooldownPrice.text = defender.Cooldown > defender.MinCooldown? handleObject.CooldownPrice.ToString("F0") : "Maxed";
            SellPrice.text = ((uint)(defender.Damage / defender.Cooldown) * 10).ToString();

            if (Targer == defender)
                return;
            Targer = defender;
            RemoveListeners();
            UpgradeDamage.onClick.AddListener(() => handleObject.Upgrade(UpgradeType.Damage));
            UpgradeCooldown.onClick.AddListener(() => handleObject.Upgrade(UpgradeType.Cooldown));
            Sell.onClick.AddListener(() => handleObject.Sell());
        }
    }
}