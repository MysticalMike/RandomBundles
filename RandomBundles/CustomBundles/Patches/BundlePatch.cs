using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using System;
using System.IO;

namespace RandomBundles.CustomBundles.Patches
{
    [HarmonyPatch(typeof(Bundle))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(int), typeof(string), typeof(bool[]), typeof(Point), typeof(string), typeof(JunimoNoteMenu) })]
    class BundlePatch
    {
        private static ModEntry Main;
        private static string Stream;

        public static void Initialize(ModEntry main, string stream)
        {
            Main = main;
            Stream = stream;
        }

        [HarmonyPostfix]
        public static void Postfix(Bundle __instance, ref string rawBundleInfo)
        {
            string[] split = rawBundleInfo.Split('/');

            if (split.Length < 6)
                return;
            
            string[] sprite_index = split[5].Split(':');

            try
            {
                if (sprite_index[0] == "Data\\BundleSprites")
                {
                    __instance.bundleTextureOverride = LoadPNG(Path.Combine(Stream, "Data\\BundleSprites.png"));
                    __instance.bundleTextureIndexOverride = int.Parse(sprite_index[1]);
                }
            }
            catch (Exception)
            {
                __instance.bundleTextureOverride = null;
                __instance.bundleTextureIndexOverride = -1;
                Main.DebugMessage("Error loading bundle image.");
            }
        }

        public static Texture2D LoadPNG(string path)
        {
            FileStream filestream = File.Open(path, FileMode.Open);
            Texture2D texture = Texture2D.FromStream(GameRunner.instance.GraphicsDevice, filestream);
            filestream.Dispose();
            return texture;
        }
    }
}
