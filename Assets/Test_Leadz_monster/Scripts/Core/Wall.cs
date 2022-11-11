using UnityEngine;
using System.Collections;

namespace Test_Leadz_monster.Scripts.Core
{
    public class Wall : MonoBehaviour
    {
        private float _moveSpeed;

        private float _lifeTime;

        private Transform _transform;
        private GameObject _gameObject;

        private Coroutine _toDestroyRoutine;

        private void Awake()
        {
            _gameObject = gameObject;
            _transform = transform;
        }

        private void OnEnable()
        {
            _toDestroyRoutine = StartCoroutine(TimeToDestroy());
        }

        private void OnDisable()
        {
            StopCoroutine(_toDestroyRoutine);
        }

        private void Update()
        {
            _transform.Translate(GetDirection());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                player.Death();
        }

        private IEnumerator TimeToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);
            DestroyMe();
        }

        private Vector2 GetDirection()
        {
            Vector2 direction = Vector2.left * _moveSpeed * Time.deltaTime;

            return direction;
        }

        public void SetPosition(Vector2 position)
        {
            _transform.position = position;
        }

        public void DestroyMe()
        {
            _gameObject.SetActive(false);
        }

        public void SetLifeTime(float value)
        {
            _lifeTime = value;
        }

        public void SetMoveSpeed(float value)
        {
            _moveSpeed = value;
        }
    }
}
