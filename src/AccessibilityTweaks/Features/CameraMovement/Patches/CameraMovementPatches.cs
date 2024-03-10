using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

[SettingsConsumer(EnumAppSide.Client)]
[HarmonySidedPatch(EnumAppSide.Client)]
[UsedImplicitly(ImplicitUseTargetFlags.All)]
public sealed partial class CameraMovementPatches : WorldSettingsConsumer<CameraMovementSettings>;