using Projectiles.Mobs;
using UnityEngine;

namespace Game.Resources.Profile
{
    public class Wallet : MonoBehaviour
    {
        public delegate void Operation(uint value);

        public event Operation OnOperationCreated;
        private uint Balance { get; set; } = 1000000;

        private void Start() => OnOperationCreated?.Invoke(Balance);

        public void AddBalance(uint value)
        {
            Balance += value;
            OnOperationCreated?.Invoke(Balance);
        }

        public void SetBalance(uint value)
        {
            Balance = value;
            OnOperationCreated?.Invoke(value);
        }

        public void AddBalanceByEnemy(Enemy enemy)
        {
            Balance += (uint)enemy.Reward;
            OnOperationCreated?.Invoke(Balance);
        }

        public bool CanWithdraw(uint value)
        {
            return Balance >= value;
        }

        public void Withdraw(uint value)
        {
            Balance -= value;
            OnOperationCreated?.Invoke(Balance);
        }
    }
}