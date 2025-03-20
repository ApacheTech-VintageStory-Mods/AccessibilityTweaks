
using ApacheTech.Common.BrighterSlim;
using Gantry.Core.Annotation;
using Gantry.Services.Brighter.Filters;
using Gantry.Services.BrighterChat;

namespace ApacheTech.VintageMods.AccessibilityTweaks;

public sealed class Program : ModHost
{
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        api.ChatCommands
            .Create("brighter")
            .WithMediatedHandler<BrighterCommand>();
    }
}

public class BrighterCommand : MediatedChatCommand;

public class BrighterCommandHandler : RequestHandler<BrighterCommand>
{
    [HandledOnClient]
    public override BrighterCommand Handle(BrighterCommand command)
    {
        command.Result = TextCommandResult.Success("Yayyyyyyy!");
        return base.Handle(command);
    }
}