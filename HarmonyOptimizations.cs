using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using UnityEngine;
using Harmony;

namespace FSMO.HarmonyOptimizations
{
    // -------------------- Misc --------------------

    [HarmonyPatch(typeof(SetMainCamera), "OnEnter")]
    static class HookMainCamera
    {
        [HarmonyPriority(0)]
        static bool Prefix(SetMainCamera __instance)
        {
            if (__instance.gameObject.Value != null)
            {
                var cam = Camera.main;
                if (cam) cam.tag = "Untagged";
                __instance.gameObject.Value.tag = "MainCamera";
                FSMO.cam = __instance.gameObject.Value.GetComponent<Camera>();
            }
            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(Fsm), "UpdateDelayedEvents")]
    static class UpdateDelayedEvents
    {
        [HarmonyPriority(0)]
        static bool Prefix(List<DelayedEvent> ___delayedEvents)
        {
            for (var i = 0; i < ___delayedEvents.Count; i++)
            {
                ___delayedEvents[i].Update();
                if (___delayedEvents[i].Finished)
                {
                    ___delayedEvents.RemoveAt(i);
                    i--;
                }
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(ActionHelpers), "IsMouseOver")]
    static class IsMouseOver
    {
        static RaycastHit hit;

        [HarmonyPriority(0)]
        static bool Prefix(ref bool __result, GameObject gameObject, float distance, int layerMask)
        {
            if (gameObject == null)
            {
                __result = false;
                return false;
            }
            hit = FSMO.raycast(layerMask);
            if (hit.distance <= distance && hit.collider != null && hit.collider.gameObject == gameObject) __result = true;
            return false;
        }
    }

    [HarmonyPatch(typeof(ActionHelpers), "LayerArrayToLayerMask")]
    static class LayerArrayToLayerMask
    {
        static int num;
        static int i;

        [HarmonyPriority(0)]
        static bool Prefix(ref int __result, FsmInt[] layers, bool invert)
        {
            num = 0;
            for (i = 0; i < layers.Length; i++) num |= 1 << layers[i].Value;
            if (invert) num = ~num;
            if (num != 0) __result = num;
            else __result = -5;
            return false;
        }
    }

    // -------------------- Variables --------------------

    [HarmonyPatch(typeof(FsmVariables), "FindVariable")]
    static class FindVariable
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref NamedVariable __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.FloatVariables.Length; i++)
                if (__instance.FloatVariables[i].Name == name)
                {
                    __result = __instance.FloatVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.IntVariables.Length; i++)
                if (__instance.IntVariables[i].Name == name)
                {
                    __result = __instance.IntVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.BoolVariables.Length; i++)
                if (__instance.BoolVariables[i].Name == name)
                {
                    __result = __instance.BoolVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.Vector2Variables.Length; i++)
                if (__instance.Vector2Variables[i].Name == name)
                {
                    __result = __instance.Vector2Variables[i];
                    return false;
                }
            for (var i = 0; i < __instance.Vector3Variables.Length; i++)
                if (__instance.Vector3Variables[i].Name == name)
                {
                    __result = __instance.Vector3Variables[i];
                    return false;
                }
            for (var i = 0; i < __instance.StringVariables.Length; i++)
                if (__instance.StringVariables[i].Name == name)
                {
                    __result = __instance.StringVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.RectVariables.Length; i++)
                if (__instance.RectVariables[i].Name == name)
                {
                    __result = __instance.RectVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.ColorVariables.Length; i++)
                if (__instance.ColorVariables[i].Name == name)
                {
                    __result = __instance.ColorVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.MaterialVariables.Length; i++)
                if (__instance.MaterialVariables[i].Name == name)
                {
                    __result = __instance.MaterialVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.TextureVariables.Length; i++)
                if (__instance.TextureVariables[i].Name == name)
                {
                    __result = __instance.TextureVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.ObjectVariables.Length; i++)
                if (__instance.ObjectVariables[i].Name == name)
                {
                    __result = __instance.ObjectVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.GameObjectVariables.Length; i++)
                if (__instance.GameObjectVariables[i].Name == name)
                {
                    __result = __instance.GameObjectVariables[i];
                    return false;
                }
            for (var i = 0; i < __instance.QuaternionVariables.Length; i++)
                if (__instance.QuaternionVariables[i].Name == name)
                {
                    __result = __instance.QuaternionVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmFloat")]
    static class FindFsmFloat
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmFloat __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.FloatVariables.Length; i++)
                if (__instance.FloatVariables[i].Name == name)
                {
                    __result = __instance.FloatVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmObject")]
    static class FindFsmObject
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmObject __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.ObjectVariables.Length; i++)
                if (__instance.ObjectVariables[i].Name == name)
                {
                    __result = __instance.ObjectVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmMaterial")]
    static class FindFsmMaterial
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmMaterial __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.MaterialVariables.Length; i++)
                if (__instance.MaterialVariables[i].Name == name)
                {
                    __result = __instance.MaterialVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmTexture")]
    static class FindFsmTexture
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmTexture __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.TextureVariables.Length; i++)
                if (__instance.TextureVariables[i].Name == name)
                {
                    __result = __instance.TextureVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmInt")]
    static class FindFsmInt
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmInt __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.IntVariables.Length; i++)
                if (__instance.IntVariables[i].Name == name)
                {
                    __result = __instance.IntVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmBool")]
    static class FindFsmBool
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmBool __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.BoolVariables.Length; i++)
                if (__instance.BoolVariables[i].Name == name)
                {
                    __result = __instance.BoolVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmString")]
    static class FindFsmString
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmString __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.StringVariables.Length; i++)
                if (__instance.StringVariables[i].Name == name)
                {
                    __result = __instance.StringVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmVector2")]
    static class FindFsmVector2
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmVector2 __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.Vector2Variables.Length; i++)
                if (__instance.Vector2Variables[i].Name == name)
                {
                    __result = __instance.Vector2Variables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmVector3")]
    static class FindFsmVector3
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmVector3 __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.Vector3Variables.Length; i++)
                if (__instance.Vector3Variables[i].Name == name)
                {
                    __result = __instance.Vector3Variables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmRect")]
    static class FindFsmRect
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmRect __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.RectVariables.Length; i++)
                if (__instance.RectVariables[i].Name == name)
                {
                    __result = __instance.RectVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmQuaternion")]
    static class FindFsmQuaternion
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmQuaternion __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.QuaternionVariables.Length; i++)
                if (__instance.QuaternionVariables[i].Name == name)
                {
                    __result = __instance.QuaternionVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmColor")]
    static class FindFsmColor
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmColor __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.ColorVariables.Length; i++)
                if (__instance.ColorVariables[i].Name == name)
                {
                    __result = __instance.ColorVariables[i];
                    return false;
                }
            return false;
        }
    }

    [HarmonyPatch(typeof(FsmVariables), "FindFsmGameObject")]
    static class FindFsmGameObject
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref FsmGameObject __result, FsmVariables __instance, string name)
        {
            for (var i = 0; i < __instance.GameObjectVariables.Length; i++)
                if (__instance.GameObjectVariables[i].Name == name)
                {
                    __result = __instance.GameObjectVariables[i];
                    return false;
                }
            return false;
        }
    }
}