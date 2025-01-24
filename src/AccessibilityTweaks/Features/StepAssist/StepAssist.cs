namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.StepAssist;

[UsedImplicitly]
public class StepAssist : ClientModSystem
{
    private bool _enabled;

    private string T(string key) => LangEx.FeatureString("StepAssist", key);

    public override void StartClientSide(ICoreClientAPI capi)
    {
        capi.ChatCommands
            .Create("step")
            .WithDescription(T("CommandDescription"))
            .HandleWith(_ =>
            {
                _enabled = !_enabled;
                var behaviour = capi.World.Player.Entity.GetBehavior<EntityBehaviorControlledPhysics>();
                behaviour.StepHeight = _enabled ? 1.2f : 0.6f;
                behaviour.stepUpSpeed = _enabled ? 0.14f : 0.07f;
                return TextCommandResult.Success(T(_enabled ? "Enabled" : "Disabled"));
            });
    }
}