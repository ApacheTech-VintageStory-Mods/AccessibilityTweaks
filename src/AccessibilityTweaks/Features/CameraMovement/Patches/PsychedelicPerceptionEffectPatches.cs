using System.Reflection.Emit;

namespace AccessibilityTweaks.Features.CameraMovement.Patches;

public class PsychedelicPerceptionEffectPatches : GantrySettingsPatch<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "ApplyPerceptionWarp" method of the <see cref="EntityShapeRenderer"/> class.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyClientPatch(typeof(PsychedelicPerceptionEffect), nameof(PsychedelicPerceptionEffect.OnBeforeGameRender))]
    public static void Patch_PsychedelicPerceptionEffect_OnBeforeGameRender_Postfix()
    {
        G.Capi.Render.ShaderUniforms.PsychedelicStrength *= Settings.PsychedelicStrengthMultiplier;
        G.Capi.World.Player.Entity.HeadBobbingAmplitude *= Settings.PsychedelicStrengthMultiplier;
    }

    /// <summary>
    ///     Transpiles the <see cref="PsychedelicPerceptionEffect.OnBeforeGameRender(float)"/> method to adjust shader intensity
    ///     and conditionally remove involuntary mouse movement instructions based on the current camera movement settings.
    /// </summary>
    /// <param name="instructions">The original sequence of IL instructions.</param>
    /// <param name="il">The IL generator used to create labels for branching.</param>
    /// <returns>An enumerable collection of modified IL instructions.</returns>
    [HarmonyTranspiler]
    [HarmonyClientPatch(typeof(PsychedelicPerceptionEffect), "OnBeforeGameRender")]
    public static IEnumerable<CodeInstruction> Harmony_PsychedelicPerceptionEffect_OnBeforeGameRender_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        var newCodes = new List<CodeInstruction>();

        const int startIndex = 97;
        const int endIndex = 251;
        var skipLabel = new Label();

        for (var i = 0; i < codes.Count; i++)
        {
            var code = codes[i];

            if (code.opcode == OpCodes.Stfld &&
                code.operand is not null &&
                code.operand.ToString()!.Contains("PerceptionEffectIntensity"))
            {
                newCodes.Add(CodeInstruction.Call(typeof(PsychedelicPerceptionEffectPatches), nameof(PerceptionWarpMultiplier)));
                newCodes.Add(new CodeInstruction(OpCodes.Mul));
                newCodes.Add(code);
                continue;
            }

            if (i == startIndex)
            {
                newCodes.Add(CodeInstruction.Call(typeof(PsychedelicPerceptionEffectPatches), nameof(InvoluntaryMouseMovement)));
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

    public static float PerceptionWarpMultiplier()
        => Settings.PerceptionWarpMultiplier;

    public static bool InvoluntaryMouseMovement()
        => Settings.InvoluntaryMouseMovement;

    public static float PsychedelicStrengthMultiplier()
        => Settings.PsychedelicStrengthMultiplier;
}