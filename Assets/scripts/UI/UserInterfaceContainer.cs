using UnityEngine;
using Utils.GameEditor;
using Screen = UI.Window.Screen;

namespace UI
{
    public class UserInterfaceContainer : MonoBehaviour
    {
        public static UserInterfaceContainer Instance;
        public GridProvider GridProvider;
        public InfoBlockData InfoBlockData;
        public CanvasGroup InfoBlock, SelectableBlock, UpgradeableBlock;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            Screen.Instance.InfoBlock = InfoBlock;
            Screen.Instance.SelectableBlock = SelectableBlock;
            Screen.Instance.UpgradeableBlock = UpgradeableBlock;
        }
    }
}