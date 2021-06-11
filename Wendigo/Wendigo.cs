using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace Wendigo
{
    [BepInPlugin(PluginId, "WendigoMonsterZZ", "0.0.7")]
    public class Wendigo : BaseUnityPlugin
    {
        public const string PluginId = "WendigMonsterZZ";
        private Harmony _harmony;
        private static GameObject Wendigo1;
        private static GameObject Wendigo2;
        private AssetBundle wendigo;

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
            
            zNetScene.m_prefabs.Add(Wendigo1);
            zNetScene.m_prefabs.Add(Wendigo2);
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
            wendigo = GetAssetBundleFromResources("wendigo");

            Debug.Log("Loading Wendigo1");
            Wendigo1 = wendigo.LoadAsset<GameObject>("WendigoForest");
            Debug.Log("Loading Wendigo2");
            Wendigo2 = wendigo.LoadAsset<GameObject>("WendigoSwamp");

            wendigo?.Unload(false);

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
