using UnityEngine;
using System.Collections;

namespace Test_Leadz_monster.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gamePanel;

        [SerializeField]
        private GameObject _endGamePanel;

        private Core.Player _player;
        private Core.Obstacle _obstacle;
        private Controllers.PlayerController _playerController;
        private Controllers.WallsController _wallsController;

        private float _playerSpeed;
        private float _defaultplayerSpeed = 5;
        private float _speedIncreament = 0.5f;
        private float _timeToIncreament = 15;

        private float _gameTime = 0;
        private int _attempts = 0;
        private bool _isGameStart = false;

        private Coroutine _playerMoveSpeed;

        private SaveManager _saveManager;
        private SaveData _saveData;

        public delegate void UpdateGameTextHandler<T>(T obj);
        public event UpdateGameTextHandler<float> OnUpdateGameTime;
        public event UpdateGameTextHandler<int> OnUpdateAttempts;

        public enum Difficulty
        {
            Easy,
            Middle,
            Hard
        }

        private void OnEnable()
        {
            if (_player != null)
            {
                _player.OnDeath += delegate ()
                {
                    GameOver();
                };
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                _player.OnDeath -= delegate ()
                {
                    GameOver();
                };
            }
        }

        private void Start()
        {
            _saveManager = new SaveManager();
            _saveData = _saveManager.Load();

            _attempts = _saveData.Attempts;

            OnUpdateGameTime?.Invoke(_gameTime);
            OnUpdateAttempts?.Invoke(_attempts);
        }

        private void Update()
        {
            if (_isGameStart == true)
                UpdateGameTime();

            if (Vector2.Distance(_playerController.Position, Vector2.zero) > 20 
                && _isGameStart == true)
                _player.Death();
        }

        private void UpdateGameTime()
        {
            _gameTime += Time.deltaTime;

            OnUpdateGameTime?.Invoke(_gameTime);
        }

        private void UpdateAttempts()
        {
            _attempts++;

            _saveData.Attempts = _attempts;
            _saveManager.Save(_saveData);

            OnUpdateAttempts?.Invoke(_attempts);
        }

        private IEnumerator IncreasePlayerMoveSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeToIncreament);
                _playerSpeed -= _speedIncreament;
                _playerController.SetVerticalSpeed(_playerSpeed);
            }
        }

        public void InitPlayer(Core.Player value)
        {
            _player = value;
            _playerController = _player.GetComponent<Controllers.PlayerController>();

            _player.OnDeath += delegate ()
            {
                GameOver();
            };
        }

        public void InitWallsController(Controllers.WallsController value) =>
            _wallsController = value;

        public void InitObstacle(Core.Obstacle value) =>
            _obstacle = value;

        public void StartGameWithDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case (int)Difficulty.Easy:
                    _wallsController.SetWallMoveSpeed(2);
                    break;
                case (int)Difficulty.Middle:
                    _wallsController.SetWallMoveSpeed(3);
                    break;
                case (int)Difficulty.Hard:
                    _wallsController.SetWallMoveSpeed(4);
                    break;

                default:
                    _wallsController.SetWallMoveSpeed(2);
                    break;
            }

            StartGame();
        }

        public void GameOver()
        {
            _gamePanel.SetActive(false);
            _endGamePanel.SetActive(true);

            _wallsController.RecreatePool();
            _wallsController.StopWallsMove();

            _obstacle.gameObject.SetActive(false);

            _isGameStart = false;

            OnUpdateGameTime?.Invoke(_gameTime);
            OnUpdateAttempts?.Invoke(_attempts);

            StopCoroutine(_playerMoveSpeed);
        }

        public void StartGame()
        {
            _gamePanel.SetActive(true);
            _endGamePanel.SetActive(false);

            _wallsController.RecreatePool();
            _wallsController.StartWallsMove();

            _obstacle.gameObject.SetActive(true);

            _playerSpeed = _defaultplayerSpeed;
            _playerMoveSpeed = StartCoroutine(IncreasePlayerMoveSpeed());
            _player.gameObject.SetActive(true);

            _gameTime = 0;

            _isGameStart = true;

            UpdateAttempts();
        }
    }
}
