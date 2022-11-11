using UnityEngine;

namespace Test_Leadz_monster.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        private GameObject _gameObject;

        public delegate void DeathHandler();
        public event DeathHandler OnDeath;

        private void Awake()
        {
            _gameObject = gameObject;
        }

        public void Death()
        {
            _gameObject.SetActive(false);

            OnDeath?.Invoke();
        }
    }
}
