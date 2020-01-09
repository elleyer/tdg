using System;
using ScriptsData.Game.Resources;
using ScriptsData.Projectiles.Towers;
using ScriptsData.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScriptsData.Projectiles.Objects
{
    public class SelectableObject : MonoBehaviour //Base class for all selectable objects
    {
        public float Price;
        public int Height, Width;
        public ObjectType ObjectType;

        public SelectableManager SelectableManager;
        public ResourcesProvider ResourcesProvider;


        private UserInterfaceContainer _container;
        private Vector2 _defaultPosition;
        private EventTrigger _eventTrigger;
        private bool _selected, _placeAble;

        public void Start()
        {
            ResourcesProvider = SelectableManager.ResourcesProvider;
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

        private void PlaceObject(Vector3 position, ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Defender:
                    switch (gameObject.GetComponent<SelectableDefender>().DefType)
                    {
                        case DefenderType.ArcherTower:
                            Instantiate(ResourcesProvider.ObjectPool.Tower, position, Quaternion.identity);
                            break;
                        case DefenderType.Cannon:
                            Instantiate(ResourcesProvider.ObjectPool.Cannon, position, Quaternion.identity);
                            break;
                        case DefenderType.Mortar:
                            Instantiate(ResourcesProvider.ObjectPool.Mortar, position, Quaternion.identity);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ObjectType.Bomb:
                    Instantiate(ResourcesProvider.ObjectPool.Bomb, position, Quaternion.identity);
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