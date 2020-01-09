using System.Collections.Generic;
using ScriptsData.Game.Resources;
using UnityEngine;

namespace ScriptsData.Projectiles.Objects
{
    public class SelectableManager : MonoBehaviour
    {
        public LayerMask LayerMask;
        public ResourcesProvider ResourcesProvider;
        public List<SelectableObject> SelectableObjects = new List<SelectableObject>();

        private void Awake()
        {
            foreach (var obj in GetComponentsInChildren<SelectableObject>())
            {
                SelectableObjects.Add(obj);
                obj.SelectableManager = this;
            }
        }
    }
}