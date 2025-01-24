using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.All)]
public sealed partial class CameraMovementPatches : WorldSettingsConsumer<CameraMovementSettings>;