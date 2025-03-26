using System.Reflection.Emit;
using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

// ReSharper disable InconsistentNaming

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
[HarmonyClientSidePatch]
public sealed class DrunkPerceptionEffectPatches : WorldSettingsConsumer<CameraMovementSettings>
{

    /// <summary>
    ///     Transpiles the <see cref="DrunkPerceptionEffect.OnBeforeGameRender(float)"/> method to adjust shader intensity
    ///     and conditionally remove involuntary mouse movement instructions based on the current camera movement settings.
    /// </summary>
    /// <param name="instructions">The original sequence of IL instructions.</param>
    /// <param name="il">The IL generator used to create labels for branching.</param>
    /// <returns>An enumerable collection of modified IL instructions.</returns>
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(DrunkPerceptionEffect), "OnBeforeGameRender")]
    public static IEnumerable<CodeInstruction> Harmony_DrunkPerceptionEffect_OnBeforeGameRender_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
        var codes = new List<CodeInstruction>(instructions);
        var newCodes = new List<CodeInstruction>();

        const int startIndex = 71;
        const int endIndex = 260;
        var skipLabel = new Label();

        for (var i = 0; i < codes.Count; i++)
        {
            var code = codes[i];

            if (code.opcode == OpCodes.Stfld &&
                code.operand is not null &&
                code.operand.ToString().Contains("PerceptionEffectIntensity"))
            {
                newCodes.Add(CodeInstruction.Call(typeof(DrunkPerceptionEffectPatches), nameof(PerceptionWarpMultiplier)));
                newCodes.Add(new CodeInstruction(OpCodes.Mul));
                newCodes.Add(code);
                continue;
            }

            if (i == startIndex)
            {
                newCodes.Add(CodeInstruction.Call(typeof(DrunkPerceptionEffectPatches), nameof(InvoluntaryMouseMovement)));
                newCodes.Add(new CodeInstruction(OpCodes.Brfalse, skipLabel));
            }

            newCodes.Add(code);

            if (i == endIndex)
            {
                var labelInstruction = new CodeInstruction(OpCodes.Nop);
                labelInstruction.labels.Add(skipLabel);
                newCodes.Add(labelInstruction);
            }
        }

        return newCodes;
    }

    /// <summary>
    ///     Retrieves the PerceptionWarpMultiplier from the static Settings.
    /// </summary>
    /// <returns>The multiplier to be applied to the perception effect intensity.</returns>
    public static float PerceptionWarpMultiplier() => Settings.PerceptionWarpMultiplier;

    /// <summary>
    ///     Retrieves the InvoluntaryMouseMovement flag from the static Settings.
    /// </summary>
    /// <returns><c>true</c> if involuntary mouse movement is enabled; otherwise, <c>false</c>.</returns>
    public static bool InvoluntaryMouseMovement() => Settings.InvoluntaryMouseMovement;
}