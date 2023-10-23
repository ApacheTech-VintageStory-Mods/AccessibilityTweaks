using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;

// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="ShaderProgramBase"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Harmony Transpiler Patch")]
public sealed partial class CameraMovementPatches
{
    private static readonly FieldInfo GlobalWorldWarp = typeof(DefaultShaderUniforms).GetField(nameof(DefaultShaderUniforms.GlobalWorldWarp));
    private static readonly FieldInfo GlitchStrength = typeof(DefaultShaderUniforms).GetField(nameof(DefaultShaderUniforms.GlitchStrength));
    private static readonly FieldInfo PerceptionEffectIntensity = typeof(DefaultShaderUniforms).GetField(nameof(DefaultShaderUniforms.PerceptionEffectIntensity));

    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "Use" method of the <see cref="ShaderProgramBase"/> class.
    /// </summary>
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(ShaderProgramBase), "Use")]
    public static IEnumerable<CodeInstruction> Patch_ShaderProgramBase_Use_Prefix(IEnumerable<CodeInstruction> instructions)
    {
        var list = new List<CodeInstruction>();
            
        foreach (var codeInstruction in instructions)
        {
            list.Add(codeInstruction);
            if (codeInstruction.LoadsField(GlobalWorldWarp))
            {
                var methodInfo = typeof(CameraMovementPatches).GetMethod(
                    "GetGlobalWarpMultiplier", BindingFlags.Static | BindingFlags.NonPublic);
                list.Add(new CodeInstruction(OpCodes.Call, methodInfo));
                list.Add(new CodeInstruction(OpCodes.Mul));
            }
            else if (codeInstruction.LoadsField(GlitchStrength))
            {
                var methodInfo = typeof(CameraMovementPatches).GetMethod(
                    "GetGlitchStrengthMultiplier", BindingFlags.Static | BindingFlags.NonPublic);
                list.Add(new CodeInstruction(OpCodes.Call, methodInfo));
                list.Add(new CodeInstruction(OpCodes.Mul));
            }
            else if(codeInstruction.LoadsField(PerceptionEffectIntensity))
            {
                var methodInfo = typeof(CameraMovementPatches).GetMethod(
                    "GetPerceptionWarpMultiplier", BindingFlags.Static | BindingFlags.NonPublic);
                list.Add(new CodeInstruction(OpCodes.Call, methodInfo));
                list.Add(new CodeInstruction(OpCodes.Mul));
            }
        }

        return list;
    }

    private static float GetGlobalWarpMultiplier() => Settings.GlobalWarpMultiplier;
    private static float GetGlitchStrengthMultiplier() => Settings.GlitchStrengthMultiplier;
    private static float GetPerceptionWarpMultiplier() => Settings.PerceptionWarpMultiplier;
}