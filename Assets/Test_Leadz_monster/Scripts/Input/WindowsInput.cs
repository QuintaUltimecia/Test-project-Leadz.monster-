using UnityEngine;
using Test_Leadz_monster.Scripts.Interfaces;

namespace Test_Leadz_monster.Scripts.Input
{
    public class WindowsInput : MonoBehaviour
    {
        private ITakeInput _takeInput;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                MoveUp();
        }

        private void MoveUp()
        {
            if (_takeInput == null)
                return;

            _takeInput.MoveUp();
        }

        public void InitTakeInput(ITakeInput takeInput) =>
            _takeInput = takeInput;
    }
}
