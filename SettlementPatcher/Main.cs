using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;


namespace SettlementPatcher
{
    public class Patcher
    {
        private bool firstOutput = false;
        private String firstOutputTimeString = "";
        private String modDirectory;
        private String sandboxDirectory;
        private String modName;

        private String basePath = "../../";

        public Patcher(String passedName)
        {
            modName = passedName;
            OutputDebugCode("Constructing object");
            modDirectory = (basePath + "Modules/" + modName + "/");
            sandboxDirectory = (basePath + "Modules/Sandbox/");
        }

        public bool patch()
        {
            // Splits by namespace separator (dot) and gets first returned array, should be primary namespace = modname

            // TODO: ADD DEPENDABILITY ON GAME TYPE
            // TODO: Add check whether every settlement has a prefab?
            // TODO: Save list of all native settlements, delete any settlements that are not native
            //       this is necessary to avoid situations where a player disables a mod that is supposed to add new settlements but the settlements are still there...
            // TODO: Make code update existing settlement entry with new values (delete and reinsert)

            /*
            if (!backup file exists)
	            create backup file
            open submodule.xml
            open settlements.xml
            open scene.xscene
            get submodule.xml settlements nodes
            get submodule.xml prefabs nodes

            for each node in settlements_nodes
	            if (!node.id exists in settlementls.xml)
		            add node to settlements.xml

            for each node in prefabs_nodes
	            if(!node.id exists in scene.xscene
		            add node to scene.sxcene
        */
            OutputDebugCode("----------------------------------------------");
            OutputDebugCode("---------- XmlPatch executed on mod ----------");
            OutputDebugCode("----------------------------------------------");
            OutputDebugCode("Identified modDirectory as " + modDirectory);
            OutputDebugCode("---------------");
            // Deletes the bak file and recreates it, making it more recent than settlements.xml
            refreshBackupDate("settlements.xml");
            OutputDebugCode("---------------");
            refreshBackupDate("scene.xscene");
            OutputDebugCode("---------------");
            OutputDebugCode("Loading submodule.xml, settlements.xml and scene.xscene");
            XmlDocument xmlModSubmodule = new XmlDocument();
            xmlModSubmodule.Load(modDirectory + "submodule.xml");
            XmlDocument xmlSandboxSettlements = new XmlDocument();
            xmlSandboxSettlements.Load(sandboxDirectory + "ModuleData/settlements.xml");
            XmlDocument xmlSandboxScene = new XmlDocument();
            xmlSandboxScene.Load(sandboxDirectory + "SceneObj/Main_map/scene.xscene");
            OutputDebugCode("---------------");


            // It is possible the first element is a Xml version/encoding element. Find Module element instead.
            int nodeIndex = 0;
            XmlNode node = ReturnDesiredNode("Module", xmlModSubmodule.ChildNodes.Cast<XmlNode>().ToArray(), out nodeIndex);

            int subModuleXmlsIndex = 0;
            for (int i = 0; i <= 999; i++)
            {
                if (node.ChildNodes[i].Name == "Xmls")
                {
                    subModuleXmlsIndex = i;
                    break;
                }
            }

            List<String> settlementPaths = new List<string>();
            List<String> prefabPaths = new List<string>();

            OutputDebugCode("Reading submodule.xml and finding children");
            // Get all settlements or prefabs files references in submodule.xml


            foreach (XmlNode xmlNode in node.ChildNodes[subModuleXmlsIndex].ChildNodes)
            {
                if (xmlNode.ChildNodes[0].Name == "XmlName")
                {
                    if (xmlNode.ChildNodes[0].Attributes["id"].Value == "Settlements")
                    {
                        settlementPaths.Add("ModuleData/" + xmlNode.ChildNodes[0].Attributes["path"].Value + ".xml");
                        OutputDebugCode("Found & loaded " + "ModuleData/" + xmlNode.ChildNodes[0].Attributes["path"].Value + ".xml");
                    }
                    else if (xmlNode.ChildNodes[0].Attributes["id"].Value == "prefabs")
                    {
                        prefabPaths.Add("Prefabs/" + xmlNode.ChildNodes[0].Attributes["path"].Value + ".xml");
                        OutputDebugCode("Found & loaded " + "Prefabs/" + xmlNode.ChildNodes[0].Attributes["path"].Value + ".xml");
                    }
                }
                else
                {
                    OutputDebugCode("Unpredicted error. No XmlName at top-level below XmlNode");
                }
            }

            OutputDebugCode("Found all files. Proceeding to inject into native files.");
            OutputDebugCode("---------------");

            XmlNode[] toBeAddedSettlements = { };

            checkAndAdd("Settlements", "Settlement", settlementPaths, xmlSandboxSettlements);
            OutputDebugCode("---------------");
            checkAndAdd("prefabs", "game_entity", prefabPaths, xmlSandboxScene);
            OutputDebugCode("---------------");
            OutputDebugCode("Attempting to save files...");

            OutputDebugCode("Saving " + sandboxDirectory + "ModuleData/settlements.xml...");
            xmlSandboxSettlements.Save(sandboxDirectory + "ModuleData/settlements.xml");
            OutputDebugCode("...Saved ");
            OutputDebugCode("---------------");
            OutputDebugCode("Saving " + sandboxDirectory + "SceneObj/Main_map/scene.xscene...");
            xmlSandboxScene.Save(sandboxDirectory + "SceneObj/Main_map/scene.xscene");
            OutputDebugCode("...Saved ");

            OutputDebugCode("...files saved!");
            OutputDebugCode("----------------------------------------------");

            // Deletes the bak file and recreates it, making it more recent than settlements.xml
            refreshBackupDate("settlements.xml");
            OutputDebugCode("---------------");
            refreshBackupDate("scene.xscene");



            return true;
        }

