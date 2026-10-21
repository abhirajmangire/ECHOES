using UnityEngine;
using UnityEngine.InputSystem;

namespace Echoes.Player
{
    /// <summary>Input abstraction: keyboard/mouse in Editor, on-screen touch in builds.
    /// Gameplay reads only these properties, never devices directly.</summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 Move { get; private set; }
        public Vector2 LookDelta { get; private set; }
        public bool SprintHeld { get; private set; }
        public bool InteractPressed { get; private set; }

        // Set by UI widgets (virtual joystick / look pad / buttons) on mobile.
        public Vector2 TouchMove { get; set; }
        public Vector2 TouchLook { get; set; }
        public bool TouchSprint { get; set; }
        public bool TouchInteract { get; set; }

        public float LookSensitivity = 0.12f;

        void Update()
        {
            Vector2 m = Vector2.zero; Vector2 look = Vector2.zero;
            bool sprint = false, interact = false;

#if UNITY_EDITOR || UNITY_STANDALONE
            var kb = Keyboard.current; var mouse = Mouse.current;
            if (kb != null)
            {
                m.x = (kb.dKey.isPressed ? 1 : 0) - (kb.aKey.isPressed ? 1 : 0);
                m.y = (kb.wKey.isPressed ? 1 : 0) - (kb.sKey.isPressed ? 1 : 0);
                sprint = kb.leftShiftKey.isPressed;
                interact = kb.eKey.wasPressedThisFrame;
            }
            if (mouse != null && mouse.rightButton.isPressed)
                look = mouse.delta.ReadValue() * LookSensitivity;
#endif
            Move = Vector2.ClampMagnitude(m + TouchMove, 1f);
            LookDelta = look + TouchLook * LookSensitivity;
            SprintHeld = sprint || TouchSprint;
            InteractPressed = interact || TouchInteract;
            TouchInteract = false; TouchLook = Vector2.zero;
        }
    }
}
