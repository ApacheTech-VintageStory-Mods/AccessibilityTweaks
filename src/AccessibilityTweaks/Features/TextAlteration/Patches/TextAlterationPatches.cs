using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

/// <summary>
///     Harmony Patches for the <see cref="TextAlteration"/> feature. This class cannot be inherited.
/// </summary>
/// <seealso cref="WorldSettingsConsumer{TextAlterationSettings}" />
[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed partial class TextAlterationPatches : WorldSettingsConsumer<TextAlterationSettings>;