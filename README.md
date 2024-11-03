![378217004-09f8e32f-22f7-422b-be3c-4cf276b1527b](https://github.com/user-attachments/assets/1736c692-0d27-46ca-b862-9eae41d56b7a)


Original mod by [legoandmars](https://github.com/legoandmars/Utilla).

# Newtilla
A stable rewrite of Utilla that update dynamically using mostly harmony patches, this version should be less prone to breaking and is much simpler.

# Installation
First, get the original [Utilla](https://github.com/legoandmars/Utilla), and put it in your plugins. Then download [Newtilla](https://github.com/Loafiat/Newtilla/releases) and put that in your plugins aswell. Your done, enjoy!

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
