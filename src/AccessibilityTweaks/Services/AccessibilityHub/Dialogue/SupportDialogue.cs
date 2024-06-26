﻿using Gantry.Core.Extensions.Helpers;
using Gantry.Core.GameContent.GUI.Abstractions;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Dialogue;

/// <summary>
///     User interface that acts as a hub, to bring together all features within the mod.
/// </summary>
/// <seealso cref="GenericDialogue" />
[UsedImplicitly]
public sealed class SupportDialogue : GenericDialogue
{
    private float _row;
    private const float ButtonWidth = 350f;
    private const float HeightOffset = 0f;

    /// <summary>
    /// 	Initialises a new instance of the <see cref="SupportDialogue"/> class.
    /// </summary>
    /// <param name="capi">The client API.</param>
    public SupportDialogue(ICoreClientAPI capi) : base(capi)
    {
        Alignment = EnumDialogArea.CenterMiddle;
        Title = LangEx.ModTitle();
        ModalTransparency = 0f;
        ShowTitleBar = true;
    }

    /// <summary>
    ///     Gets an entry from the language files, for the feature this instance is representing.
    /// </summary>
    /// <param name="code">The entry to return.</param>
    /// <returns>A localised <see cref="string"/>, for the specified language file code.</returns>
    private static string LangEntry(string code) =>
        LangEx.FeatureString("Support.Dialogue", code);

    /// <summary>
    ///     Composes the header for the GUI.
    /// </summary>
    /// <param name="composer">The composer.</param>
    protected override void ComposeBody(GuiComposer composer)
    {
        AddButton(composer, LangEntry("Patreon"), OnPatreonButtonPressed);
        AddButton(composer, LangEntry("Donate"), OnDonateButtonPressed);
        AddButton(composer, LangEntry("Coffee"), OnCoffeeButtonPressed);
        AddButton(composer, LangEntry("Twitch"), OnTwitchButtonPressed);
        AddButton(composer, LangEntry("YouTube"), OnYouTubeButtonPressed);
        AddButton(composer, LangEntry("WishList"), OnWishListButtonPressed);
        AddButton(composer, LangEntry("Website"), OnWebsiteButtonPressed);
        IncrementRow(ref _row);
        AddButton(composer, LangEntry("Back"), TryClose);
    }

    private void AddButton(GuiComposer composer, string langEntry, ActionConsumable onClick)
    {
        composer.AddSmallButton(langEntry, onClick, ButtonBounds(ref _row, ButtonWidth, HeightOffset));
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

    private static bool OnDonateButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://bit.ly/APGDonate");
        return true;
    }

    private static bool OnPatreonButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://www.patreon.com/ApacheTechSolutions?fan_landing=true");
        return true;
    }

    private static bool OnWebsiteButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://apachegaming.net");
        return true;
    }

    private static bool OnCoffeeButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://www.buymeacoffee.com/Apache");
        return true;
    }

    private static bool OnTwitchButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://twitch.tv/ApacheGamingUK");
        return true;
    }

    private static bool OnYouTubeButtonPressed()
    {
        BrowserEx.TryOpenUrl("https://youtube.com/ApacheGamingUK");
        return true;
    }

    private static bool OnWishListButtonPressed()
    {
        BrowserEx.TryOpenUrl("http://amzn.eu/7qvKTFu");
        return true;
    }
}