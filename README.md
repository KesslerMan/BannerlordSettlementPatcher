# BannerlordSettlementPatcher
## Brief description
A library for the videogame Mount & Blade II Bannerlord.

This library allows modders to load custom settlements into the game without needing to manually modify the sandbox module. Loads the submodule.xml and checks for any entries with id prefabs or settlements, then proceeds to insert them into the sandbox module settlements.xml and main_map/scene.xscene files. Creates a backup and makes sure to restore the file to vanilla when the game ends. If it crashes, it will restore the file upon restart. If the game is updated by Taleworlds it will detect it and use the new settlements.xml and/or main_map/scene.xscene files making this method patch proof.

## Caveats
Since ultimately the library does resort to modifying the settlements.xml and scene.xscene files from the Sandbox module it is not exactly a clean solution. The library does its best to clean up, but if a crash occurs before the library's reset code is executed and a subsequent game launch occurs with no modules using this library then this will result in the modified native file staying modified. This is the only case I can come up with where any problems can occur. 

Therefore: Advise your mod users that if they desire to turn your module off they make sure that the game exited cleanly before doing so. The library will clean after itself if you call its function correctly untir OnGameEnd.

## Exposed functions of the patcher

OutputDebugCode(String input): Writes whatever string you feed it into a Patcherlog.txt file in your mods directory. Is useful for debugging purposes. The Patherlog.txt file will normally contain the patcher's progress, so you can debug faulty XML files easier. Calling this function just adds a line to the Patcherlog.txt file, it does not interfere with the Patcher's logging. 


## Usage guide

Within your Submodules code it is necessary to include the following code:
```C#
using SettlementPatcher;
// MAKE SURE "ModTest" IS CHANGED TO WHATEVER YOUR MODULE IS ACTUALLY CALLED!
/*
 * See this link for more info on proper naming
 * https://docs.bannerlordmodding.com/_xmldocs/submodule.html#element-descriptions
 */
namespace ModTest
{
    public class Main : MBSubModuleBase
    {
        // CURRENTLY HARDCODE EDITED:
        // spkingdoms.xml collegia
        // spclans.xml clan_opificum_1
        // heroes.xml lord_7_1
        // lords.xml lord_7_1

        protected int totalSeconds = 0;
        protected float passedSeconds = 0;

        protected bool isLoading = false;

        private Patcher patcher;
        // Player troop exists on the campaign map

        protected override void OnSubModuleLoad()
        {
            // You need to pass the name of your module folder for the patcher to work properly.
            patcher = new Patcher("ModTest");

            patcher.onSubModuleLoad();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            patcher.patch();

            isLoading = false;
        }        

        public override void OnGameEnd(Game game)
        {
            if (!isLoading)
                patcher.ResetFiles();
            base.OnGameEnd(game);
        }

        public override bool DoLoading(Game game)
        {
            isLoading = true;
            return base.DoLoading(game);
        }
    }
}
```
Every single line of code presented above is absolutly necessary and may not be omitted.
As a result you can do the following:
* you can have as many separate modules as you want all implement this patcher and it will work
* you don't have to worry about updates breaking your module
* you can load a different savegame while ingame (courtesy of the isLoading variable being set and read)
* you have handling for crashes to avoid corrupting the original settlements.xml file and scene.xscene file
* you however do NOT have protection against undesired consequences if your game crashes and you disable the module before it can exit cleanly. In this case the settlements.xml and scene.xscene files will remain altered. To fix this just run the module again and shut it down properly. It will clean after itself.

The actual settlements are completely defined within xml files in exactly the same way you'd expect to do it for the base game. An example is contained within the repository.


Basically you just add as many Settlements and corresponding prefabs files as you want to your submodule.xml and they will be loaded into the game automatically. 

