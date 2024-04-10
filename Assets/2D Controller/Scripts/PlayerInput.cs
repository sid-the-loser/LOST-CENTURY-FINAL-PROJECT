using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TarodevController
{
    public class PlayerInput : MonoBehaviour
    {
#if ENABLE_INPUT_SYSTEM
        private PlayerInputActions _actions;
        private InputAction _move, _jump, _dash, _looklock, _lookaxis, _attack, _throw, _stomp, _timeJump;

        private void Awake()
        {
            _actions = new PlayerInputActions();
            _move = _actions.Player.Move;
            _jump = _actions.Player.Jump;
            _dash = _actions.Player.Dash;
            _looklock = _actions.Player.LookLock;
            _lookaxis = _actions.Player.LookAxis;
            _attack = _actions.Player.Attack;
            _throw = _actions.Player.Throw;
            _stomp = _actions.Player.Stomp;
            _timeJump = _actions.Player.TImeJump;
        }

        private void OnEnable() => _actions.Enable();

        private void OnDisable() => _actions.Disable();

        public FrameInput Gather()
        {
            return new FrameInput
            {
                JumpDown = _jump.WasPressedThisFrame(),
                JumpHeld = _jump.IsPressed(),
                DashDown = _dash.WasPressedThisFrame(),
                Move = _move.ReadValue<Vector2>(),
                LookLock = _looklock.WasPressedThisFrame(),
                LookAxis = _lookaxis.ReadValue<Vector2>(),
                Attack = _attack.WasPressedThisFrame(),
                Throw = _throw.WasPressedThisFrame(),
                Stomp = _stomp.WasPressedThisFrame(),
                TimeJump = _timeJump.WasPressedThisFrame(),
            };
        }
#else
    public FrameInput Gather()
        {
            return new FrameInput
            {
                JumpDown = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.C),
                DashDown = Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };
        }
#endif
    }

    public struct FrameInput
    {
        public Vector2 Move;
        public bool JumpDown;
        public bool JumpHeld;
        public bool DashDown;
        public bool LookLock; // remove later
        public Vector2 LookAxis;
        public bool Attack;
        public bool Throw;
        public bool Stomp;
        public bool TimeJump;
    }
}