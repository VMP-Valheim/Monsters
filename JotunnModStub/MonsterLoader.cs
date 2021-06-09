using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MonsterLoader
{

    [BepInPlugin(PluginId, "Monsterzz", "0.0.5")]
    public class MonsterLoader : BaseUnityPlugin
    {
        public const string PluginId = "Monsterzz";
        private Harmony _harmony;
        private static GameObject CrazyTroll;
        private static GameObject EarthTroll;
        private static GameObject Wizard;
        private static GameObject Golem;
        private static GameObject Yeti;
        private static GameObject Nasty;
        private static GameObject WereWolf1;
        private static GameObject WereWolf2;
        private static GameObject WereWolf3;
        private AssetBundle assetBundle;
        private AssetBundle golem;
        private AssetBundle wizard;
        private AssetBundle yetiboy;
        private AssetBundle nasty;
        private AssetBundle werewolf;
        private AssetBundle werebear;
        //private AssetBundle testfish;

        private static GameObject marlin;
        private static GameObject yak;

        public static Dictionary<string, string> t;
        private static GameObject WereWolf4;
        private static GameObject WereWolf5;
        private static GameObject WereBear1;
        private static GameObject WereBear2;
        private static GameObject WereBear3;

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
            zNetScene.m_prefabs.Add(Wizard);
            zNetScene.m_prefabs.Add(Golem);
            zNetScene.m_prefabs.Add(Yeti);
            zNetScene.m_prefabs.Add(Nasty);
            zNetScene.m_prefabs.Add(WereWolf1);
            zNetScene.m_prefabs.Add(WereWolf2);
            zNetScene.m_prefabs.Add(WereWolf3);
            zNetScene.m_prefabs.Add(WereWolf4);
            zNetScene.m_prefabs.Add(WereWolf5);
            zNetScene.m_prefabs.Add(WereBear1);
            zNetScene.m_prefabs.Add(WereBear2);
            zNetScene.m_prefabs.Add(WereBear3);
            //zNetScene.m_prefabs.Add(marlin);
            //zNetScene.m_prefabs.Add(yak);

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
            assetBundle = GetAssetBundleFromResources("earthtroll");
            golem = GetAssetBundleFromResources("golem");
            wizard = GetAssetBundleFromResources("wizard");
            yetiboy = GetAssetBundleFromResources("yetiboy");
            nasty = GetAssetBundleFromResources("nasty");
            werewolf = GetAssetBundleFromResources("werewolf");
            werebear = GetAssetBundleFromResources("werebear");

            //testfish = GetAssetBundleFromResources("testfish");
            CrazyTroll = assetBundle.LoadAsset<GameObject>("CrazyTroll");
            //var thing1 = CrazyTroll.AddComponent<CharacterDrop>();
            //thing1.m_dropsEnabled = true;
            EarthTroll = assetBundle.LoadAsset<GameObject>("EarthTroll");
            //var thing2 = EarthTroll.AddComponent<CharacterDrop>();
            Debug.Log("Loading Wizard");
            Wizard = wizard.LoadAsset<GameObject>("Wizard");
            //var thing3 = Wizard.AddComponent<CharacterDrop>();
            Debug.Log("Loading Golem");
            Golem = golem.LoadAsset<GameObject>("Golem2");
            //var thing4 = Golem.AddComponent<CharacterDrop>();
            Debug.Log("Loading Yeti");
            Yeti = yetiboy.LoadAsset<GameObject>("Yeti");
            //var thing5 = Yeti.AddComponent<CharacterDrop>();
            Debug.Log("Loading Nasty Crawler");
            Nasty = nasty.LoadAsset<GameObject>("TheNasty");
            Debug.Log("Loading Wolf1");
            WereWolf1 = werewolf.LoadAsset<GameObject>("WereWolfBlackArmored");
            Debug.Log("Loading Wolf2");
            WereWolf2 = werewolf.LoadAsset<GameObject>("WereWolfDarkGrey");
            Debug.Log("Loading Wolf3");
            WereWolf3 = werewolf.LoadAsset<GameObject>("WereWolfBlack");
            Debug.Log("Loading Wolf4");
            WereWolf4 = werewolf.LoadAsset<GameObject>("WereWolfBrown");
            Debug.Log("Loading Wolf5");
            WereWolf5 = werewolf.LoadAsset<GameObject>("WereWolfWhite");
            Debug.Log("Loading Bear1");
            WereBear1 = werebear.LoadAsset<GameObject>("WereBearBlack");
            Debug.Log("Loading Bear2");
            WereBear2 = werebear.LoadAsset<GameObject>("WereBearGray");
            Debug.Log("Loading Bear3");
            WereBear3 = werebear.LoadAsset<GameObject>("WereBearRed");

            //Debug.Log("Loading Marlin");
            //marlin = testfish.LoadAsset<GameObject>("Marlin");
            //Debug.Log("Loading Yak");
            //yak = testfish.LoadAsset<GameObject>("Yak");

            assetBundle?.Unload(false);
            golem?.Unload(false);
            wizard?.Unload(false);
            yetiboy?.Unload(false);
            nasty?.Unload(false);
            werewolf?.Unload(false);
            werebear?.Unload(false);

            //testfish?.Unload(false);
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