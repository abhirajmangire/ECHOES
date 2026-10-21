using UnityEngine;

namespace Echoes.Player
{
    /// <summary>Camera-relative walk/run using CharacterController. Walking is default; running is opt-in.</summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerInputHandler input;
        public Transform cameraTransform;
        public float walkSpeed = 1.8f, runSpeed = 4.2f, turnSmoothing = 12f, gravity = -20f;

        CharacterController _cc;
        float _vy;

        void Awake() => _cc = GetComponent<CharacterController>();

        void Update()
        {
            Vector3 fwd = cameraTransform ? Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized : transform.forward;
            Vector3 right = Vector3.Cross(Vector3.up, fwd);
            Vector3 dir = fwd * input.Move.y + right * input.Move.x;

            float speed = input.SprintHeld ? runSpeed : walkSpeed;
            if (dir.sqrMagnitude > 0.0001f)
            {
                var target = Quaternion.LookRotation(dir.normalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f - Mathf.Exp(-turnSmoothing * Time.deltaTime));
            }

            _vy = _cc.isGrounded ? -1f : _vy + gravity * Time.deltaTime;
            _cc.Move((dir * speed + Vector3.up * _vy) * Time.deltaTime);
        }
    }
}
