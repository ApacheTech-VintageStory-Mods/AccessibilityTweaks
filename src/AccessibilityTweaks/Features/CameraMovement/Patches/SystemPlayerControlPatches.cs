using System.Threading;
using ApacheTech.Common.Extensions.Harmony;
using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="EntityControls"/> class. This class cannot be inherited.
/// </summary>
[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.All)]
public sealed class EntityControlsPatches : WorldSettingsConsumer<CameraMovementSettings>
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityControls), nameof(EntityControls.Sneak), MethodType.Setter)]
    public static void Patch_EntityControls_Sneak_Prefix(EntityControls __instance, ref bool value)
    {
        var allMovementCaptured = ApiEx.ClientMain.MouseGrabbed 
            || ApiEx.Client.Settings.Bool["immersiveMouseMode"] 
            && ApiEx.Client.Gui.OpenedGuis.All(g => !g.PrefersUngrabbedMouse);

        var wasSneak = __instance.Sneak;
        value = (value || (wasSneak && __instance.TriesToMove && Settings.ToggleSneak)) && allMovementCaptured;
    }
}