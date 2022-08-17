using UnityEngine.InputSystem;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerInputGameplay : MonoBehaviour
    {
        #region Variables
    
        public FrameInput frameInput { get; private set; }
        
        [SerializeField] private InputActionAsset inputActions;
        private InputAction _move, _use, _dash, _pickUp;
        
        #endregion

        #region Unity Methods

        private void OnEnable() {
            inputActions.Enable();
        }

        private void OnDisable() {
            inputActions.Disable();
        }
        
        private void Awake()
        {
            if (inputActions == null)
            {
                return;
            }

            _move = inputActions.FindAction("Move");
            _use = inputActions.FindAction("Use");
            _pickUp = inputActions.FindAction("PickUp");
            _dash = inputActions.FindAction("Dash");
        }

        private void Update()
        {
            frameInput = GetFrameInput();
        }

        #endregion

        #region Methods

        private FrameInput GetFrameInput()
        {
            return new FrameInput{
                Move = _move.ReadValue<Vector2>(),
                Use  = _use.WasPressedThisFrame(),
                PickUp = _pickUp.WasPressedThisFrame(),
                Dash = _dash.WasPressedThisFrame()
            };
        }
        
        public struct FrameInput {
            public Vector2 Move;
            public bool Use;
            public bool PickUp;
            public bool Dash;
        }
        
        #endregion
    }
}