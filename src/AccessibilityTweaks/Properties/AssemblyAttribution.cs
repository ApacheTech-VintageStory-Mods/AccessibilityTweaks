[assembly: ModDependency("game", GameVersion.ShortGameVersion)]

[assembly:ModInfo(
    "Accessibility Tweaks",
    "accessibilitytweaks",
    Description = "Quality of Life changes to aid content creators, and those with motion/light/noise affected epilepsy, or light sensitivity.",
    Side = "Client",
#if DEBUG
    Version = "5.0.0-dev.1",
#else
    Version = "5.0.0-rc.8",
#endif
    NetworkVersion = "1.0.0",
    IconPath = "modicon.png",
    Website = "https://apachetech.co.uk",
    Contributors = ["ApacheTech Solutions"],
    Authors = ["ApacheTech Solutions"])]