        private void checkAndAdd(String type, String elementName, List<String> pathList, XmlDocument document)
        {
            OutputDebugCode("----------------------------------------------");

            // Iterate over every identified settlements file
            foreach (String pathName in pathList)
            {
                XmlDocument reader = new XmlDocument();

                OutputDebugCode("[" + pathName + "] Loading file from " + modDirectory + pathName);

                reader.Load(modDirectory + pathName);

                int topLevelIndex = 0;

                XmlNode desiredElement = ReturnDesiredNode(type, reader.ChildNodes.Cast<XmlNode>().ToArray(), out topLevelIndex);

                if (desiredElement != null)
                {
                    OutputDebugCode("[" + pathName + "] found top level element of type " + desiredElement.Name);
                    // Verify file is properly formatted
                    if (desiredElement.Name == type)
                    {
                        foreach (XmlNode xmlNode in desiredElement)
                        {
                            OutputDebugCode("[" + pathName + "] Second level element found. Element type is " + xmlNode.Name);
                            // If correctly formatted this should always return true
                            if (xmlNode.Name == elementName)
                            {
                                int temp = 0;

                                // Loop over all elements that have the name property and see whether any of them
                                // have the same name as the one we are trying to add. 
                                foreach (XmlNode child in desiredElement.ChildNodes[topLevelIndex].SelectNodes("name"))
                                {
                                    if (child.Name == xmlNode.Name)
                                    {
                                        temp++;
                                    }
                                }

                                if (temp == 0)
                                {
                                    if (type == "prefabs")
                                    {
                                        ReturnDesiredNode("entities",
                                            document.GetElementsByTagName("scene")[0].ChildNodes.Cast<XmlNode>().ToArray(),
                                            out temp)
                                            .AppendChild(document.ImportNode(xmlNode, true));
                                    }
                                    else
                                    {
                                        document.GetElementsByTagName(type)[0].AppendChild(document.ImportNode(xmlNode, true));
                                    }
                                    OutputDebugCode("[" + pathName + "] Did not find any duplicates for element with name " + xmlNode.Attributes["name"].Value);
                                    OutputDebugCode("[" + pathName + "] Adding element to " + document.Name);

                                }
                                else
                                {
                                    OutputDebugCode("Node was not added. Found " + temp + " occurences of element with name " + xmlNode.Attributes["name"].Value);
                                    OutputDebugCode("Are you trying to overwrite an existing cities values?");
                                }
                            }
                        }
                    }
                }
                else
                {
                    OutputDebugCode("[" + pathName + "] Looked for top-level element " + type + " but did not find any in file " + pathName);
                }
            }
        }

        private XmlNode ReturnDesiredNode(String topLevelName, XmlNode[] nodes, out int index)
        {
            OutputDebugCode("---------------");
            OutputDebugCode("Searching for top level element " + topLevelName);
            int tempNum = 0;

            foreach (XmlNode element in nodes)
            {
                OutputDebugCode("Current element " + element.Name);

                if (element.Name == topLevelName)
                {
                    OutputDebugCode("It's a match! Returning...");
                    index = tempNum;
                    return element;
                }
                tempNum++;
            }

            index = 0;
            return (XmlNode)null;
        }

