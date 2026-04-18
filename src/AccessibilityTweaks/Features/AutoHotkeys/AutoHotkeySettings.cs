namespace AccessibilityTweaks.Features.AutoHotkeys;

[ProtoContract]
public class AutoHotkeySettings : FeatureSettings<AutoHotkeySettings>
{
    [ProtoMember(1)]
    public bool AutoWalkEnabled { get; set; } = true;

    [ProtoMember(2)]
    public bool AutoSprintEnabled { get; set; } = true;

    [ProtoMember(3)]
    public bool AutoSneakEnabled { get; set; } = true;
}