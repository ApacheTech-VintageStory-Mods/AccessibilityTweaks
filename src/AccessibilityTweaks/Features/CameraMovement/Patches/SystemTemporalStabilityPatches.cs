using Newtonsoft.Json.Linq;
using System.Reflection.Emit;
using System.Runtime;

namespace AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="SystemTemporalStability"/> class. This class cannot be inherited.
/// </summary>
public sealed class SystemTemporalStabilityPatches : GantrySettingsPatch<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "OnGameTick" method of the <see cref="EntityBehaviorTemporalStabilityAffected"/> class.
    /// </summary>
    /// <param name="instructions">The list of instructions, written in MSIL, that make up the original method.</param>
    [HarmonyTranspiler]
    [HarmonyClientPatch(typeof(EntityBehaviorTemporalStabilityAffected), "OnGameTick")]
    public static IEnumerable<CodeInstruction> Patch_EntityBehaviorTemporalStabilityAffected_OnGameTick_Transpiler(IEnumerable<CodeInstruction> instructions)
        => instructions.Select(codeInstruction => codeInstruction.Is(OpCodes.Ldc_R8, 0.002d)
            ? CodeInstruction.Call(typeof(SystemTemporalStabilityPatches), nameof(InvoluntaryMouseMovement))
            : codeInstruction);

    private static double InvoluntaryMouseMovement() 
        => Settings.InvoluntaryMouseMovement ? 0.002d : 0d;
}