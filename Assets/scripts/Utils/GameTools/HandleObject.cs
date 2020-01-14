using System;
using Game.Resources.Profile;
using Projectiles.Towers;
using UI.Buttons;
using UI.Window;
using UnityEngine;
using Screen = UI.Window.Screen;

namespace Utils.GameTools
{
    public class HandleObject : MonoBehaviour
    {
        private Defender _defender;
        public float DamagePrice, CooldownPrice, SellPrice;

        private void Start()
        {
            _defender = gameObject.GetComponent<Defender>();
            DamagePrice = 200;
            CooldownPrice = 200;
            SellPrice = (uint)(_defender.Damage / _defender.Cooldown * 10);
        }

        public void Sell()
        {
            ProfileInfo.Instance.Wallet.AddBalance((uint)(_defender.Damage / _defender.Cooldown) * 10);
            UpgradeBlockProperties.Instance.RemoveListeners();
            Destroy(gameObject);
        }

        public void Remove()
        {

        }

        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Cooldown:
                    if (ProfileInfo.Instance.Wallet.CanWithdraw((uint)(CooldownPrice * 2)) && _defender.Cooldown > _defender.MinCooldown)
                    {
                        ProfileInfo.Instance.Wallet.Withdraw((uint)(CooldownPrice * 1.05f));
                        if (_defender.Cooldown - _defender.MaxCooldown / 100 * 5 > _defender.MinCooldown)
                            _defender.Cooldown -= _defender.MaxCooldown / 100 * 5;
                        else
                            _defender.Cooldown = _defender.MinCooldown;
                    }

                    CooldownPrice *= 1.05f;
                    _defender.CoolDownFill.fillAmount = _defender.MinCooldown / _defender.Cooldown;
                    break;
                case UpgradeType.Damage:
                    if (ProfileInfo.Instance.Wallet.CanWithdraw((uint)(DamagePrice * 1.05f)) && Mathf.RoundToInt(_defender.Damage)
                        != Mathf.RoundToInt(_defender.MaxDamage))
                    {
                        ProfileInfo.Instance.Wallet.Withdraw((uint)(DamagePrice * 1.05f));
                        if (_defender.Damage + _defender.MaxDamage / 100 * 5 < _defender.MaxDamage)
                            _defender.Damage += Mathf.RoundToInt(_defender.MaxDamage / 100 * 5);
                        else
                            _defender.Damage = _defender.MaxDamage;
                    }

                    DamagePrice *= 1.05f;
                    _defender.DamageFill.fillAmount = 1 / _defender.MaxDamage * _defender.Damage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            SellPrice = (uint)(_defender.Damage / _defender.Cooldown) * 100;
            UpgradeBlockProperties.Instance.UpdateProperties(_defender);
        }

        public void OnMouseDown()
        {
            Screen.Instance.HandleUserInterface(UserInterfaceType.UpgradeableBlock);
            //UpgradeBlockProperties.Instance.RemoveListeners();
            UpgradeBlockProperties.Instance.UpdateProperties(_defender);
        }
    }

    public enum UpgradeType
    {
        Cooldown,
        Damage
    }
}