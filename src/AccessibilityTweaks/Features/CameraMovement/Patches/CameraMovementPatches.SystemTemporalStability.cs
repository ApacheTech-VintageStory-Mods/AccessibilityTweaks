// ReSharper disable InconsistentNaming

using System.Reflection.Emit;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="SystemTemporalStability"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementPatches" />
public sealed partial class CameraMovementPatches
{
    private static bool _previousStormActive;
    private static bool _shadersReloaded;

    /// <summary>
    ///     Prepares the class prior to patches, and cleans up the class when the patches are disposed.
    /// </summary>
    [HarmonyPrepare]
    [HarmonyCleanup]
    public static void Patch_SystemTemporalStability_Prepare_Cleanup()
    {
        _previousStormActive = false;
        _shadersReloaded = false;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "onServerData" method of the <see cref="SystemTemporalStability"/> class.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="SystemTemporalStability"/> this patch has been applied to.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SystemTemporalStability), "onServerData")]
    public static void Patch_SystemTemporalStability_onServerData_Postfix(SystemTemporalStability __instance)
    {
        var currentStormActive = __instance.StormData.nowStormActive;
        if (currentStormActive != _previousStormActive)
        {
            _previousStormActive = currentStormActive;
            _shadersReloaded = false;
        }
        if (_shadersReloaded) return;
        ApiEx.Client!.Shader.ReloadShadersThreadSafe();
        _shadersReloaded = true;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "OnGameTick" method of the <see cref="EntityBehaviorTemporalStabilityAffected"/> class.
    /// </summary>
    /// <param name="instructions">The list of instructions, written in MSIL, that make up the original method.</param>
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "OnGameTick")]
    public static IEnumerable<CodeInstruction> Patch_EntityBehaviorTemporalStabilityAffected_OnGameTick_Transpiler(IEnumerable<CodeInstruction> instructions)
        => instructions.Select(codeInstruction => codeInstruction.Is(OpCodes.Ldc_R8, 0.002d)
            ? CodeInstruction.Call(typeof(CameraMovementPatches), nameof(InvoluntaryMouseMovement))
            : codeInstruction);

    private static double InvoluntaryMouseMovement() => Settings.InvoluntaryMouseMovement ? 0.002d : 0d;
}