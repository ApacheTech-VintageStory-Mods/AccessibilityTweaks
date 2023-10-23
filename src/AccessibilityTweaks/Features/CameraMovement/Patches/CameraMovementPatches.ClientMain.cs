namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="ClientMain"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed partial class CameraMovementPatches
{
    /// <summary>
    ///     Applies a Postfix patch to the "AddCameraShake" method in <see cref="ClientMain"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ClientMain), "AddCameraShake")]
    public static void Patch_ClientMain_AddCameraShake_Prefix(ref float strength)
    {
        strength *= Settings.CameraShakeMultiplier;
    }
}