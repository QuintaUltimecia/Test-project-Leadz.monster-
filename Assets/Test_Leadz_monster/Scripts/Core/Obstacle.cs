using UnityEngine;
using System.Collections;

namespace Test_Leadz_monster.Scripts.Core
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private float _timeToSpawn;

        private Transform _transform;

        private Vector2 _startPosition;
        private Vector2 _nextPosition = new Vector2(-20, 0);

        private bool _isMove = false;

        private Coroutine _spawnRoutine;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _startPosition = _transform.position;
        }

        private void OnEnable()
        {
            _spawnRoutine = StartCoroutine(TimeToSpawn());
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnRoutine);
            _isMove = false;
            _transform.position = _startPosition;
        }

        private void Update()
        {
            if (_isMove == true)
            {
                _transform.Translate(GetDirection());

                if (Vector2.Distance(_transform.position, _nextPosition) < 1)
                {
                    _transform.position = _startPosition;
                    _isMove = false;
                    _spawnRoutine = StartCoroutine(TimeToSpawn());
                }
            }
        }

        private IEnumerator TimeToSpawn()
        {
            yield return new WaitForSeconds(_timeToSpawn);
            _isMove = true;
            StopCoroutine(_spawnRoutine);
        }

        private Vector2 GetDirection()
        {
            Vector2 direction = Vector2.left * _moveSpeed * Time.deltaTime;

            return direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                player.Death();
        }
    }
}
