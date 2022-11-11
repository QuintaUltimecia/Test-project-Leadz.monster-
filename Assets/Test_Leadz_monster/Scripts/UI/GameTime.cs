using UnityEngine;
using TMPro;

namespace Test_Leadz_monster.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameTime : MonoBehaviour
    {
        [SerializeField]
        private Managers.GameManager _gameManager;

        private string _description = "Time";

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _gameManager.OnUpdateGameTime += delegate (float value)
            {
                _text.text = $"{_description}: {(int)value}s";
            };
        }

        private void OnDisable()
        {
            _gameManager.OnUpdateGameTime -= delegate (float value)
            {
                _text.text = $"{_description}: {(int)value}s";
            };
        }
    }
}
