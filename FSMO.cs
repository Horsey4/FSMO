using UnityEngine;
using MSCLoader;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Harmony;

namespace FSMO
{
    public class FSMO : Mod
    {
        public override string ID => "FSMO";
        public override string Name => "FSMO";
        public override string Author => "Horsey4";
        public override string Version => "1.1.0";
        public override bool LoadInMenu => true;
        internal static Camera cam;
        readonly static Assembly asm = Assembly.GetExecutingAssembly();
        readonly static HarmonyInstance instance = HarmonyInstance.Create("FSMO");
        readonly static Dictionary<int, RaycastHit> raycasts = new Dictionary<int, RaycastHit>();
        static GameObject fsmo;
        static RaycastHit hit;
        static Ray ray;
        static Ray last;

        public override void OnMenuLoad() => OnModEnabled();

        public override void OnLoad() => cam = Camera.main;

        public override void Update()
        {
            last = ray;
            if (cam)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (last.origin != ray.origin || last.direction != ray.direction) raycasts.Clear();
            }
        }

        public override void OnModDisabled() => instance.UnpatchAll("FSMO");

        public override void OnModEnabled()
        {
            if (!fsmo)
            {
                fsmo = new GameObject("FSMO");
                Object.DontDestroyOnLoad(fsmo);
                fsmo.AddComponent<FSMOMono>();
            }
            cam = Camera.main;
            instance.PatchAll(asm);
        }

        internal static RaycastHit raycast(int mask)
        {
            if (!cam) return default;
            if (raycasts.ContainsKey(mask)) return raycasts[mask];
            Physics.Raycast(ray, out hit, 3000, mask);
            raycasts.Add(mask, hit);
            return hit;
        }
    }

    class FSMOMono : MonoBehaviour
    {
        void OnLevelWasLoaded()
        {
            // DontDestroyOnLoad(gameObject);
            switch (Application.loadedLevelName)
            {
                case "GAME":
                case "MainMenu":
                    break;
                case "Ending":
                    FSMO.cam = GameObject.FindGameObjectsWithTag("MainCamera").First(x => x.name == "CAM").GetComponent<Camera>();
                    break;
                default:
                    FSMO.cam = Camera.main;
                    break;
            }
        }
    }
}