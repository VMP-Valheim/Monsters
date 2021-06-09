using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WereBear
{
    [BepInPlugin(PluginId, "WereBearZZ", "0.0.7")]
    public class WereBear : BaseUnityPlugin
    {
        public const string PluginId = "WereBearZZ";
        private Harmony _harmony;
        private static GameObject WereBear1;
        private static GameObject WereBear2;
        private static GameObject WereBear3;
        private AssetBundle werebear;
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
            zNetScene.m_prefabs.Add(WereBear1);
            zNetScene.m_prefabs.Add(WereBear2);
            zNetScene.m_prefabs.Add(WereBear3);

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
        private void LoadAssets()
        {

            werebear = GetAssetBundleFromResources("werebear");

            Debug.Log("Loading Bear1");
            WereBear1 = werebear.LoadAsset<GameObject>("WereBearBlack");
            Debug.Log("Loading Bear2");
            WereBear2 = werebear.LoadAsset<GameObject>("WereBearGray");
            Debug.Log("Loading Bear3");
            WereBear3 = werebear.LoadAsset<GameObject>("WereBearRed");
            

            werebear?.Unload(false);

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

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }

}
