using UnityEngine;
using Test_Leadz_monster.Scripts.Interfaces;

namespace Test_Leadz_monster.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, ITakeInput
    {
        [SerializeField]
        private float _pushForce;

        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        private void OnEnable()
        {
            _transform.position = Vector3.zero;
        }

        public void SetVerticalSpeed(float value)
        {
            if (value < 1)
                value = 1;
            else if (value > 5)
                value = 5;

            _rigidbody2D.angularDrag = value;

            _rigidbody2D.gravityScale += (value / 10);

            if (_rigidbody2D.gravityScale > 2)
                _rigidbody2D.gravityScale = 2;
        }

        public void MoveUp()
        {
            Vector2 force = Vector2.up * _pushForce;

            _rigidbody2D.AddForce(force);
        }

        public Vector2 Position { get => _transform.position; }
    }
}
