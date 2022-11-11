using UnityEngine;
using UnityEngine.EventSystems;

namespace Test_Leadz_monster.Scripts.UI
{
    public class TapPanel : MonoBehaviour, IPointerDownHandler
    {
        public delegate void DownHandler();
        public event DownHandler OnDown;

        public void OnPointerDown(PointerEventData eventData) =>
            OnDown?.Invoke();
    }
}