        public void onSubModuleLoad()
        {
            OutputDebugCode("----------------------------------------------");
            OutputDebugCode("SubModule implementing XmlPatcher has loaded, checking for patcher backup files");
            OutputDebugCode("---------------");
            internalModuleLoad("settlements.xml");
            OutputDebugCode("---------------");
            internalModuleLoad("scene.xscene");
            OutputDebugCode("---------------");
            refreshBackupDate("settlements.xml");
            refreshBackupDate("scene.xscene");
            OutputDebugCode("----------------------------------------------");
        }
        public void ResetFiles()
        {
            OutputDebugCode("----------------------------------------------");
            OutputDebugCode("File reset triggered. Most likely due to game ending.");
            if (!File.Exists(sandboxDirectory + "ModuleData/settlements.xml" + ".bak"))
            {
                OutputDebugCode("!!!Backup file was seemingly already deleted by another mod using this patcher.");
            }
            else
            {
                OutputDebugCode("Cleaning up directory by restoring " + sandboxDirectory + "ModuleData/settlements.xml and deleting backup.");
                File.Delete(sandboxDirectory + "ModuleData/settlements.xml");
                File.Move(sandboxDirectory + "ModuleData/settlements.xml" + ".bak", sandboxDirectory + "ModuleData/settlements.xml");
            }

            if (!File.Exists(sandboxDirectory + "SceneObj/Main_map/scene.xscene" + ".bak"))
            {
                OutputDebugCode("!!!Backup file was seemingly already deleted by another  mod using this patcher.");
            }
            else
            {
                OutputDebugCode("Cleaning up directory by restoring " + sandboxDirectory + "SceneObj/Main_map/scene.xscene and deleting backup.");
                File.Delete(sandboxDirectory + "SceneObj/Main_map/scene.xscene");
                File.Move(sandboxDirectory + "SceneObj/Main_map/scene.xscene" + ".bak", sandboxDirectory + "SceneObj/Main_map/scene.xscene");
            }
        }

        private void internalModuleLoad(String filename)
        {

            String filepath = "";

            switch (filename)
            {
                case "settlements.xml":
                    filepath = sandboxDirectory + "ModuleData/settlements.xml";
                    break;
                case "scene.xscene":
                    filepath = sandboxDirectory + "SceneObj/Main_map/scene.xscene";
                    break;
                default:
                    break;
            }

            OutputDebugCode("Checking file " + filepath);

            if (File.Exists(filepath + ".bak"))
            {
                if (DateTime.Compare(
                    File.GetLastWriteTime(filepath),
                    File.GetLastWriteTime(filepath + ".bak")
                    ) > 0)
                {
                    OutputDebugCode("Game must have updated the original file in between launches and the the last time this mod was run the game crashed.");
                    OutputDebugCode("Deleting old backup file and making a fresh copy from the now patched original file.");

                    File.Delete(filepath + ".bak");
                    File.Copy(filepath, filepath + ".bak");
                }
                else
                {
                    OutputDebugCode("Either game crashed last time it was run, or more than one patcher is in use.");
                    OutputDebugCode("As a precaution, re-create the backup file from the supposedly clean main file.");

                    File.Delete(filepath);
                    File.Copy(filepath + ".bak", filepath);
                }
            }
            else
            {
                OutputDebugCode("No backup file found, creating...");
                File.Copy(filepath, filepath + ".bak");
            }
        }

        private void refreshBackupDate(String type)
        {
            String filePath = "";
            OutputDebugCode("Making xml backup file of " + type + " more recent than original file.");
            switch (type)
            {
                case "settlements.xml":
                    filePath = sandboxDirectory + "ModuleData/settlements.xml";
                    break;
                case "scene.xscene":
                    filePath = sandboxDirectory + "SceneObj/Main_map/scene.xscene";
                    break;
                default:
                    break;
            }

            // Create copy of backup file, deletes original file, renames copy to original backup name
            File.Copy(filePath + ".bak", filePath + ".bak2");
            File.Delete(filePath + ".bak");
            File.Move(filePath + ".bak2", filePath + ".bak");
            File.SetLastWriteTime(filePath + ".bak", DateTime.Now.AddMinutes(1));
        }

        public void OutputDebugCode(String input)
        {
            if (firstOutput == false)
            {
                firstOutputTimeString = DateTime.Now.ToString("ddMMyyyy.hh-mm-ss");
                firstOutput = true;
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(modDirectory + "PatcherLog" + firstOutputTimeString + ".txt", true))
            {
                file.WriteLine(input);
            }
        }
    }
}
