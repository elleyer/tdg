using System;
using Projectiles.Towers;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.GameEditor;

namespace Projectiles.Objects
{
    public class SelectableDefender : SelectableObject
    {
        public DefenderType DefType;
        private new void Start()
        {
            base.Start(); //Call base method
            switch (DefType) //We finally need to check what type of object we got. Different params for different types.
            {
                case DefenderType.Cannon:
                    Price = 200;
                    Height = 2;
                    Width = 2;
                    break;
                case DefenderType.ArcherTower:
                    Price = 400;
                    Height = 3;
                    Width = 3;
                    break;
                case DefenderType.Mortar:
                    Price = 500;
                    Height = 2;
                    Width = 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ObjectType = ObjectType.Defender; //We should say that our object is Defender.
        }
    }
}