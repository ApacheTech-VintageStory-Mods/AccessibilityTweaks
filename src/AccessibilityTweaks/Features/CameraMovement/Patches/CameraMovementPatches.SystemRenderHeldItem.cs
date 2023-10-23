

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="SystemRenderHeldItem"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed partial class CameraMovementPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "rot" method in the <see cref="SystemRenderHeldItem"/> class.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SystemRenderHeldItem), "rot")]
    public static void Patch_SystemRenderHeldItem_rot_Postfix(ref float __result)
    {
        if (Settings.HeldItemBobbing) return;
        __result = 0;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "rot2" method in the <see cref="SystemRenderHeldItem"/> class.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SystemRenderHeldItem), "rot2")]
    public static void Patch_SystemRenderHeldItem_rot2_Postfix(ref float __result)
    {
        if (Settings.HeldItemBobbing) return;
        __result = 0;
    }
}