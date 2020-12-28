using StardewValley.GameData;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace RandomBundles.CustomBundles
{

    // Bundle deserializer lmao
    class FileManager
    {
        public static List<RandomBundleData> getCustomBundleData(string path)
        {
            if (File.Exists(path))
            {
                return new JavaScriptSerializer().Deserialize<List<RandomBundleData>>(File.ReadAllText(path));
            }
            else return null;
        }
    }
}
