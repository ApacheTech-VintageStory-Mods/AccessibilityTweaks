//namespace AccessibilityTweaks.Features.CameraMovement.Patches;

///// <summary>
/////     Harmony Patches for the <see cref="EntityControls"/> class. This class cannot be inherited.
///// </summary>
//public sealed class EntityControlsPatches : GantrySettingsPatch<CameraMovementSettings>
//{
//    [HarmonyPrefix]
//    [HarmonyClientPatch(typeof(EntityControls), nameof(EntityControls.Sneak), MethodType.Setter)]
//    public static void Patch_EntityControls_Sneak_Prefix(EntityControls __instance, ref bool value)
//    {
//        var allMovementCaptured = Gantry.ApiEx.ClientMain.MouseGrabbed
//            || Gantry.ApiEx.Client.Settings.Bool["immersiveMouseMode"]
//            && Gantry.ApiEx.Client.Gui.OpenedGuis.All(g => !g.PrefersUngrabbedMouse);

//        var wasSneak = __instance.Sneak;
//        value = (value || (wasSneak && __instance.TriesToMove && Settings.ToggleSneak)) && allMovementCaptured;
//    }
//}