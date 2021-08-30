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
        private const string Version = "1.1.0.0";

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

            // Read polymorph transformations from configuration file
            for (int i = 0; i < transfromations.Length; i++)
            {
                transfromations[i] = Config.Bind("Settings", "Polymorph Shape " + (i + 1).ToString("d2"), "");
                if (transfromations[i].Value != null)
                {
                    Debug.Log("Polymorph Shape " + (i + 1).ToString("d2") + " is " + transfromations[i].Value);
                }
                else
                {
                    Debug.Log("Polymorph Shape " + (i + 1).ToString("d2") + " is not set");
                }
            }

            // Add transformation main menu
            RadialUI.RadialSubmenu.EnsureMainMenuItem(  RadialUI.RadialUIPlugin.Guid + ".Ploymorph",
                                                        RadialUI.RadialSubmenu.MenuType.character,
                                                        "Polymorph",
                                                        FileAccessPlugin.Image.LoadSprite("Polymorph.png")
                                                      );


            // Create sub menu entry for each transformation
            foreach (ConfigEntry<string> iconName in transfromations)
            {
                if (iconName.Value != "")
                {
                    RadialUI.RadialSubmenu.CreateSubMenuItem(   RadialUI.RadialUIPlugin.Guid + ".Ploymorph",
                                                                iconName.Value,
                                                                (FileAccessPlugin.File.Exists(iconName.Value + ".PNG")) ? FileAccessPlugin.Image.LoadSprite(iconName.Value + ".PNG") : FileAccessPlugin.Image.LoadSprite("Polymorph.png"),
                                                                (cid, obj, mmi) => ApplyPolymorph(cid, obj, mmi, iconName.Value),
                                                                true, () => { return LocalClient.HasControlOfCreature(new CreatureGuid(RadialUI.RadialUIPlugin.GetLastRadialTargetCreature())); }
                                                            );
                }
            }

            StateDetection.Initialize(this.GetType());
        }

        /// <summary>
        /// Method to apply transformation using CMP
        /// </summary>
        /// <param name="cid">Radial menu creature</param>
        /// <param name="transformationName">String version of transformation name</param>
        private void ApplyPolymorph(CreatureGuid cid, string obj, MapMenuItem mmi, string transformationName)
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
