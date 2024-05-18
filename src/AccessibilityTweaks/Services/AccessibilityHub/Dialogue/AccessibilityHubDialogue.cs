

using ApacheTech.Common.DependencyInjection.Abstractions.Extensions;
using Gantry.Core.GameContent.GUI.Abstractions;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Dialogue;

/// <summary>
///     User interface that acts as a hub, to bring together all features within the mod.
/// </summary>
/// <seealso cref="GenericDialogue" />
[UsedImplicitly]
public sealed class AccessibilityHubDialogue : GenericDialogue
{
    private float _row;
    private const float ButtonWidth = 300f;
    private const float HeightOffset = 0f;
    private readonly Dictionary<Type, string> _dialogues;

    /// <summary>
    /// 	Initialises a new instance of the <see cref="AccessibilityHubDialogue"/> class.
    /// </summary>
    /// <param name="capi">The client API.</param>
    /// <param name="system">The mod system that controls this dialogue window.</param>
    public AccessibilityHubDialogue(ICoreClientAPI capi, AccessibilityHub system) : base(capi)
    {
        Alignment = EnumDialogArea.CenterMiddle;
        Title = LangEx.ModTitle();
        ModalTransparency = 0f;
        ShowTitleBar = true;
        _dialogues = system.FeatureDialogues;
    }

    /// <summary>
    ///     Gets an entry from the language files, for the feature this instance is representing.
    /// </summary>
    /// <param name="code">The entry to return.</param>
    /// <returns>A localised <see cref="string"/>, for the specified language file code.</returns>
    private static string LangEntry(string code) => 
        LangEx.FeatureString("AccessibilityHub.Dialogue", code);

    /// <summary>
    ///     Composes the header for the GUI.
    /// </summary>
    /// <param name="composer">The composer.</param>
    protected override void ComposeBody(GuiComposer composer)
    {
        foreach (var dialogue in _dialogues)
        {
            AddDialogueButton(composer, dialogue.Value, dialogue.Key);
        }
        IncrementRow(ref _row);
        AddButton(composer, LangEntry("Support"), OnDonateButtonPressed);
        AddButton(composer, Lang.Get("pause-back2game"), TryClose);
    }
        
    private void AddButton(GuiComposer composer, string langEntry, ActionConsumable onClick)
    {
        composer.AddSmallButton(langEntry, onClick, ButtonBounds(ref _row, ButtonWidth, HeightOffset));
    }

    private void AddDialogueButton(GuiComposer composer, string langEntry, Type dialogueType)
    {
        composer.AddSmallButton(langEntry, () => OpenDialogue(dialogueType), ButtonBounds(ref _row, ButtonWidth, HeightOffset));
    }

    private static void IncrementRow(ref float row) => row += 0.5f;

    private static ElementBounds ButtonBounds(ref float row, double width, double height)
    {
        IncrementRow(ref row);
        return ElementStdBounds
            .MenuButton(row, EnumDialogArea.LeftFixed)
            .WithFixedOffset(0, height)
            .WithFixedSize(width, 30);
    }

    private static bool OpenDialogue(Type type)
    {
        ((GuiDialog)IOC.Services.GetService(type)!).Toggle();
        return true;
    }

    private static bool OnDonateButtonPressed()
    {
        var dialogue = IOC.Services.Resolve<SupportDialogue>();
        return dialogue.TryOpen();
    }
}