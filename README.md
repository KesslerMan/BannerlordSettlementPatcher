# BannerlordSettlementPatcher
## Brief description
A library for the videogame Mount & Blade II Bannerlord.

This library allows modders to load custom settlements into the game without needing to manually modify the sandbox module. Loads the submodule.xml and checks for any entries with id prefabs or settlements, then proceeds to insert them into the sandbox module settlements.xml and main_map/scene.xscene files. Creates a backup and makes sure to restore the file to vanilla when the game ends. If it crashes, it will restore the file upon restart. If the game is updated by Taleworlds it will detect it and use the new settlements.xml and/or main_map/scene.xscene files making this method patch proof.

## Usage guide

Within your Submodules code it is necessary to include the following code:
```
using SettlementPatcher;

namespace YourModName
{
  public class modclass : MBSubModuleBase
  {
    private Patcher patcher;

    protected override void OnSubModuleLoad()
    {
      patcher = new Patcher("YourModName");
      patcher.onSubModuleLoad();
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
    {
      patcher.patch();
    }
  }
}
```

That's all you gotta do. The rest is completely defined within xml files in exactly the same way you'd expect to do it for the base game. 


Therefore you need to include your desired Settlements or Prefabs in the submodule.xml. For example if there were two files:
* ModuleData/foo.xml - containing settlements definition
* Prefabs/bar.xml - containing prefabs/game_entity definitions for the settlements.

```
        <XmlNode>                
            <XmlName id="Settlements" path="frugal"/>
            <IncludedGameTypes>
                <GameType value = "Campaign"/>
                <GameType value = "CampaignStoryMode"/>
            </IncludedGameTypes>
        </XmlNode>
        <XmlNode>                
            <XmlName id="prefabs" path="customprefab"/>
            <IncludedGameTypes>
                <GameType value = "Campaign"/>
                <GameType value = "CampaignStoryMode"/>
            </IncludedGameTypes>
        </XmlNode>   
```        
In ModuleData/foo.xml could look like
```
<?xml version="1.0" encoding="utf-8"?>
<Settlements>
 <!-- Name of a local clan hero-->
  <Settlement id="town_M1" name="{=!}Frugal" owner="Faction.clan_opificum_1" posX="392.018" posY="410.64" culture="Culture.empire" prosperity="7150" gate_posX="390.1465" gate_posY="411.1724">
    <Components>
      <Town id="town_comp_M1" is_castle="false" level="1" background_crop_position="0.0" background_mesh="menu_empire_seaside_1" wait_mesh="wait_empire_town" gate_rotation="0.408" />
    </Components>
    <Locations complex_template="LocationComplexTemplate.town_complex">
      <Location id="center" scene_name="empire_town_h" scene_name_1="empire_town_h" scene_name_2="empire_town_h" scene_name_3="empire_town_h" />
      <Location id="arena" scene_name="arena_empire_a" />
      <Location id="tavern" scene_name="empire_house_c_tavern_a" />
      <Location id="lordshall" scene_name_1="empire_castle_keep_a_l1_interior" scene_name_2="empire_castle_keep_a_l2_interior" scene_name_3="empire_castle_keep_a_l3_interior" />
      <Location id="prison" scene_name="empire_dungeon_a" />
      <Location id="house_1" scene_name="empire_house_d_interior_house" />
      <Location id="house_2" scene_name="empire_house_d_interior_house" />
      <Location id="house_3" scene_name="empire_house_d_interior_house" />
    </Locations>
    <CommonAreas>
      <Area type="Backstreet" name="{=a0MVffcN}Backstreet" />
      <Area type="Clearing" name="{=LWHIVshb}Clearing" />
      <Area type="Waterfront" name="{=Rr1cy5Sk}Waterfront" />
    </CommonAreas>
  </Settlement>
</Settlements>
```

Prefabs/bar.xml could look like:
```
<prefabs>
  <game_entity name="town_M1" old_prefab_name="">
  (...) your stuff here
  </game_entity>
</prefabs>
```
