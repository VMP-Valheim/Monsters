using BepInEx;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Wizard;

namespace EvilWizard
{

  [BepInPlugin(PluginId, "Wizard", "0.0.8")]
    public class EvilWizard : BaseUnityPlugin
    {
        public const string PluginId = "Wizard";
        private Harmony _harmony;
        private static GameObject Wizard;
        private AssetBundle wizard;

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
            zNetScene.m_prefabs.Add(Wizard);

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
            wizard = GetAssetBundleFromResources("wizard");
            Debug.Log("Loading Wizard");
            Wizard = wizard.LoadAsset<GameObject>("Wizard");
            Wizard.AddMonoBehaviour(CustomMonoBehavioursNames.MyCustomMonoBehaviour);
            Wizard.GetOrAddMonoBehaviour(CustomMonoBehavioursNames.UnRemoveableCustomMonoBehaviour);
            wizard?.Unload(false);
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

    public static class CustomMonoBehavioursNames
    {
      public static string MyCustomMonoBehaviour = nameof(MyCustomMonoBehaviour);
      public static string UnRemoveableCustomMonoBehaviour = nameof(UnRemoveableCustomMonoBehaviour);
    }
}