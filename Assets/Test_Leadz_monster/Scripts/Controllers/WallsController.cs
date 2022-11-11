using UnityEngine;
using System.Collections.Generic;
using Test_Leadz_monster.Scripts.Core;
using System.Collections;

namespace Test_Leadz_monster.Scripts.Controllers
{
    public class WallsController : MonoBehaviour
    {
        [SerializeField]
        private float _timeSpawn;

        [SerializeField]
        private int _wallCount;

        [SerializeField]
        private Wall _wallPrefab;

        private float _wallMoveSpeed;

        private List<Wall> _wallPool = new List<Wall>();

        private Transform _transform;
        private Camera _camera;

        private Vector3 _wallStartPosition;

        private Coroutine _wallSpawnRoutine;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }

        private void Start()
        {
            CreatePool(_wallCount);
        }

        private Vector2 GetPositionRelativeCamera()
        {
            float x = _camera.pixelHeight;
            float y = _camera.pixelWidth;

            Vector2 position = new Vector2(x, y);

            position = _camera.ScreenToWorldPoint(position);

            return position;
        }

        private Vector2 GetPositionRelativeCameraInverted()
        {
            float x = 0;
            float y = 0;

            Vector2 position = new Vector2(x, y);

            position = _camera.ScreenToWorldPoint(position);

            return position;
        }

        private Vector2 GetRandomStartPosition()
        {
            Vector2 position = _wallStartPosition;

            int y = Random.Range(-13, 13);

            if (y == 0 || y == 1)
                y = 2;
            else if (y == -1)
                y = -2;

            position = new Vector2(position.x, y);

            return position;
        }

        private IEnumerator SpawnWall()
        {
            while (true)
            {
                GetFreeWall();
                yield return new WaitForSeconds(_timeSpawn);
            }
        }

        private void GetFreeWall()
        {
            for (int i = 0; i < _wallCount; i++)
            {
                Wall wall = _wallPool[i];

                if (wall.gameObject.activeInHierarchy == false)
                {
                    wall.gameObject.SetActive(true);
                    wall.SetPosition(GetRandomStartPosition());

                    break;
                }
            }
        }

        private void CreatePool(int wallCount)
        {
            for (int i = 0; i < wallCount; i++)
            {
                Wall wall = Instantiate(_wallPrefab, _transform);

                int y = Random.Range(-9, 9);

                _wallStartPosition = new Vector2(
                    x: (GetPositionRelativeCamera().x),
                    y: y);

                Vector2 direction = new Vector2(
                    x: (GetPositionRelativeCameraInverted().x * 2),
                    y: y);

                float distance = Vector2.Distance(_wallStartPosition, direction);
                float time = distance / _wallMoveSpeed;

                wall.SetPosition(GetRandomStartPosition());
                wall.SetMoveSpeed(_wallMoveSpeed);
                wall.SetLifeTime(time);
                wall.DestroyMe();

                _wallPool.Add(wall);
            }
        }

        public void RecreatePool()
        {
            for (int i = 0; i < _wallPool.Count; i++)
            {
                int y = Random.Range(-9, 9);

                _wallStartPosition = new Vector2(
                    x: (GetPositionRelativeCamera().x),
                    y: y);

                Vector2 direction = new Vector2(
                    x: (GetPositionRelativeCameraInverted().x * 2),
                    y: y);

                float distance = Vector2.Distance(_wallStartPosition, direction);
                float time = distance / _wallMoveSpeed;

                _wallPool[i].SetPosition(GetRandomStartPosition());
                _wallPool[i].SetMoveSpeed(_wallMoveSpeed);
                _wallPool[i].SetLifeTime(time);
                _wallPool[i].DestroyMe();
            }
        }

        public void StartWallsMove() =>
            _wallSpawnRoutine = StartCoroutine(SpawnWall());

        public void StopWallsMove() =>
            StopCoroutine(_wallSpawnRoutine);

        public void SetWallMoveSpeed(float value) =>
            _wallMoveSpeed = value;
    }
}
