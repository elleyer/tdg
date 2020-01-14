using System;
using Projectiles.Bombs;
using UI;
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
                    Height = 1;
                    Width = 1;
                    Price = 750;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Calculate()
        {
            PlaceAble = true;
            var converted = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var rounded = new Vector3(Mathf.RoundToInt(converted.x * 2) * 0.5f,
                Mathf.RoundToInt(converted.y * 2) * 0.5f, 0);

            gameObject.transform.position = rounded;

            UserInterfaceContainer.Instance.GridProvider.UpdateGridProperties(rounded, PlaceAble ? Color.cyan : Color.red, PlaceAble);

            if(Selected && PlaceAble && Input.GetMouseButtonDown(0))
                PlaceObject(rounded, ObjectType);
        }
    }
}