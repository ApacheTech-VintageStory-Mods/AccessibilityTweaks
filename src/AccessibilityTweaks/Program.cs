namespace AccessibilityTweaks;

internal sealed class Program : G.Host<Program>;

internal class Sandbox : UniversalModSystem<Sandbox>
{
    public override void StartServerSide(ICoreServerAPI api)
    {
        api.ChatCommands.Create("thor")
            .RequiresPrivilege(Privilege.controlserver)
            .HandleWith(args =>
            {
                var system = Core.Resolve<SystemTemporalStability>();
                system.StormData.nextStormTotalDays = api.World.Calendar.TotalDays;
            });
    }
}