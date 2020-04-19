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
  <Settlement id="town_M1" name="{=!}Frugal" owner="Faction.clan_empire_north_6" posX="392.018" posY="410.64" culture="Culture.empire" prosperity="7150" gate_posX="390.1465" gate_posY="411.1724">
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
		<transform position="392.018, 410.64.313, 11.838" rotation_euler="0.000, 0.000, 0.463"/>
      <physics mass="1.000" />
      <children>
        <game_entity name="town_M1_main" old_prefab_name="">
          <physics mass="1.000" />
          <children>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <transform position="2.508, 1.125, -0.047" rotation_euler="-0.245, 0.276, 1.176" scale="0.785, 0.785, 0.785" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a">
                  <mesh name="mi_market_tent_a.0" factor="4287932504" />
                  <mesh name="mi_market_tent_a.1" factor="4287932504" />
                </meta_mesh_component>
              </components>
              <additional_features>
                <feature name="apply_factor_color_to_all_components" value="true">
                  <factor value="0.580, 0.659, 0.344, 1.000" />
                </feature>
              </additional_features>
            </game_entity>
            <game_entity name="map_icons_empire_tavern" old_prefab_name="">
              <transform position="-0.940, 1.120, -0.049" rotation_euler="0.000, 0.000, 0.611" scale="0.680, 0.680, 0.680" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tavern" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_tavern" old_prefab_name="">
              <transform position="-1.784, 0.487, -0.009" rotation_euler="0.000, 0.000, 0.000" scale="0.505, 0.505, 0.505" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tavern" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.202, 0.858, -0.168" rotation_euler="0.000, 0.000, 2.182" scale="0.556, 0.556, 0.556" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.402, 0.656, -0.190" rotation_euler="0.000, 0.000, 0.611" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.139, -0.164, -0.178" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.139, -0.529, -0.199" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.139, -0.774, -0.165" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.283, -0.504, -0.249" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.244, -0.125, -0.203" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.283, -0.765, -0.216" rotation_euler="0.000, 0.000, 1.571" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.330, -1.053, -0.205" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.614, -1.008, -0.285" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.628, -0.719, -0.298" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.628, -0.560, -0.282" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.871, -0.586, -0.310" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.863, -1.053, -0.289" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.871, -0.745, -0.298" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.152, -1.053, -0.328" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.152, -0.492, -0.349" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.152, -0.651, -0.356" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.152, -0.108, -0.287" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.855, -0.038, -0.264" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.578, -0.024, -0.218" rotation_euler="0.000, 0.000, -3.141" scale="0.604, 0.604, 0.604" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.401, 0.018, -0.268" rotation_euler="0.000, 0.000, 0.000" scale="0.554, 0.554, 0.554" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.893, 0.176, -0.298" rotation_euler="0.000, 0.000, 1.571" scale="0.850, 0.850, 0.850" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.893, -0.155, -0.358" rotation_euler="0.000, 0.000, 1.571" scale="0.850, 0.850, 0.850" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.972, -0.704, -0.441" rotation_euler="0.000, 0.000, -0.175" scale="0.660, 0.660, 0.660" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.484, -0.400, -0.340" rotation_euler="0.000, 0.000, 0.000" scale="0.543, 0.543, 0.543" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.505, -0.925, -0.388" rotation_euler="0.000, 0.000, 0.524" scale="0.544, 0.544, 0.544" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.194, 0.346, -0.177" rotation_euler="0.000, 0.000, -1.601" scale="0.608, 0.608, 0.608" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.167, 0.073, -0.187" rotation_euler="0.000, 0.000, -1.567" scale="0.608, 0.608, 0.608" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.669, 0.345, -0.215" rotation_euler="0.000, 0.000, 0.000" scale="0.587, 0.587, 0.587" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.147, 0.362, -0.238" rotation_euler="0.000, 0.000, 1.554" scale="0.587, 0.587, 0.587" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.354, 1.244, -0.063" rotation_euler="0.000, 0.000, 0.611" scale="0.688, 0.688, 0.688" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.087, 1.645, -0.094" rotation_euler="0.000, 0.000, -2.531" scale="0.688, 0.688, 0.688" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_monument_1" old_prefab_name="">
              <transform position="2.023, -0.227, -0.089" rotation_euler="0.000, 0.000, -0.175" scale="1.068, 1.068, 1.068" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_3_monument_1" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.316, -0.447, 0.101" rotation_euler="0.000, 0.000, 2.356" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.493, -0.625, 0.081" rotation_euler="0.000, 0.000, 2.356" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.672, -0.803, 0.122" rotation_euler="0.000, 0.000, 2.356" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.726, -0.492, 0.043" rotation_euler="0.000, 0.000, 0.785" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.529, -0.689, -0.012" rotation_euler="0.000, 0.000, 0.785" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.331, -0.887, -0.047" rotation_euler="0.000, 0.000, 0.785" scale="0.562, 0.562, 0.562" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.166, -1.031, 0.053" rotation_euler="0.000, 0.000, 0.000" scale="0.845, 0.845, 0.845" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.728, -0.992, 0.024" rotation_euler="0.000, 0.000, -1.585" scale="0.681, 0.681, 0.681" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.013, -0.699, 0.062" rotation_euler="0.000, 0.000, 0.009" scale="0.541, 0.541, 0.541" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.708, -0.269, 0.132" rotation_euler="0.000, 0.000, 0.873" scale="0.678, 0.678, 0.678" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.397, -0.008, 0.158" rotation_euler="0.000, 0.000, 0.873" scale="0.678, 0.678, 0.678" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.610, 0.327, 0.210" rotation_euler="0.000, 0.000, 2.443" scale="0.678, 0.678, 0.678" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.885, 0.780, 0.294" rotation_euler="0.000, 0.000, -0.611" scale="0.736, 0.736, 0.736" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="3.180, 1.162, 0.332" rotation_euler="0.000, 0.000, 0.960" scale="0.736, 0.736, 0.736" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="1.296, -0.485, -0.059" rotation_euler="0.000, 0.000, -0.785" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="1.710, -0.094, -0.059" rotation_euler="0.000, 0.000, -0.785" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="2.125, 0.329, -0.059" rotation_euler="0.000, 0.000, -0.698" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="2.388, 0.695, -0.059" rotation_euler="0.000, 0.000, 2.705" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_b" argument="0.530, 0.530, -0.010, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="2.412, 1.154, -0.059" rotation_euler="0.000, 0.000, -2.793" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_b" argument="0.530, 0.530, -0.010, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="2.310, 1.418, -0.059" rotation_euler="0.000, 0.000, -2.356" scale="0.352, 0.193, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="0.961, -0.709, -0.059" rotation_euler="0.000, 0.000, 2.007" scale="0.352, 0.352, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_b" argument="0.530, 0.530, -0.010, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="0.732, -0.787, -0.059" rotation_euler="0.000, 0.000, -1.571" scale="0.274, 0.195, 0.352" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="0.175, -0.351, -0.059" rotation_euler="0.000, 0.000, -1.571" scale="0.230, 0.230, 0.230" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="-0.232, -0.351, -0.059" rotation_euler="0.000, 0.000, -1.571" scale="0.230, 0.230, 0.230" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="-0.362, -0.338, -0.059" rotation_euler="0.000, 0.000, 1.309" scale="0.232, 0.232, 0.232" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_b" argument="0.530, 0.530, -0.010, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="-0.584, -0.305, -0.059" rotation_euler="0.000, 0.000, -1.745" scale="0.232, 0.232, 0.232" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_b" argument="0.530, 0.530, -0.010, 0.010" />
              </components>
            </game_entity>
            <game_entity name="empty_object" old_prefab_name="">
              <transform position="-0.852, -0.284, -0.059" rotation_euler="0.000, 0.000, -1.484" scale="0.187, 0.187, 0.187" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_battania_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.719, -0.250, -0.112" rotation_euler="0.000, 0.000, 0.000" scale="0.586, 0.586, 0.586" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.775, -0.574, -0.123" rotation_euler="0.000, 0.000, -1.585" scale="0.494, 0.494, 0.494" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.003, -0.239, -0.073" rotation_euler="0.000, 0.000, -0.016" scale="0.810, 0.810, 0.810" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.358, -0.068, -0.016" rotation_euler="0.000, 0.000, 2.358" scale="0.743, 0.743, 0.743" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.198, 0.296, -0.012" rotation_euler="0.000, 0.000, 0.105" scale="0.542, 0.542, 0.542" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.121, 0.610, -0.015" rotation_euler="0.000, 0.000, 2.137" scale="0.474, 0.474, 0.474" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.463, 0.807, 0.063" rotation_euler="0.000, 0.000, -0.934" scale="0.745, 0.745, 0.745" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.693, 0.332, 0.071" rotation_euler="0.000, 0.000, -2.485" scale="0.745, 0.745, 0.745" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.892, 1.049, 0.112" rotation_euler="0.000, 0.000, 0.599" scale="0.710, 0.710, 0.710" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.123, 1.132, 0.147" rotation_euler="0.000, 0.000, -0.983" scale="0.735, 0.735, 0.735" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.050, 0.612, 0.142" rotation_euler="0.000, 0.000, -0.863" scale="0.697, 0.697, 0.697" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.862, 1.364, 0.290" rotation_euler="0.000, 0.000, -0.890" scale="0.697, 0.697, 0.697" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.701, 1.611, 0.253" rotation_euler="0.000, 0.000, 0.611" scale="0.625, 0.625, 0.625" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.323, 2.064, 0.191" rotation_euler="0.000, 0.000, -0.971" scale="0.651, 0.651, 0.651" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.341, 2.536, 0.196" rotation_euler="0.000, 0.000, 0.068" scale="0.701, 0.701, 0.701" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.308, 2.946, 0.232" rotation_euler="0.000, 0.000, 1.656" scale="0.701, 0.701, 0.701" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.821, 3.508, 0.302" rotation_euler="0.000, 0.000, -1.027" scale="0.826, 0.826, 0.826" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.478, 3.155, 0.160" rotation_euler="0.000, 0.000, 0.586" scale="0.927, 0.927, 0.927" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.908, 3.407, 0.241" rotation_euler="0.000, 0.000, -0.980" scale="0.829, 0.829, 0.829" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.887, 2.794, 0.186" rotation_euler="0.000, 0.000, -0.043" scale="0.752, 0.752, 0.752" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.758, 3.946, 0.297" rotation_euler="0.000, 0.000, 0.468" scale="0.848, 0.848, 0.848" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.492, 4.171, -0.164" rotation_euler="0.000, 0.000, -2.537" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.822, 3.937, -0.211" rotation_euler="0.000, 0.000, -2.537" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_b" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.585, 1.832, -0.263" rotation_euler="0.000, 0.000, 2.181" scale="1.240, 1.240, 1.240" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_d" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.942, 2.734, -0.267" rotation_euler="0.000, 0.000, 0.012" scale="1.026, 1.026, 1.026" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_d" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.950, 3.438, -0.151" rotation_euler="0.000, 0.000, -0.974" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.738, 3.120, -0.096" rotation_euler="0.000, 0.000, -0.974" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_house_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.440, 2.736, -0.087" rotation_euler="0.000, 0.000, 0.589" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_house_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_well" old_prefab_name="">
              <transform position="1.066, 2.055, 0.047" rotation_euler="0.000, 0.000, -0.960" scale="0.346, 0.346, 0.346" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_well" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_stall" old_prefab_name="">
              <transform position="1.752, 2.054, 0.079" rotation_euler="0.000, 0.000, -0.791" scale="1.915, 1.915, 1.915" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_stall" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_stall" old_prefab_name="">
              <transform position="1.528, 1.806, 0.079" rotation_euler="0.000, 0.000, -1.750" scale="1.915, 1.915, 1.915" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_stall" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <transform position="1.074, 2.793, 0.078" rotation_euler="-0.034, 0.130, 2.327" scale="0.702, 0.702, 0.702" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a">
                  <mesh name="mi_market_tent_a.0" factor="4294916935" />
                  <mesh name="mi_market_tent_a.1" factor="4294916935" />
                </meta_mesh_component>
              </components>
              <additional_features>
                <feature name="apply_factor_color_to_all_components" value="true">
                  <factor value="1.000, 0.231, 0.280, 1.000" />
                </feature>
              </additional_features>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <transform position="0.385, 2.279, 0.079" rotation_euler="0.000, 0.145, 1.901" scale="0.702, 0.702, 0.702" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a">
                  <mesh name="mi_market_tent_a.0" factor="4287932504" />
                  <mesh name="mi_market_tent_a.1" factor="4287932504" />
                </meta_mesh_component>
              </components>
              <additional_features>
                <feature name="apply_factor_color_to_all_components" value="true">
                  <factor value="0.580, 0.659, 0.344, 1.000" />
                </feature>
              </additional_features>
            </game_entity>
            <game_entity name="map_icons_production_spice_c" old_prefab_name="">
              <transform position="0.441, 2.134, 0.080" rotation_euler="0.000, 0.000, 0.276" scale="0.875, 0.875, 0.875" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_spice_pile" />
              </components>
            </game_entity>
            <game_entity name="map_icons_props_salt" old_prefab_name="">
              <transform position="1.241, 1.660, 0.078" rotation_euler="0.000, 0.000, 0.779" scale="0.511, 0.511, 0.511" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_tuz_prop">
                  <mesh name="mi_tuz_prop.0" argument="40.000, 0.100, 0.000, 0.000" argument2="4.520, 2.000, 0.100, 0.500" />
                </meta_mesh_component>
              </components>
            </game_entity>
            <game_entity name="map_icons_props_cart_a" old_prefab_name="">
              <transform position="0.758, 1.286, 0.078" rotation_euler="0.000, 0.000, -0.534" scale="0.830, 0.830, 0.830" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_cart_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_props_cart_b_full" old_prefab_name="">
              <transform position="0.656, 1.527, 0.078" rotation_euler="0.000, 0.000, -1.120" scale="0.785, 0.785, 0.785" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_cart_b_full" />
              </components>
            </game_entity>
            <game_entity name="map_icons_sack_a" old_prefab_name="">
              <transform position="0.869, 1.433, 0.078" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_sack_a" />
              </components>
            </game_entity>
            <game_entity name="map_icons_sack_c" old_prefab_name="">
              <transform position="0.845, 1.180, 0.020" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_sack_c" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <transform position="-1.102, -0.815, 0.020" rotation_euler="-0.031, -0.066, 1.241" scale="0.689, 0.689, 0.689" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a">
                  <mesh name="mi_market_tent_a.0" factor="4282721803" />
                  <mesh name="mi_market_tent_a.1" factor="4282721803" />
                </meta_mesh_component>
              </components>
              <additional_features>
                <feature name="apply_factor_color_to_all_components" value="true">
                  <factor value="0.272, 0.151, 0.043, 1.000" />
                </feature>
              </additional_features>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.492, 0.404, -0.133" rotation_euler="0.000, 0.000, -3.141" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_arc" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.489, 0.415, -0.081" rotation_euler="0.000, 0.000, -1.591" scale="1.733, 1.626, 2.125" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_arc" />
              </components>
            </game_entity>
            <game_entity name="empire_road_plaza_a" old_prefab_name="">
              <transform position="1.069, 2.053, 0.064" rotation_euler="0.000, 0.000, 0.611" scale="0.074, 0.074, 0.074" />
              <physics shape="bo_empire_road_plaza_a" override_material="stone" mass="1.000" />
              <components>
                <meta_mesh_component name="empire_road_plaza_a" />
              </components>
            </game_entity>
            <game_entity name="map_icon_docks_d" old_prefab_name="">
              <transform position="0.244, 3.948, -0.211" rotation_euler="0.000, 0.000, -2.531" scale="1.572, 1.642, 1.642" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_docks_d" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="2.436, 1.817, 0.216" rotation_euler="0.000, 0.000, -0.960" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
              <transform position="4.041, 1.844, -0.164" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1s" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.958, 1.469, 0.126" rotation_euler="0.000, 0.000, 2.182" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="map_icons_empire_arena" old_prefab_name="">
              <transform position="3.222, 2.320, 0.183" rotation_euler="0.000, 0.000, -0.972" scale="0.541, 0.541, 0.541" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_arena" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.424, 1.130, 0.028" rotation_euler="0.000, 0.000, -0.960" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="1.048, 0.868, -0.012" rotation_euler="0.000, 0.000, -0.960" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.479, -1.664, -0.102" rotation_euler="0.000, 0.000, 0.000" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.434, 0.051, -0.136" rotation_euler="0.000, 0.000, 1.484" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.434, -1.285, -0.082" rotation_euler="0.000, 0.000, 0.000" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.448, -0.749, -0.146" rotation_euler="0.000, 0.000, 0.000" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.474, -0.255, -0.146" rotation_euler="0.000, 0.000, -3.141" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.151, 0.331, -0.170" rotation_euler="0.000, 0.000, 0.175" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.261, 0.664, -0.129" rotation_euler="0.000, 0.000, -0.873" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.661, 0.677, -0.074" rotation_euler="0.000, 0.000, -2.094" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.779, 0.168, -0.083" rotation_euler="0.000, 0.000, 2.443" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_b" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.812, 0.564, -0.059" rotation_euler="0.000, 0.000, -2.531" scale="0.320, 0.320, 0.320" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_b" argument="0.530, 0.530, 0.000, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.074, 0.994, -0.111" rotation_euler="0.000, 0.000, -2.531" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.266, 1.479, -0.155" rotation_euler="0.000, 0.000, 0.611" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.626, 1.994, -0.154" rotation_euler="0.000, 0.000, 0.611" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.934, 2.484, -0.172" rotation_euler="0.000, 0.000, 0.611" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.222, 2.964, -0.187" rotation_euler="0.000, 0.000, -2.531" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.463, 3.332, -0.195" rotation_euler="0.000, 0.000, -2.531" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.341, 3.681, -0.179" rotation_euler="0.000, 0.000, -0.960" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="decal_path_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-1.122, 3.811, -0.162" rotation_euler="0.000, 0.000, -0.960" scale="0.340, 0.340, 0.340" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_empire_path_a" argument="0.530, 0.530, 0.480, 0.010" />
              </components>
            </game_entity>
            <game_entity name="map_icon_docks_d" old_prefab_name="">
              <transform position="-0.111, 4.455, -0.211" rotation_euler="0.000, 0.000, -2.531" scale="1.572, 1.642, 1.642" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_docks_d" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="-0.867, 3.864, -0.119" rotation_euler="0.000, 0.000, -0.960" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="-1.090, -0.327, -0.119" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="0.803, -0.327, -0.119" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="1.675, -0.327, -0.119" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="-1.120, 1.496, -0.119" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="0.720, 1.496, -0.119" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="2.585, 1.368, -0.119" rotation_euler="0.000, 0.000, -0.524" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="2.122, 3.184, -0.119" rotation_euler="0.000, 0.000, -1.047" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="0.421, 2.598, -0.119" rotation_euler="0.000, 0.000, -1.047" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
            <game_entity name="decal_ground" old_prefab_name="">
              <transform position="-1.114, 2.825, -0.119" rotation_euler="0.000, 0.000, -1.571" />
              <physics mass="1.000" />
              <components>
                <decal_component material="decal_city_ground" argument="1.000, 1.000, 0.000, 0.000" />
              </components>
            </game_entity>
          </children>
        </game_entity>
        <game_entity name="town_M1_l1" old_prefab_name="">
          <physics mass="1.000" />
          <children>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-2.251, -0.586, -0.161" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-2.251, 0.581, -0.161" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-1.538, -1.303, -0.161" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="2.430, -1.311, -0.161" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l1" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="1.479, -1.324, -0.090" rotation_euler="0.000, 0.000, 1.550" scale="1.025, 0.980, 1.105" />
              <physics mass="1.000" />
              <children>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_breach_l1" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="0.010, 0.012, -0.209" rotation_euler="0.000, 0.000, 0.000" scale="0.910, 0.899, 0.910" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_1s_collapsed" />
                  </components>
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="0.028, 0.018, -0.085" rotation_euler="0.000, 0.000, -0.019" scale="0.800, 0.866, 0.800" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_1s" />
                  </components>
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
              <transform position="2.951, -1.094, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1s" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
              <transform position="2.951, -0.574, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1s" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
              <transform position="2.951, 0.029, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1s" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="3.679, 1.309, -0.161" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="3.382, 0.888, -0.161" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="3.846, 2.694, -0.161" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="3.285, 3.639, -0.161" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="1.646, 4.332, -0.161" rotation_euler="0.000, 0.000, -1.222" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-2.233, 2.023, -0.065" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-2.229, 3.461, -0.065" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-1.623, 4.612, -0.065" rotation_euler="0.000, 0.000, -0.960" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-0.428, 5.428, -0.065" rotation_euler="0.000, 0.000, -0.960" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="0.560, 5.258, -0.065" rotation_euler="0.000, 0.000, -2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="1.250, 4.246, -0.065" rotation_euler="0.000, 0.000, -2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="1.052, 3.244, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-0.078, 2.452, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l1" old_prefab_name="">
              <transform position="-1.264, 1.641, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
              <transform position="-2.042, 1.109, -0.069" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_1s" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_gatehouse_l1" old_prefab_name="">
              <transform position="0.491, -1.352, -0.029" rotation_euler="0.000, 0.000, 1.571" scale="0.880, 0.880, 0.880" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_1_gatehouse" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="1.331, -1.279, 0.025" rotation_euler="0.000, 0.000, -1.583" scale="0.071, 0.869, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="map_icons_empire_keep_l1" old_prefab_name="">
              <transform position="-0.120, 4.549, 0.140" rotation_euler="0.000, 0.000, 0.620" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_keep_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_small_gatehouse_l1" old_prefab_name="">
              <transform position="-0.737, 2.087, -0.059" rotation_euler="0.000, 0.000, -0.977" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_1_mini_gatehouse" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l1" old_prefab_name="">
              <transform position="2.810, -1.267, -0.380" rotation_euler="0.000, 0.000, 0.022" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l1" old_prefab_name="">
              <transform position="-2.004, -1.117, -0.059" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l1" old_prefab_name="">
              <transform position="-2.242, 1.091, -0.195" rotation_euler="0.000, 0.000, 0.000" scale="0.757, 0.757, 0.757" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l1" old_prefab_name="">
              <transform position="1.033, 4.354, -0.300" rotation_euler="0.000, 0.000, 0.000" scale="1.323, 1.323, 1.323" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l1" old_prefab_name="">
              <transform position="2.485, 4.153, -0.743" rotation_euler="0.000, 0.000, 0.000" scale="1.323, 1.323, 1.323" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_1" />
              </components>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l1" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="-0.503, -1.335, -0.259" rotation_euler="0.000, 0.000, 1.583" scale="0.959, 0.959, 0.959" />
              <physics mass="1.000" />
              <children>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_breach_l1" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="0.010, 0.012, 0.000" rotation_euler="0.000, 0.000, 0.000" scale="0.847, 0.837, 0.847" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_1s_collapsed" />
                  </components>
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_l1" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="0.028, 0.018, 0.118" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_1s" />
                  </components>
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="0.502, -1.323, 0.471" rotation_euler="0.000, 0.000, -1.563" scale="0.602, 0.322, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
          </children>
        </game_entity>
        <game_entity name="town_M1_l2" old_prefab_name="">
          <physics mass="1.000" />
          <children>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-2.251, -0.586, -0.161" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-2.251, 0.581, -0.161" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l2" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="-0.945, -1.275, -0.201" rotation_euler="0.000, 0.000, 1.578" scale="0.757, 0.757, 0.757" />
              <physics mass="1.000" />
              <children>
                <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="-0.041, 0.026, 0.100" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_2s" />
                  </components>
                </game_entity>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform position="-0.014, 0.036, 0.000" rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_breach_l2" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="-0.006, 0.029, 0.000" rotation_euler="0.000, 0.000, 0.000" scale="0.925, 0.963, 1.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_2s_collapsed" />
                  </components>
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="1.356, -1.307, -0.168" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="2.951, -1.094, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="2.951, -0.574, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="2.951, 0.029, -0.164" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="3.679, 1.309, -0.161" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="3.382, 0.888, -0.161" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="3.846, 2.694, -0.161" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="3.285, 3.639, -0.161" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="1.646, 4.332, -0.161" rotation_euler="0.000, 0.000, -1.222" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-2.233, 2.023, -0.065" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-2.229, 3.461, -0.065" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-1.639, 4.585, -0.065" rotation_euler="0.000, 0.000, -0.942" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-0.428, 5.428, -0.065" rotation_euler="0.000, 0.000, -0.960" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="0.543, 5.281, -0.065" rotation_euler="0.000, 0.000, -2.513" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="1.250, 4.246, -0.065" rotation_euler="0.000, 0.000, -2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="1.052, 3.244, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="0.101, 2.578, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l2" old_prefab_name="">
              <transform position="-1.564, 1.431, -0.065" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_gatehouse_l2" old_prefab_name="">
              <transform position="0.491, -1.352, -0.029" rotation_euler="0.000, 0.000, 1.571" scale="0.880, 0.880, 0.880" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_2_gatehouse" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="-0.412, -1.312, -0.164" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_keep_l2" old_prefab_name="">
              <transform position="-0.120, 4.549, 0.140" rotation_euler="0.000, 0.000, 0.620" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_keep_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_small_gatehouse_l2" old_prefab_name="">
              <transform position="-0.737, 2.087, -0.059" rotation_euler="0.000, 0.000, -0.977" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_2_mini_gatehouse" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l2" old_prefab_name="">
              <transform position="2.810, -1.267, -0.540" rotation_euler="0.000, 0.000, 0.022" scale="0.970, 0.970, 0.868" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l2" old_prefab_name="">
              <transform position="-2.004, -1.117, -0.429" rotation_euler="0.000, 0.000, 0.000" scale="0.784, 0.784, 0.784" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l2" old_prefab_name="">
              <transform position="-2.242, 1.091, -0.059" rotation_euler="0.000, 0.000, 0.000" scale="0.757, 0.757, 0.757" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l2" old_prefab_name="">
              <transform position="1.033, 4.354, -0.560" rotation_euler="0.000, 0.000, 0.000" scale="1.141, 1.141, 1.141" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l2" old_prefab_name="">
              <transform position="2.485, 4.153, -0.743" rotation_euler="0.000, 0.000, 0.000" scale="0.918, 0.918, 0.918" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="-1.530, -1.312, -0.164" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l2" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="1.993, -1.293, -0.234" rotation_euler="0.000, 0.000, 1.566" scale="0.802, 0.802, 0.802" />
              <physics mass="1.000" />
              <children>
                <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="-0.041, 0.026, 0.100" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_2s" />
                  </components>
                </game_entity>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform position="-0.014, 0.036, 0.000" rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_breach_l2" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="-0.006, 0.029, 0.000" rotation_euler="0.000, 0.000, 0.000" scale="0.925, 0.963, 1.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_2s_collapsed" />
                  </components>
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l2" old_prefab_name="">
              <transform position="1.272, -1.615, -0.930" rotation_euler="0.000, 0.000, 0.022" scale="0.970, 0.970, 0.868" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_2" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="-0.173, -1.020, 0.738" rotation_euler="0.000, 0.000, -1.571" scale="0.673, 0.369, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.459, 0.416, 0.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l2" old_prefab_name="">
              <transform position="2.552, -1.322, -0.164" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_2s" />
              </components>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="0.794, -1.020, 0.738" rotation_euler="0.000, 0.000, -1.571" scale="0.673, 0.369, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.459, 0.416, 0.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
          </children>
        </game_entity>
        <game_entity name="town_M1_l3" old_prefab_name="">
          <physics mass="1.000" />
          <children>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-2.251, -0.586, -0.492" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-2.251, 0.581, -0.492" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="-0.151, -1.048, 1.194" rotation_euler="0.000, 0.000, -1.564" scale="0.642, 0.329, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.500, 0.500, -0.500" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="1.356, -1.285, -0.299" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="2.951, -1.094, -0.504" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="2.951, -0.574, -0.524" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="2.951, 0.029, -0.524" rotation_euler="0.000, 0.000, -3.141" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="3.813, 1.499, -0.521" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="3.382, 0.888, -0.521" rotation_euler="0.000, 0.000, 2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="3.846, 2.694, -0.521" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="3.285, 3.639, -0.521" rotation_euler="0.000, 0.000, -2.618" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="1.646, 4.332, -0.161" rotation_euler="0.000, 0.000, -1.222" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-2.233, 2.023, -0.316" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-2.229, 3.461, -0.316" rotation_euler="0.000, 0.000, 0.000" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-1.639, 4.585, -0.325" rotation_euler="0.000, 0.000, -0.942" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-0.428, 5.428, -0.325" rotation_euler="0.000, 0.000, -0.960" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="0.543, 5.281, -0.325" rotation_euler="0.000, 0.000, -2.513" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="1.238, 4.206, -0.353" rotation_euler="0.000, 0.000, -2.531" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="1.052, 3.244, -0.353" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="0.101, 2.578, -0.353" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_l3" old_prefab_name="">
              <transform position="-1.564, 1.431, -0.353" rotation_euler="0.000, 0.000, 2.182" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_gatehouse_l3" old_prefab_name="">
              <transform position="0.491, -1.352, -0.069" rotation_euler="0.000, 0.000, 1.571" scale="0.880, 0.880, 0.880" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_3_gatehouse" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="-0.500, -1.312, -0.299" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_keep_l3" old_prefab_name="">
              <transform position="0.105, 4.182, 0.140" rotation_euler="0.000, 0.000, 0.620" scale="0.661, 0.661, 0.661" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_keep_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_small_gatehouse_l3" old_prefab_name="">
              <transform position="-0.737, 2.087, -0.059" rotation_euler="0.000, 0.000, -0.977" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_3_mini_gatehouse" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l3" old_prefab_name="">
              <transform position="2.810, -1.267, -0.200" rotation_euler="0.000, 0.000, 0.022" scale="0.970, 0.970, 0.868" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l3" old_prefab_name="">
              <transform position="-2.004, -1.117, -0.429" rotation_euler="0.000, 0.000, 0.000" scale="0.784, 0.784, 0.784" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_square_tower_l3" old_prefab_name="">
              <transform position="-2.242, 1.091, -0.059" rotation_euler="0.000, 0.000, 0.000" scale="0.757, 0.757, 0.757" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_square_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l3" old_prefab_name="">
              <transform position="1.033, 4.354, -0.680" rotation_euler="0.000, 0.000, 0.000" scale="0.969, 0.969, 0.969" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l3" old_prefab_name="">
              <transform position="2.485, 4.153, -0.783" rotation_euler="0.000, 0.000, 0.000" scale="0.918, 0.918, 0.918" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l3" old_prefab_name="">
              <transform position="-0.348, -1.749, -1.109" rotation_euler="0.000, 0.000, 0.000" scale="0.784, 0.784, 0.784" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l3" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="2.014, -1.300, -0.239" rotation_euler="0.000, 0.000, 1.563" scale="0.757, 0.900, 0.757" />
              <physics mass="1.000" />
              <children>
                <game_entity name="map_icons_empire_wall_small_breach_l3" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="0.057, 0.010, 0.000" rotation_euler="0.000, 0.000, 0.000" scale="1.000, 0.960, 1.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_3s_collapsed" />
                  </components>
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="0.035, 0.010, -0.017" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_3s" />
                  </components>
                </game_entity>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="-1.590, -1.312, -0.299" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_tower_l3" old_prefab_name="">
              <transform position="1.242, -1.749, -1.109" rotation_euler="0.000, 0.000, 0.000" scale="0.784, 0.784, 0.784" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_tower_3" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_breach_l3" old_prefab_name="">
              <tags>
                <tag name="map_breachable_wall" />
              </tags>
              <transform position="-1.048, -1.337, -0.196" rotation_euler="0.000, 0.000, 1.601" scale="0.757, 0.852, 0.757" />
              <physics mass="1.000" />
              <children>
                <game_entity name="map_icons_empire_wall_small_breach_l3" old_prefab_name="">
                  <tags>
                    <tag name="map_broken_wall" />
                  </tags>
                  <transform position="0.057, 0.010, 0.000" rotation_euler="0.000, 0.000, 0.000" scale="1.000, 0.960, 1.000" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_3s_collapsed" />
                  </components>
                </game_entity>
                <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
                  <tags>
                    <tag name="map_solid_wall" />
                  </tags>
                  <transform position="0.035, 0.010, -0.017" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="mi_emp_wall_3s" />
                  </components>
                </game_entity>
                <game_entity name="bo_siege_wall" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, 1.590" scale="0.368, 0.368, 0.367" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_empire_wall_small_l3" old_prefab_name="">
              <transform position="2.651, -1.285, -0.299" rotation_euler="0.000, 0.000, 1.571" scale="0.800, 0.800, 0.800" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_emp_wall_3s" />
              </components>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
                <level name="looted" />
              </levels>
            </game_entity>
            <game_entity name="banner_pos" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_banner_placeholder" />
              </tags>
              <transform position="0.824, -1.041, 1.194" rotation_euler="0.000, 0.000, -1.564" scale="0.642, 0.329, 0.049" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.500, 0.500, -0.500" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                </game_entity>
              </children>
            </game_entity>
          </children>
        </game_entity>
        <game_entity name="town_gate" old_prefab_name="">
          <flags>
            <flag name="align_to_terrain" value="true" />
          </flags>
          <tags>
            <tag name="main_map_city_gate" />
          </tags>
          <transform position="0.456, -2.213, -0.028" rotation_euler="0.000, 0.000, -1.010" />
          <physics mass="1.000" />
        </game_entity>
        <game_entity name="town_circle_decal" old_prefab_name="">
          <tags>
            <tag name="map_settlement_circle" />
          </tags>
          <transform position="0.468, 1.463, -0.029" rotation_euler="0.000, 0.000, 3.132" scale="6.181, 6.181, 1.343" />
          <physics mass="1.000" />
          <components>
            <decal_component material="decal_city_circle_a" argument="1.000, 1.000, 0.000, 0.000" />
          </components>
        </game_entity>
        <game_entity name="bo_town" old_prefab_name="">
          <tags>
            <tag name="bo_town" />
          </tags>
          <transform position="0.532, 1.526, -0.647" rotation_euler="0.000, 0.000, -1.980" scale="3.694, 3.694, 3.694" />
          <physics shape="bo_sphere_collider" mass="1.000">
            <body_flags>
              <body_flag name="only_collide_with_raycast" />
            </body_flags>
          </physics>
        </game_entity>
        <game_entity name="map_icon_siege" old_prefab_name="">
          <transform position="0.029, -4.803, -0.136" rotation_euler="0.000, 0.000, 1.797" />
          <physics mass="1.000" />
          <children>
            <game_entity name="map_icon_siege_camp_1" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="2.010, 2.012, -0.217" rotation_euler="0.230, -0.165, -2.387" scale="1.188, 1.188, 1.188" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_1" />
              </components>
            </game_entity>
            <game_entity name="map_barrel_c" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <transform position="-0.611, 1.168, 0.184" rotation_euler="-0.131, 0.154, 1.780" scale="1.618, 1.618, 1.618" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_barrels_c" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_5" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="0.663, -4.919, 0.061" rotation_euler="0.070, -0.126, -0.385" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_5" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_1" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="0.996, -4.063, 0.141" rotation_euler="0.196, 0.205, -2.266" scale="1.188, 1.188, 1.188" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_1" />
              </components>
            </game_entity>
            <game_entity name="defender_ranged_l1" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_3" />
              </tags>
              <transform position="3.273, -1.691, 0.463" rotation_euler="0.000, 0.000, 1.346" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="volume_box_corner" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_camp_area_2" />
              </tags>
              <transform position="1.432, 3.195, -0.366" rotation_euler="0.000, 0.000, 2.741" scale="2.000, 2.000, 1.000" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.500, 0.500, -0.500" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                  <levels>
                    <level name="base" />
                  </levels>
                </game_entity>
              </children>
              <levels>
                <level name="base" />
              </levels>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="0.002, 0.770, 0.258" rotation_euler="0.031, -0.312, -1.036" scale="0.779, 0.779, 0.779" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_5" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="0.621, -3.044, 0.108" rotation_euler="0.000, 0.000, -0.452" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_5" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_1" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="1.044, -2.301, 0.248" rotation_euler="-0.122, 0.255, 2.828" scale="1.188, 1.188, 1.188" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_1" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_4" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="1.237, -1.474, 0.281" rotation_euler="-0.018, 0.143, 0.000" scale="1.014, 1.014, 1.014" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_4" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_1" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="1.503, -1.032, 0.235" rotation_euler="-0.048, -0.136, -2.684" scale="1.188, 1.188, 1.188" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_1" />
              </components>
            </game_entity>
            <game_entity name="map_icon_siege_camp_4" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="1.331, -0.023, 0.225" rotation_euler="-0.187, 0.077, -0.269" scale="1.014, 1.014, 1.014" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_4" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.103, 0.000, 0.225" rotation_euler="0.000, 0.000, 1.287" scale="0.779, 0.779, 0.779" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a" />
              </components>
            </game_entity>
            <game_entity name="volume_box_corner" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_camp_area_2" />
              </tags>
              <transform position="-0.648, -3.567, 0.048" rotation_euler="0.026, 0.142, -2.933" scale="1.446, 3.069, 1.631" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.500, 0.500, -0.500" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                  <levels>
                    <level name="base" />
                  </levels>
                </game_entity>
              </children>
              <levels>
                <level name="base" />
              </levels>
            </game_entity>
            <game_entity name="volume_box_corner" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_camp_area_1" />
              </tags>
              <transform position="-1.103, 0.145, 0.078" rotation_euler="0.087, 0.182, 2.965" scale="1.518, 2.499, 1.631" />
              <physics mass="1.000" />
              <children>
                <game_entity name="volume_box" old_prefab_name="">
                  <flags>
                    <flag name="record_to_scene_replay" value="true" />
                  </flags>
                  <visibility_masks>
                    <visibility_mask name="visible_only_when_editing" value="true" />
                  </visibility_masks>
                  <transform position="0.500, 0.500, -0.500" />
                  <physics mass="1.000" />
                  <components>
                    <meta_mesh_component name="volume_box" />
                  </components>
                  <scripts>
                    <script name="VolumeBox">
                      <variables>
                        <variable name="NavMeshPrefabName" value="" />
                      </variables>
                    </script>
                  </scripts>
                  <levels>
                    <level name="base" />
                  </levels>
                </game_entity>
              </children>
              <levels>
                <level name="base" />
              </levels>
            </game_entity>
            <game_entity name="map_icon_siege_camp_4" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
                <flag name="align_rotation_to_terrain" value="true" />
              </flags>
              <tags>
                <tag name="siege_preparation" />
              </tags>
              <transform position="1.664, 1.089, 0.033" rotation_euler="-0.200, 0.027, -0.526" scale="0.973, 0.973, 0.973" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="map_icon_siege_camp_4" />
              </components>
            </game_entity>
            <game_entity name="map_icons_market_tent_a" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <transform position="-0.805, -2.741, 0.066" rotation_euler="-0.212, 0.233, 0.549" scale="0.779, 0.779, 0.779" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="mi_market_tent_a" />
              </components>
            </game_entity>
            <game_entity name="attacker_ranged_engine" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_engine_3" />
              </tags>
              <transform position="0.751, 0.934, 0.138" rotation_euler="0.000, 0.000, -1.970" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.048, 0.021, -0.013" rotation_euler="0.000, 0.000, 2.438" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_ranged_engine" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_engine_2" />
              </tags>
              <transform position="0.530, 0.167, 0.258" rotation_euler="0.000, 0.000, -1.769" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.006, -0.038, -0.017" rotation_euler="0.000, 0.000, -0.236" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_ranged_engine" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_engine_1" />
              </tags>
              <transform position="0.452, -1.481, 0.242" rotation_euler="0.000, 0.000, -1.686" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.051, -0.014, 0.009" rotation_euler="0.000, 0.000, 2.889" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_ranged_engine" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_engine_0" />
              </tags>
              <transform position="0.072, -3.066, 0.048" rotation_euler="0.000, 0.000, -1.542" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.024, -0.093, -0.028" rotation_euler="0.000, 0.000, 2.879" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_siege_ram" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_ram" />
              </tags>
              <transform position="-2.913, 1.548, 0.570" rotation_euler="0.000, 0.000, -1.703" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.079, 0.058, -0.020" rotation_euler="0.000, 0.000, 1.922" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_siege_tower" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_tower" />
              </tags>
              <transform position="-3.492, 0.328, 0.569" rotation_euler="0.000, 0.000, -1.831" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="-0.023, 0.025, -0.024" rotation_euler="0.000, 0.000, -0.555" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="attacker_siege_tower" old_prefab_name="">
              <flags>
                <flag name="align_to_terrain" value="true" />
              </flags>
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_siege_tower" />
              </tags>
              <transform position="-3.459, -1.402, 0.648" rotation_euler="0.000, 0.000, -1.711" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform position="0.024, 0.028, -0.022" rotation_euler="0.000, 0.000, -2.195" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
            </game_entity>
            <game_entity name="defender_ranged_l2" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_0" />
              </tags>
              <transform position="2.845, -3.512, 0.768" rotation_euler="0.000, 0.000, 1.392" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l2" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_1" />
              </tags>
              <transform position="4.076, 1.205, 0.837" rotation_euler="0.000, 0.000, 1.581" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.102" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l2" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_2" />
              </tags>
              <transform position="3.409, -0.779, 1.028" rotation_euler="0.000, 0.000, 1.361" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.088" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l2" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_3" />
              </tags>
              <transform position="3.127, -1.684, 1.001" rotation_euler="0.005, 0.003, 1.354" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_2" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l3" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_3" />
              </tags>
              <transform position="3.221, -1.713, 1.352" rotation_euler="0.004, 0.003, 1.356" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l3" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_2" />
              </tags>
              <transform position="3.476, -0.819, 1.364" rotation_euler="0.004, 0.003, 1.354" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.088" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l3" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_1" />
              </tags>
              <transform position="4.051, 1.146, 1.345" rotation_euler="0.005, 0.002, 1.526" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.102" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l3" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_0" />
              </tags>
              <transform position="2.832, -3.516, 1.106" rotation_euler="0.004, 0.003, 1.302" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_3" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l1" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_0" />
              </tags>
              <transform position="2.864, -3.495, 0.550" rotation_euler="0.000, 0.000, 1.337" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.076" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l1" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_1" />
              </tags>
              <transform position="4.050, 1.139, 0.822" rotation_euler="0.000, 0.000, 1.716" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.102" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
            <game_entity name="defender_ranged_l1" old_prefab_name="">
              <visibility_masks>
                <visibility_mask name="visible_only_when_editing" value="true" />
              </visibility_masks>
              <tags>
                <tag name="map_defensive_engine_2" />
              </tags>
              <transform position="3.454, -0.784, 0.468" rotation_euler="0.000, 0.000, 1.349" scale="0.350, 0.350, 0.350" />
              <physics mass="1.000" />
              <components>
                <meta_mesh_component name="arrow_new_icon" />
              </components>
              <children>
                <game_entity name="bo_siege_engine" old_prefab_name="">
                  <transform rotation_euler="0.000, 0.000, -3.088" scale="1.050, 1.050, 1.050" />
                  <physics shape="bo_sphere_collider" mass="1.000" />
                </game_entity>
              </children>
              <levels>
                <level name="level_1" />
                <level name="civilian" />
                <level name="siege" />
              </levels>
            </game_entity>
          </children>
          <levels>
            <level name="level_1" />
            <level name="level_2" />
            <level name="level_3" />
            <level name="siege" />
          </levels>
        </game_entity>
      </children>
    </game_entity>
</prefabs>
```
