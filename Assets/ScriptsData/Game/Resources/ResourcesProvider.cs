using System;
using Game.Resources.Profile;
using UnityEngine;
using Utils.Navigation;

namespace Game.Resources
{
    public class ResourcesProvider : MonoBehaviour
    {
        public static ResourcesProvider Instance = null;
        public Pool Pool;
        public ObjectPool ObjectPool;
        public PathCreator PathCreator;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }
    }
}