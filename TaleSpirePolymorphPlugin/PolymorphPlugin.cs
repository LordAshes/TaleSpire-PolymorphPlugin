using UnityEngine;
using BepInEx;
using Bounce.Unmanaged;
using System.Collections.Generic;
using System.Linq;
using System;
using BepInEx.Configuration;

namespace LordAshes
{
    [BepInPlugin(Guid, "Polymorph Plug-In", Version)]
    [BepInDependency(RadialUI.RadialUIPlugin.Guid)]
    [BepInDependency(StatMessaging.Guid)]
    [BepInDependency(CustomMiniPlugin.Guid)]
    public class PolymorphPlugin : BaseUnityPlugin
    {
        // Plugin info
        private const string Guid = "org.lordashes.plugins.polymorph";
        private const string Version = "1.0.0.0";

        // Content directory
        private string dir = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData/";

        // Hold names of transfromations
        private ConfigEntry<string>[] transfromations = new ConfigEntry<string>[10];

        // Holds the creature of the radial menu
        private CreatureGuid radialCreature = CreatureGuid.Empty;

        /// <summary>
        /// Function for initializing plugin
        /// This function is called once by TaleSpire
        /// </summary>
        void Awake()
        {
            UnityEngine.Debug.Log("Lord Ashes Polymorph Plugin Active.");

            StateDetection.Initialize(this.GetType());

            // Add transformation main menu
            RadialUI.RadialSubmenu.EnsureMainMenuItem(RadialUI.RadialUIPlugin.Guid + ".Transformation",
                                                        RadialUI.RadialSubmenu.MenuType.character,
                                                        "Transformation",
                                                        RadialUI.RadialSubmenu.GetIconFromFile(dir + "Images/Icons/Transformation.png")
                                                      );

            RadialUI.RadialSubmenu.CreateSubMenuItem(   RadialUI.RadialUIPlugin.Guid + ".Transformation",
                                                        "Polymorph", 
                                                        RadialUI.RadialSubmenu.GetIconFromFile(dir + "Images/Icons/Polymorph.png"),
                                                        SelectPolymorph,
                                                        false
                                                    );

            // Read polymorph transformations from configuration file
            for(int i=0; i<transfromations.Length; i++)
            {
                transfromations[i] = Config.Bind("Settings", "Polymorph Shape " + i + 1, "");
            }
        }

        /// <summary>
        /// Method to create polymorph sub-menu
        /// </summary>
        /// <param name="cid">Radiam menu creature</param>
        /// <param name="menu">Menu</param>
        /// <param name="mmi">MapMenuItem</param>
        private void SelectPolymorph(CreatureGuid cid, string menu, MapMenuItem mmi)
        {
            Debug.Log("Building Sub-Menu");
            // Create sub-menu
            MapMenu mapMenu = MapMenuManager.OpenMenu(mmi, MapMenu.MenuType.SUBROOT);
            // Populate sub-menu based on all items added by any plugins for the specific main menu entry
            foreach (ConfigEntry<string> iconName in transfromations)
            {
                if (iconName.Value != null && iconName.Value != "")
                {
                    Debug.Log("Building Adding '" + iconName.Value + "'...");
                    mapMenu.AddItem(new MapMenu.ItemArgs()
                    {
                        Action = (_mmi, _obj) => { ApplyPolymorph(cid, iconName.Value); },
                        Icon = (System.IO.File.Exists(dir + "Images/Icons/" + PolymorphPlugin.Guid + "/" + iconName.Value + ".PNG")) ? RadialUI.RadialSubmenu.GetIconFromFile(dir + "Images/Icons/" + PolymorphPlugin.Guid + "/" + iconName.Value + ".PNG") : RadialUI.RadialSubmenu.GetIconFromFile(dir + "Images/Icons/Polymorph.png"),
                        Title = iconName.Value,
                        CloseMenuOnActivate = true
                    });
                }
            }
            Debug.Log("Sub-Menu Build Complete");
        }

        /// <summary>
        /// Method to apply transformation using CMP
        /// </summary>
        /// <param name="cid">Radial menu creature</param>
        /// <param name="transformationName">String version of transformation name</param>
        private void ApplyPolymorph(CreatureGuid cid, string transformationName)
        {
            StatMessaging.SetInfo(cid, CustomMiniPlugin.Guid, transformationName);
        }

        /// <summary>
        /// Function for determining if view mode has been toggled and, if so, activating or deactivating Character View mode.
        /// This function is called periodically by TaleSpire.
        /// </summary>
        void Update()
        {
        }
    }
}
