using System;
using Projectiles.Bombs;
using UnityEngine;

namespace Projectiles.Objects
{
    public class SelectableBomb : SelectableObject //TODO: Class, responsible for ...?
    {
        public Bomb.BombTypes BombType;

        private new void Start()
        {
            base.Start();

            switch (BombType)
            {
                case Bomb.BombTypes.C4:
                    Height = 1;
                    Width = 1;
                    Price = 500;
                    break;
                case Bomb.BombTypes.Mine:
                    Height = 2;
                    Width = 2;
                    Price = 750;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}