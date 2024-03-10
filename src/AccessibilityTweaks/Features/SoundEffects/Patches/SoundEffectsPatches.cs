﻿using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.SoundEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="ILoadedSound"/> concrete implementation. This class cannot be inherited.
/// </summary>
/// <seealso cref="SoundEffectsSettings" />
[SettingsConsumer(EnumAppSide.Client)]
[HarmonySidedPatch(EnumAppSide.Client)]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed partial class SoundEffectsPatches : WorldSettingsConsumer<SoundEffectsSettings>;