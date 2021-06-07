using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using UnityEngine;
using Harmony;

namespace FSMO.HarmonyOptimizations
{
    // -------------------- Misc --------------------

    [HarmonyPatch(typeof(Fsm), "UpdateDelayedEvents")]
    class UpdateDelayedEvents
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
    class DoMousePickEvent
    {
        [HarmonyPriority(0)]
        static bool Prefix(ref bool __result, GameObject gameObject, float distance, int layerMask)
        {
            var hit = FSMO.raycast(layerMask);
            if (hit.distance <= distance && hit.collider != null && hit.collider.gameObject == gameObject) __result = true;
            return false;
        }
    }

    // -------------------- Variables --------------------

    [HarmonyPatch(typeof(FsmVariables), "FindVariable")]
    class FindVariable
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
    class FindFsmFloat
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
    class FindFsmObject
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
    class FindFsmMaterial
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
    class FindFsmTexture
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
    class FindFsmInt
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
    class FindFsmBool
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
    class FindFsmString
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
    class FindFsmVector2
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
    class FindFsmVector3
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
    class FindFsmRect
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
    class FindFsmQuaternion
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
    class FindFsmColor
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
    class FindFsmGameObject
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

    // -------------------- Actions --------------------

    [HarmonyPatch(typeof(SetMainCamera), "OnEnter")]
    class HookMainCamera
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

    [HarmonyPatch(typeof(GetDistance), "DoGetDistance")]
    class DoGetDistance
    {
        [HarmonyPriority(0)]
        static bool Prefix(GetDistance __instance)
        {
            var go = __instance.Fsm.GetOwnerDefaultTarget(__instance.gameObject);
            if (!go) return false;
            if (__instance.target.Value == null) return false;
            __instance.storeResult.Value = Vector3.Distance(
                go.transform.position,
                __instance.target.Value.transform.position
            );
            return false;
        }
    }

    [HarmonyPatch(typeof(GetPosition), "DoGetPosition")]
    class DoGetPosition
    {
        [HarmonyPriority(0)]
        static bool Prefix(GetPosition __instance)
        {
            var go = __instance.Fsm.GetOwnerDefaultTarget(__instance.gameObject);
            if (!go) return false;
            __instance.vector.Value = __instance.space == Space.World ? go.transform.position : go.transform.localPosition;
            __instance.x.Value = __instance.vector.Value.x;
            __instance.y.Value = __instance.vector.Value.y;
            __instance.z.Value = __instance.vector.Value.z;
            return false;
        }
    }

    [HarmonyPatch(typeof(GetScale), "DoGetScale")]
    class DoGetScale
    {
        [HarmonyPriority(0)]
        static bool Prefix(GetScale __instance)
        {
            var go = __instance.Fsm.GetOwnerDefaultTarget(__instance.gameObject);
            if (!go) return false;
            __instance.vector.Value = __instance.space == Space.World ? go.transform.lossyScale : go.transform.localScale;
            __instance.xScale.Value = __instance.vector.Value.x;
            __instance.yScale.Value = __instance.vector.Value.y;
            __instance.zScale.Value = __instance.vector.Value.z;
            return false;
        }
    }

    [HarmonyPatch(typeof(GetRotation), "DoGetRotation")]
    class DoGetRotation
    {
        [HarmonyPriority(0)]
        static bool Prefix(GetRotation __instance)
        {
            var go = __instance.Fsm.GetOwnerDefaultTarget(__instance.gameObject);
            if (!go) return false;
            if (__instance.space == Space.World)
            {
                __instance.quaternion.Value = go.transform.rotation;
                __instance.vector.Value = go.transform.eulerAngles;
                __instance.xAngle.Value = go.transform.eulerAngles.x;
                __instance.yAngle.Value = go.transform.eulerAngles.y;
                __instance.zAngle.Value = go.transform.eulerAngles.z;
            }
            else
            {
                __instance.quaternion.Value = go.transform.localRotation;
                __instance.vector.Value = go.transform.localEulerAngles;
                __instance.xAngle.Value = go.transform.localEulerAngles.x;
                __instance.yAngle.Value = go.transform.localEulerAngles.y;
                __instance.zAngle.Value = go.transform.localEulerAngles.z;
            }
            return false;
        }
    }
}
