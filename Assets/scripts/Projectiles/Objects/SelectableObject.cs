using System;
using Audio;
using Game.Resources;
using Game.Resources.Profile;
using Projectiles.Towers;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Projectiles.Objects
{
    public class SelectableObject : MonoBehaviour //Base class for all selectable objects
    {
        internal float Price;
        internal int Height, Width;
        public ObjectType ObjectType;

        private EventTrigger _eventTrigger;
        public bool Selected, PlaceAble;

        public void Start()
        {
            _eventTrigger = GetComponent<EventTrigger>();
            var entryDrag = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            entryDrag.callback.AddListener(x => OnMouseDown());
            _eventTrigger.triggers.Add(entryDrag);
        }

        public void OnMouseDown()
        {
            UserInterfaceContainer.Instance.GridProvider.CreateGrid(Height, Width);
            Selected = true;
            Debug.Log("Mouse down");
        } //MouseDrag event. We should call Grid.Draw() here.

        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && Selected)
                Deselect();

            if (Camera.main == null || !Selected)
                return;
            Calculate();
        }

        protected virtual void Calculate()
        {
            var converted = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var rounded = new Vector3(Mathf.RoundToInt(converted.x * 2) * 0.5f,
                Mathf.RoundToInt(converted.y * 2) * 0.5f, 0);

            gameObject.transform.position = rounded;

            if (Selected)
            {
                PlaceAble = UserInterfaceContainer.Instance.GridProvider.IsPlaceAble();
            }

            UserInterfaceContainer.Instance.GridProvider.UpdateGridProperties(rounded, PlaceAble ? Color.cyan : Color.red, PlaceAble);

            if(Selected && PlaceAble && Input.GetMouseButtonDown(0))
                PlaceObject(rounded, ObjectType);
        }

        private void Deselect()
        {
            Selected = false;
            if(UserInterfaceContainer.Instance.GridProvider.IsAvailable)
                UserInterfaceContainer.Instance.GridProvider.RemoveGrid();
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            Debug.Log($"Placed new object with price {Price} and h&w {Height} & {Width}");
        }

        protected void PlaceObject(Vector3 position, ObjectType objectType) //TODO: probably we need handle this shit better than now
        {
            if (!ProfileInfo.Instance.Wallet.CanWithdraw((uint)Price))
                return;
            ProfileInfo.Instance.Wallet.Withdraw((uint)Price);
            ProfileInfo.Instance.Statistics.DefenderCreated();
            AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool.Build);
            switch (objectType)
            {
                case ObjectType.Defender:
                    GameObject gObject;
                    switch (gameObject.GetComponent<SelectableDefender>().DefType)
                    {
                        case DefenderType.ArcherTower:
                            gObject = Instantiate(ResourcesProvider.Instance.ObjectPool.Tower, position,
                                Quaternion.identity);
                            break;
                        case DefenderType.Cannon:
                            gObject = Instantiate(ResourcesProvider.Instance.ObjectPool.Cannon, position,
                                Quaternion.identity);
                            break;
                        case DefenderType.Mortar:
                            gObject = Instantiate(ResourcesProvider.Instance.ObjectPool.Mortar, position,
                                Quaternion.identity);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    ResourcesProvider.Instance.Pool.AddDefender(gObject.GetComponent<Defender>());
                    break;
                case ObjectType.Bomb:
                    Instantiate(ResourcesProvider.Instance.ObjectPool.Bomb, position, Quaternion.identity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }
        }
    }

    public enum ObjectType
    {
        Defender,
        Bomb
    }
}