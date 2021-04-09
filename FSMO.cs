#pragma warning disable CS0672 // Member overrides obsolete member

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
        public override string Version => "1.0.0";
        public override bool LoadInMenu => true;
        public static Camera cam;
        readonly static Assembly asm = Assembly.GetExecutingAssembly();
        readonly static HarmonyInstance instance = HarmonyInstance.Create("FSMO");
        readonly static Dictionary<int, RaycastHit> raycasts = new Dictionary<int, RaycastHit>();
        static GameObject fsmo;
        static Ray ray;

        public override void OnMenuLoad()
        {
            if (!fsmo)
            {
                fsmo = new GameObject("FSMO");
                Object.DontDestroyOnLoad(fsmo);
                fsmo.AddComponent<FSMOMono>();
            }
            instance.PatchAll(asm);
        }

        public override void OnLoad()
        {
            cam = Camera.main;
        }

        public override void Update()
        {
            if (cam) ray = cam.ScreenPointToRay(Input.mousePosition);
            raycasts.Clear();
        }

        public override void OnModDisabled() => instance.UnpatchAll("FSMO");

        public override void OnModEnabled() => instance.PatchAll(asm);

        public static RaycastHit raycast(int mask)
        {
            if (!cam) return default;
            if (raycasts.ContainsKey(mask)) return raycasts[mask];
            Physics.Raycast(ray, out var hit, float.PositiveInfinity, mask);
            raycasts.Add(mask, hit);
            return hit;
        }
    }

    class FSMOMono : MonoBehaviour
    {
        void OnLevelWasLoaded()
        {
            DontDestroyOnLoad(gameObject);
            switch (Application.loadedLevelName)
            {
                case "GAME":
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