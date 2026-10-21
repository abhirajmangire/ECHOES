using UnityEngine;
using Echoes.Player;
using Echoes.CameraSystem;

namespace Echoes.Core
{
    /// <summary>Phase 1 only: builds a greybox corridor + room, player and camera at runtime
    /// so movement/camera feel can be tested in an empty scene. Replaced by real level prefabs later.</summary>
    public class PrototypeBootstrap : MonoBehaviour
    {
        void Start()
        {
            Block("Floor", new Vector3(0, -0.5f, 10), new Vector3(30, 1, 40));
            Block("CorridorL", new Vector3(-2.5f, 1.5f, 8), new Vector3(0.5f, 3, 16));
            Block("CorridorR", new Vector3(2.5f, 1.5f, 8), new Vector3(0.5f, 3, 16));
            Block("RoomBack", new Vector3(0, 1.5f, 30), new Vector3(20, 3, 0.5f));
            Block("RoomL", new Vector3(-10, 1.5f, 23), new Vector3(0.5f, 3, 14));
            Block("RoomR", new Vector3(10, 1.5f, 23), new Vector3(0.5f, 3, 14));

            var light = new GameObject("Dim Light").AddComponent<Light>();
            light.type = LightType.Directional; light.intensity = 0.6f;
            light.transform.rotation = Quaternion.Euler(50, -30, 0);
            RenderSettings.fog = true; RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = 0.03f; RenderSettings.fogColor = new Color(0.08f, 0.09f, 0.11f);

            var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player"; Destroy(player.GetComponent<CapsuleCollider>());
            player.transform.position = new Vector3(0, 1.1f, 0);
            var cc = player.AddComponent<CharacterController>(); cc.height = 2f; cc.radius = 0.4f;
            var input = player.AddComponent<PlayerInputHandler>();
            var pc = player.AddComponent<PlayerController>(); pc.input = input;

            var camGO = Camera.main ? Camera.main.gameObject : new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
            camGO.tag = "MainCamera";
            var cam = camGO.AddComponent<ThirdPersonCamera>();
            cam.target = player.transform; cam.input = input; pc.cameraTransform = camGO.transform;
        }

        static void Block(string n, Vector3 pos, Vector3 scale)
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.name = n; g.transform.position = pos; g.transform.localScale = scale;
        }
    }
}
