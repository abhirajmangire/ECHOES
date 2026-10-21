using UnityEngine;
using Echoes.Player;

namespace Echoes.CameraSystem
{
    /// <summary>Orbit follow camera with spherecast wall collision.</summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Transform target;
        public PlayerInputHandler input;
        public Vector3 pivotOffset = new Vector3(0, 1.5f, 0);
        public float distance = 3.5f, minPitch = -20f, maxPitch = 55f, followSmoothing = 14f;
        public float collisionRadius = 0.25f;
        public LayerMask collisionMask = ~0;

        float _yaw, _pitch = 15f, _currentDist;

        void Start() { _currentDist = distance; _yaw = target ? target.eulerAngles.y : 0f; }

        void LateUpdate()
        {
            if (!target) return;
            _yaw += input.LookDelta.x;
            _pitch = Mathf.Clamp(_pitch - input.LookDelta.y, minPitch, maxPitch);

            Vector3 pivot = target.position + pivotOffset;
            Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0);
            Vector3 back = rot * Vector3.back;

            float desired = distance;
            if (Physics.SphereCast(pivot, collisionRadius, back, out var hit, distance, collisionMask, QueryTriggerInteraction.Ignore)
                && !hit.collider.transform.IsChildOf(target))
                desired = Mathf.Max(0.4f, hit.distance);

            _currentDist = Mathf.Lerp(_currentDist, desired, 1f - Mathf.Exp(-followSmoothing * Time.deltaTime));
            transform.SetPositionAndRotation(pivot + back * _currentDist, rot);
        }
    }
}
