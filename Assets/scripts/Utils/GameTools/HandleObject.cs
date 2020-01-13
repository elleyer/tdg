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

        private void Start() => _defender = gameObject.GetComponent<Defender>();
        public void Sell()
        {

        }

        public void Remove()
        {

        }

        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Cooldown:
                    if (ProfileInfo.Instance.Wallet.CanWithdraw((uint)_defender.CooldownLevel * 200) && Mathf.RoundToInt(_defender.Cooldown)
                        != Mathf.RoundToInt(_defender.MinCooldown))
                    {
                        ProfileInfo.Instance.Wallet.Withdraw((uint)_defender.CooldownLevel * 200);
                        if (_defender.Cooldown - _defender.MinCooldown / 100 * 15 > _defender.MinCooldown)
                            _defender.Cooldown -= _defender.MaxDamage / 100 * 15;
                        else
                            _defender.Cooldown = _defender.MinCooldown;
                    }
                    break;
                case UpgradeType.Damage:
                    if (ProfileInfo.Instance.Wallet.CanWithdraw((uint)_defender.DamageLevel * 200) && Mathf.RoundToInt(_defender.Damage)
                        != Mathf.RoundToInt(_defender.MaxDamage))
                    {
                        ProfileInfo.Instance.Wallet.Withdraw((uint)_defender.DamageLevel * 200);
                        if (_defender.Damage + _defender.MaxDamage / 100 * 15 < _defender.MaxDamage)
                            _defender.Damage += _defender.MaxDamage / 100 * 15;
                        else
                            _defender.Damage = _defender.MaxDamage;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void OnMouseDown()
        {
            Screen.Instance.HandleUserInterface(UserInterfaceType.UpgradeableBlock);
            UpgradeBlockProperties.Instance.RemoveListeners();
            UpgradeBlockProperties.Instance.UpdateProperties(_defender);
        }
    }

    public enum UpgradeType
    {
        Cooldown,
        Damage
    }
}