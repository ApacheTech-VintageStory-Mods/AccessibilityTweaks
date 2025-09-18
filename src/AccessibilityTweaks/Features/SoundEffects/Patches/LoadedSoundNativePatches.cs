using Vintagestory.Client;

namespace AccessibilityTweaks.Features.SoundEffects.Patches;

public sealed partial class LoadedSoundNativePatches : GantrySettingsPatch<SoundEffectsSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "GlobalVolume" getter method in <see cref="ILoadedSound"/> concrete class.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="ILoadedSound"/> this patch has been applied to.</param>
    /// <param name="__result">The <see cref="float"/> value passed back from the original method.</param>
    [HarmonyPostfix]
    [HarmonyClientPatch(typeof(LoadedSoundNative), "GlobalVolume", MethodType.Getter)]
    public static void Patch_LoadedSoundNative_GlobalVolume_Getter_Postfix(ILoadedSound __instance, ref float __result)
    {
        if (Settings.MuteAll != false)
        {
            __result = 0f;
            return;
        }
        var path = __instance.Params.Location?.ToString();
        var volumeOverride = Settings.SoundAssets.FirstOrDefault(p => p.Key == path).Value;
        if (volumeOverride is null) return;
        __result *= volumeOverride.Muted ? 0f : volumeOverride.VolumeMultiplier;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "SetPitchOffset" method in <see cref="ILoadedSound"/> concrete class.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="ILoadedSound"/> this patch has been applied to.</param>
    /// <param name="val">The <see cref="float"/> value passed into the original method.</param>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(LoadedSoundNative), nameof(LoadedSoundNative.SetPitchOffset))]
    public static void Patch_LoadedSoundNative_SetPitchOffset_Prefix(ILoadedSound __instance, ref float val)
    {
        var path = __instance.Params.Location?.ToString();
        var volumeOverride = Settings?.SoundAssets.FirstOrDefault(p => p.Key == path).Value;
        if (volumeOverride is null) return;
        val *= volumeOverride.PitchMultiplier;
    }
}