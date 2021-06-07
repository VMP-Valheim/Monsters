using BepInEx;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BeeBomber
{

    [BepInPlugin(PluginId, "Monsterzz", "0.0.2")]
    public class ThingLoader : BaseUnityPlugin
    {
        public const string PluginId = "Monsterzz";
        private Harmony _harmony;
        private static GameObject CrazyTroll;
        private static GameObject EarthTroll;
        private static GameObject Wizard;
        private static GameObject Golem;
        private static GameObject Yeti;

        private void Awake()
        {
            LoadAssets();
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginId);
        }
        public static void TryRegisterFabs(ZNetScene zNetScene)
        {
            if (zNetScene == null || zNetScene.m_prefabs == null || zNetScene.m_prefabs.Count <= 0)
            {
                return;
            }
            zNetScene.m_prefabs.Add(CrazyTroll);
            zNetScene.m_prefabs.Add(EarthTroll);
        }
        private static AssetBundle GetAssetBundleFromResources(string filename)
        {
            var execAssembly = Assembly.GetExecutingAssembly();
            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }
        private static void LoadAssets()
        {
            AssetBundle assetBundle = GetAssetBundleFromResources("earthtroll");
            AssetBundle golem = GetAssetBundleFromResources("golem");
            AssetBundle wizard = GetAssetBundleFromResources("wizard");
            AssetBundle yetiboy = GetAssetBundleFromResources("yetiboy");
            CrazyTroll = assetBundle.LoadAsset<GameObject>("CrazyTroll");
            EarthTroll = assetBundle.LoadAsset<GameObject>("EarthTroll");
            Wizard = wizard.LoadAsset<GameObject>("Wizard");
            Golem = golem.LoadAsset<GameObject>("Golem2");
            Yeti = yetiboy.LoadAsset<GameObject>("Yeti");

            assetBundle?.Unload(false);

        }

        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class ZNetScene_Awake_Patch
        {
            public static bool Prefix(ZNetScene __instance)
            {
                TryRegisterFabs(__instance);
                Debug.Log("Loading the eggs");
                return true;
            }
        }

        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        public static class ObjectDB_Awake_Patch
        {
            public static void Postfix()
            {
                Debug.Log("Growing some beasts.....");

            }
        }
        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }
}