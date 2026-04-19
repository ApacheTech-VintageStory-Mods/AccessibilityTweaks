using Gantry.Services.HarmonyPatches.Extensions;

namespace AccessibilityTweaks.Features.CameraMovement.Patches;

public sealed class EntityShapeRendererPatches : GantrySettingsPatch<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "DoRender3DOpaqueBatched" method of the <see cref="EntityShapeRenderer"/> class.
    /// </summary>
    /// <param name="instructions">The list of instructions, written in MSIL, that make up the original method.</param>
    [HarmonyTranspiler]
    [HarmonyClientPatch(typeof(EntityShapeRenderer), "DoRender3DOpaqueBatched")]
    public static IEnumerable<CodeInstruction> Patch_EntityShapeRenderer_DoRender3DOpaqueBatched_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        return instructions.MultiplyUniform<float>("glitchEffectStrength",
            $"{typeof(EntityShapeRendererPatches).FullName}:{nameof(GlitchEffectStrengthMultiplier)}");
    }

    private static float GlitchEffectStrengthMultiplier()
        => Settings.GlitchEffectStrengthMultiplier;
}