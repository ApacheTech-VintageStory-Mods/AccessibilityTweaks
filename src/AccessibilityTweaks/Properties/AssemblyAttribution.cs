[assembly: ModDependency("game", GameVersion.ShortGameVersion)]

[assembly:ModInfo(
    "Accessibility Tweaks",
    "accessibilitytweaks",
    Description = "Quality of Life changes to aid content creators, and those with motion/light/noise affected epilepsy, or light sensitivity.",
#if DEBUG
    Side = "Universal",
    Version = "5.0.0-dev.0",
#else
    Side = "Client",
    Version = "5.0.0",
#endif
    NetworkVersion = GameVersion.NetworkVersion,
    IconPath = "modicon.png",
    Website = "https://apachetech.co.uk",
    Contributors = ["ApacheTech Solutions"],
    Authors = ["ApacheTech Solutions"])]