using UnityEngine;
using TMPro;

namespace Test_Leadz_monster.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Attempts : MonoBehaviour
    {
        [SerializeField]
        private Managers.GameManager _gameManager;

        private string _description = "Attempts";

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _gameManager.OnUpdateAttempts += delegate (int value)
            {
                _text.text = $"{_description}: {value}";
            };
        }

        private void OnDisable()
        {
            _gameManager.OnUpdateAttempts -= delegate (int value)
            {
                _text.text = $"{_description}: {value}";
            };
        }
    }
}
