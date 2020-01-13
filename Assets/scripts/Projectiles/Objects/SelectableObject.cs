using System;
using Game.Resources;
using Game.Resources.Profile;
using Projectiles.Bombs;
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
        internal SelectableManager SelectableManager;

        public ObjectType ObjectType;

        private UserInterfaceContainer _container;
        private Vector2 _defaultPosition;
        private EventTrigger _eventTrigger;
        private bool _selected, _placeAble;

        public void Start()
        {
            _container = GameObject.FindGameObjectWithTag("UI Container").GetComponent<UserInterfaceContainer>();

            var rect = GetComponent<RectTransform>();
            var anchoredPosition = rect.anchoredPosition;

            _defaultPosition = new Vector2(anchoredPosition.x,
                anchoredPosition.y);

            _eventTrigger = GetComponent<EventTrigger>();

            var entryDrag = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };

            entryDrag.callback.AddListener(x => OnMouseDown());

            _eventTrigger.triggers.Add(entryDrag);
        }

        public void OnMouseDown()
        {
            _container.GridProvider.CreateGrid(Height, Width);
            _selected = true;
            Debug.Log("Mouse down");
        } //MouseDrag event. We should call Grid.Draw() here.

        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && _selected)
                Deselect();

            if (Camera.main == null || !_selected)
                return;

            var converted = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var rounded = new Vector3(Mathf.RoundToInt(converted.x * 2) * 0.5f,
                Mathf.RoundToInt(converted.y * 2) * 0.5f, 0);

            gameObject.transform.position = rounded;

            if (_selected)
            {
                var hit = Physics2D.Raycast(Vector2.zero, rounded, Mathf.Infinity,
                    SelectableManager.LayerMask);
                Debug.DrawRay(Vector2.zero, rounded);
                if (hit.collider == null || hit.transform.gameObject.layer == LayerMask.NameToLayer("Placeable"))
                {
                    _placeAble = true;
                }

                else _placeAble = false;
            }

            _container.GridProvider.UpdateGridProperties(rounded, _placeAble ? Color.cyan : Color.red, _placeAble);

            if(_selected && _placeAble && Input.GetMouseButtonDown(0))
                PlaceObject(rounded, ObjectType);
        }

        private void Deselect()
        {
            _selected = false;
            if(_container.GridProvider.IsAvailable)
                _container.GridProvider.RemoveGrid();
            gameObject.transform.localPosition = new Vector3(_defaultPosition.x - 50, _defaultPosition.y + 50, 0);
            Debug.Log($"Placed new object with price {Price} and h&w {Height} & {Width}");
        }

        private void PlaceObject(Vector3 position, ObjectType objectType) //TODO: probably we need handle this shit better than now
        {
            GameObject gObject;
            if (!ProfileInfo.Instance.Wallet.CanWithdraw((uint)Price))
                return;
            ProfileInfo.Instance.Wallet.Withdraw((uint)Price);
            ProfileInfo.Instance.Statistics.DefendersCreated++;
            switch (objectType)
            {
                case ObjectType.Defender:
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
                    gObject = Instantiate(ResourcesProvider.Instance.ObjectPool.Bomb, position, Quaternion.identity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }
        }

/*        public void PlaceObject(Bomb.BombTypes bombType)
        {
            switch (bombType)
            {
                case Bomb.BombTypes.C4:
                    break;
                case Bomb.BombTypes.Mine:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bombType), bombType, null);
            }
        }*/
    }

    public enum ObjectType
    {
        Defender,
        Bomb
    }
}