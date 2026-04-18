using AccessibilityTweaks.Features.AutoHotkeys.Dialogue;
using Gantry.Utilities;

namespace AccessibilityTweaks.Features.AutoHotkeys;

internal class AutoHotkeys : ClientModSystem<AutoHotkeys>, IClientServiceRegistrar
{
    private AutoHotkeySettings _settings = default!;
    private List<AutoHotkey>? _keys;

    internal AutoHotkey AutoWalk { get; set; } = default!;
    internal AutoHotkey AutoSprint { get; set; } = default!;
    internal AutoHotkey AutoSneak { get; set; } = default!;

    public void ConfigureClientModServices(IServiceCollection services, ICoreGantryAPI gantry)
    {
        services.AddFeatureWorldSettings<AutoHotkeySettings>();
        services.AddSingleton<AutoHotkeysDialogue>();
    }

    public override void StartClientSide(ICoreClientAPI capi)
    {
        _settings = Core.Resolve<AutoHotkeySettings>();
        _keys =
        [
            AutoWalk = new AutoHotkey(Core, "walkforward", enabled: _settings.AutoWalkEnabled, interrupts: ["sitdown"]),
            AutoSprint = new AutoHotkey(Core, "sprint", enabled: _settings.AutoSprintEnabled, interrupts: ["sitdown"]),
            AutoSneak = new AutoHotkey(Core, "sneak", enabled: _settings.AutoSneakEnabled, interrupts: ["sitdown"])
        ];

        capi.Event.KeyUp += OnKeyUp;
        capi.Event.KeyDown += OnKeyDown;

        _settings.AddPropertyChangedAction(p => p.AutoWalkEnabled, AutoWalk.SetEnabled);
        _settings.AddPropertyChangedAction(p => p.AutoSneakEnabled, AutoSneak.SetEnabled);
        _settings.AddPropertyChangedAction(p => p.AutoSprintEnabled, AutoSprint.SetEnabled);

        capi.AddAccessibilityHubDialogue<AutoHotkeysDialogue>("AutoHotkey");
    }

    private void OnKeyUp(KeyEvent e)
    {
        _keys?.ForEach(p => p.Trigger(e));
        e.Handled = false;
    }

    private void OnKeyDown(KeyEvent e)
    {
        _keys?.ForEach(p => p.Interrupt(e));
        e.Handled = false;
    }

    public override void Dispose()
    {
        Capi.Event.KeyUp -= OnKeyUp;
        Capi.Event.KeyDown -= OnKeyDown;
        _keys?.ForEach(p => p.Dispose());
    }
}