![pootilla](https://github.com/user-attachments/assets/f081b065-f763-427a-aaed-6ce63dff8aca)

Original mod by [Loafiat](https://github.com/Loafiat/Newtilla).

# Pootilla
A modified version of Newtilla (Version: Nov 7th) With poop mode enabled

# Installation
First, download [Newtilla](https://github.com/VTMGR/Pootilla/releases) and put that in your plugins. Optionally, you can download the original [Utilla](https://github.com/legoandmars/Utilla) aswell, I'd recommend it as most mods won't work without it. Your done, enjoy!

# Examples:

Mod support example:
```cs
using BepInEx;
using Newtilla;

public class TestMod : BaseUnityPlugin
{
    void Start()
    {
        //These events will run when a modded is joined or left.
        Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
        Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
    }

    void OnModdedJoined(string modeName)
    {
        //Run mod init
    }

    void OnModdedLeft(string modeName)
    {
        //Run mod deinit
    }
}

```

Custom game mode example:
```cs
using BepInEx;
using Newtilla;

public class TestMod : BaseUnityPlugin
{
    void Start()
    {
        //This creates and adds the mode to the mode selector, OnModeJoined and OnModeLeft aren't required.
        Newtilla.Newtilla.AddGameMode("TESTMODE", "TESTMODE", BaseGamemode.HUNT, false, OnModeJoined, OnModeLeft);
    }

    void OnModeJoined()
    {
        //Run mode init stuff
    }

    void OnModeLeft()
    {
        //Run mode deinit stuff
    }
}
```
Advanced game mode example:
```cs
using BepInEx;
using Newtilla;

public class TestMod : BaseUnityPlugin
{
    void Start()
    {
        //This creates and adds the mode to the mode selector, the base gamemode is replaced with "GHOST" here.
        //This is because it's a temporary gamemode so it's not included by default to avoid issues in the future
        //currently "AMBUSH" works too.
        Newtilla.Newtilla.AddGameMode("TESTMODE", "TESTMODE", "GHOST", false, OnModeJoined, OnModeLeft);
    }

    void OnModeJoined()
    {
        //Run mode init stuff
    }

    void OnModeLeft()
    {
        //Run mode deinit stuff
    }
}
```
