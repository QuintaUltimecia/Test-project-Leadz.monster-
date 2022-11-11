using UnityEngine;

namespace Test_Leadz_monster.Scripts.DI
{
    public class DIContainer : MonoBehaviour
    {
        [SerializeField]
        private Core.Player _playerPrefab;

        [SerializeField]
        private Input.WindowsInput _windowsInput;

        [SerializeField]
        private Input.AndroidInput _androidInput;

        [SerializeField]
        private Managers.GameManager _gameManager;

        [SerializeField]
        private Controllers.WallsController _wallController;

        [SerializeField]
        private Core.Obstacle _obstacle;

        private void Awake()
        {
            _playerPrefab = Instantiate(_playerPrefab);
            _playerPrefab.gameObject.SetActive(false);
            _playerPrefab.transform.position = Vector3.zero;

            _windowsInput.InitTakeInput(_playerPrefab.GetComponent<Controllers.PlayerController>());
            _androidInput.InitTakeInput(_playerPrefab.GetComponent<Controllers.PlayerController>());

            _gameManager.InitPlayer(_playerPrefab);
            _gameManager.InitWallsController(_wallController);
            _gameManager.InitObstacle(_obstacle);
        }
    }
}
