using UnityEngine.InputSystem;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerInputGameplay : MonoBehaviour
    {
        #region Variables
    
        public FrameInput frameInput { get; private set; }

        [SerializeField] private PlayerInput playerInput;
        
        private InputActionAsset _inputActions;
        private InputAction _move, _use, _dash, _pickUpOrPlace;
        
        #endregion

        #region Unity Methods

        private void OnEnable() {
            _inputActions.Enable();
        }

        private void OnDisable() {
            _inputActions.Disable();
        }
        
        private void Awake()
        {
            _inputActions = playerInput.actions;
            
            if (_inputActions == null)
            {
                return;
            }

            _move = _inputActions.FindAction("Move");
            _use = _inputActions.FindAction("Use");
            _pickUpOrPlace = _inputActions.FindAction("PickUp");
            _dash = _inputActions.FindAction("Dash");
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
                PickUpOrPlace = _pickUpOrPlace.WasPressedThisFrame(),
                Dash = _dash.WasPressedThisFrame()
            };
        }
        
        public struct FrameInput {
            public Vector2 Move;
            public bool Use;
            public bool PickUpOrPlace;
            public bool Dash;
        }
        
        #endregion
    }
}