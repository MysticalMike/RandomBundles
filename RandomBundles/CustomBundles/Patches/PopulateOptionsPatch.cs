using Harmony;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace RandomBundles.CustomBundles.Patches
{
    [HarmonyPatch(typeof(AdvancedGameOptions), "PopulateOptions")]
    class PopulateOptionsPatch
    {
        private static ModEntry Main;

        public static void Initialize(ModEntry main)
        {
            Main = main;
        }

        [HarmonyPrefix]
        public static bool Prefix(AdvancedGameOptions __instance)
        {
            Main.DebugMessage("Populated options");

            __instance.options.Clear();
            __instance.tooltips.Clear();
            __instance.applySettingCallbacks.Clear();
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_Label")));
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_CCB"))
            {
                style = OptionsElement.Style.OptionLabel
            });
            __instance.AddDropdown("", Game1.content.LoadString("Strings\\UI:AGO_CCB_Tooltip"), () => Game1.bundleType, delegate (Game1.BundleType val)
            {
                Game1.bundleType = val;
            }, new KeyValuePair<string, Game1.BundleType>(Game1.content.LoadString("Strings\\UI:AGO_CCB_Normal"), Game1.BundleType.Default), new KeyValuePair<string, Game1.BundleType>(Game1.content.LoadString("Strings\\UI:AGO_CCB_Remixed"), Game1.BundleType.Remixed), new KeyValuePair<string, Game1.BundleType>("Randomized", (Game1.BundleType)2));
            __instance.AddCheckbox(Game1.content.LoadString("Strings\\UI:AGO_Year1Completable"), Game1.content.LoadString("Strings\\UI:AGO_Year1Completable_Tooltip"), () => Game1.game1.GetNewGameOption<bool>("YearOneCompletable"), delegate (bool val)
            {
                Game1.game1.SetNewGameOption("YearOneCompletable", val);
            });
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_MineTreasureShuffle"))
            {
                style = OptionsElement.Style.OptionLabel
            });
            __instance.AddDropdown("", Game1.content.LoadString("Strings\\UI:AGO_MineTreasureShuffle_Tooltip"), () => Game1.game1.GetNewGameOption<Game1.MineChestType>("MineChests"), delegate (Game1.MineChestType val)
            {
                Game1.game1.SetNewGameOption("MineChests", val);
            }, new KeyValuePair<string, Game1.MineChestType>(Game1.content.LoadString("Strings\\UI:AGO_CCB_Normal"), Game1.MineChestType.Default), new KeyValuePair<string, Game1.MineChestType>(Game1.content.LoadString("Strings\\UI:AGO_CCB_Remixed"), Game1.MineChestType.Remixed));
            __instance.AddCheckbox(Game1.content.LoadString("Strings\\UI:AGO_FarmMonsters"), Game1.content.LoadString("Strings\\UI:AGO_FarmMonsters_Tooltip"), () => Game1.spawnMonstersAtNight, delegate (bool val)
            {
                Game1.spawnMonstersAtNight = val;
            });
            __instance.AddDropdown(Game1.content.LoadString("Strings\\UI:Character_Difficulty"), Game1.content.LoadString("Strings\\UI:AGO_ProfitMargin_Tooltip"), () => Game1.player.difficultyModifier, delegate (float val)
            {
                Game1.player.difficultyModifier = val;
            }, new KeyValuePair<string, float>(Game1.content.LoadString("Strings\\UI:Character_Normal"), 1f), new KeyValuePair<string, float>("75%", 0.75f), new KeyValuePair<string, float>("50%", 0.5f), new KeyValuePair<string, float>("25%", 0.25f));
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_MPOptions_Label")));
            __instance.AddDropdown(Game1.content.LoadString("Strings\\UI:Character_StartingCabins"), Game1.content.LoadString("Strings\\UI:AGO_StartingCabins_Tooltip"), () => Game1.startingCabins, delegate (int val)
            {
                Game1.startingCabins = val;
            }, new KeyValuePair<string, int>(Game1.content.LoadString("Strings\\UI:Character_none"), 0), new KeyValuePair<string, int>("1", 1), new KeyValuePair<string, int>("2", 2), new KeyValuePair<string, int>("3", 3));
            __instance.AddDropdown(Game1.content.LoadString("Strings\\UI:Character_CabinLayout"), Game1.content.LoadString("Strings\\UI:AGO_CabinLayout_Tooltip"), () => Game1.cabinsSeparate, delegate (bool val)
            {
                Game1.cabinsSeparate = val;
            }, new KeyValuePair<string, bool>(Game1.content.LoadString("Strings\\UI:Character_Close"), value: false), new KeyValuePair<string, bool>(Game1.content.LoadString("Strings\\UI:Character_Separate"), value: true));
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_OtherOptions_Label")));
            __instance.options.Add(new OptionsElement(Game1.content.LoadString("Strings\\UI:AGO_RandomSeed"))
            {
                style = OptionsElement.Style.OptionLabel
            });
            OptionsTextEntry optionsTextEntry = __instance.AddTextEntry("", Game1.content.LoadString("Strings\\UI:AGO_RandomSeed_Tooltip"), () => (!Game1.startingGameSeed.HasValue) ? "" : Game1.startingGameSeed.Value.ToString(), delegate (string val)
            {
                val.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    Game1.startingGameSeed = null;
                }
                else
                {
                    ulong result = 0uL;
                    while (val.Length > 0)
                    {
                        if (ulong.TryParse(val, out result))
                        {
                            Game1.startingGameSeed = result;
                            break;
                        }
                        val = val.Substring(0, val.Length - 1);
                    }
                }
            });
            optionsTextEntry.textBox.numbersOnly = true;
            optionsTextEntry.textBox.textLimit = 9;
            for (int i = __instance.options.Count; i < 7; i++)
            {
                __instance.options.Add(new OptionsElement(""));
            }

            return false;
        }
    }
}
