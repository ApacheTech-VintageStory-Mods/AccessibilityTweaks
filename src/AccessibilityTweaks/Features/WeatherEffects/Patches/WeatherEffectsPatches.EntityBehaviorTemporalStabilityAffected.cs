﻿using ApacheTech.Common.Extensions.Harmony;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="EntityBehaviorTemporalStabilityAffected"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="WeatherEffectsSettings" />
public sealed partial class WeatherEffectsPatches
{
    private static SimpleParticleProperties _rustParticles;

    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "OnGameTick" method of the <see cref="EntityBehaviorTemporalStabilityAffected"/> class.
    /// </summary>
    /// <param name="___rustParticles">The instance of <see cref="SimpleParticleProperties"/> used by the original method.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "OnGameTick")]
    public static void Patch_EntityBehaviorTemporalStabilityAffected_OnGameTick_Postfix(ref SimpleParticleProperties ___rustParticles)
    {
        if (___rustParticles is null) return;
        if (!Settings.DustParticlesEnabled)
        {
            _rustParticles ??= ___rustParticles.CallMethod<SimpleParticleProperties>("MemberwiseClone");
            ___rustParticles.MaxSize = 0f;
            ___rustParticles.MinQuantity = 0f;
            ___rustParticles.AddQuantity = 0f;
            ___rustParticles.LifeLength = 0f;
            ___rustParticles.addLifeLength = 0f;
            return;
        }
        ___rustParticles = _rustParticles ?? ___rustParticles;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "OnGameTick" method of the <see cref="EntityBehaviorTemporalStabilityAffected"/> class.
    /// </summary>
    /// <param name="instructions">The list of instructions, written in MSIL, that make up the original method.</param>
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "OnGameTick")]
    public static IEnumerable<CodeInstruction> Patch_EntityBehaviorTemporalStabilityAffected_OnGameTick_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        // Removes involuntary mouse movement.
        return instructions.ToList().With(p => p[425].operand = -1.0);
    }
}