namespace AccessibilityTweaks.Features.StepAssist.Patches;

public sealed class EntityBehaviorControlledPhysicsPatches
{
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(EntityBehaviorControlledPhysics), "FindSteppableCollisionboxSmooth")]
    public static bool Harmony_EntityBehaviorControlledPhysics_FindSteppableCollisionBoxSmooth_Prefix(
        Cuboidd entityCollisionBox, ref Cuboidd entitySensorBox, Vec3d walkVector)
    {
        var walkVecNormalized = walkVector.Clone().Normalize();

        var widthOffsetX = Math.Abs(walkVecNormalized.Z * entityCollisionBox.Width * 0.5);
        entitySensorBox.X1 -= widthOffsetX;
        entitySensorBox.X2 += widthOffsetX;

        var widthOffsetZ = Math.Abs(walkVecNormalized.X * entityCollisionBox.Width * 0.5);
        entitySensorBox.Z1 -= widthOffsetZ;
        entitySensorBox.Z2 += widthOffsetZ;

        return true;
    }
}