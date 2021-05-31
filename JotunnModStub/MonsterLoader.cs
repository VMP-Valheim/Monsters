// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 
// File:    JotunnModStub.cs
// Project: JotunnModStub

using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using Jotunn.Utils;
using Jotunn.Managers;
using Jotunn.Entities;
using System;
using Jotunn.Configs;

namespace MonsterLoader
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Major)]
    internal class MonsterLoader : BaseUnityPlugin
    {
        public const string PluginGUID = "com.zarboz.MonsterLoader";
        public const string PluginName = "MonsterLoader";
        public const string PluginVersion = "0.0.1";
        private AssetBundle assetBundle;

        private void Awake()
        {
            loadassets();
            ItemManager.OnVanillaItemsAvailable += loadenemy;
            LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
            {
                Translations = {
                                {"earth_troll", "Evil Earth Troll"},
                                {"crazy_troll", "Dark Goblin"}
                }
            });
        }

        private void loadassets()
        {
            assetBundle = AssetUtils.LoadAssetBundleFromResources("earthtroll", typeof(MonsterLoader).Assembly);
        }

         
        private void loadenemy()
        {
            try { 
            var fab = assetBundle.LoadAsset<GameObject>("EarthTroll");
            var humanoid = fab.GetComponent<Humanoid>();
            humanoid.m_health = 1500;

            var fab2 = assetBundle.LoadAsset<GameObject>("CrazyTroll");
            PrefabManager.Instance.AddPrefab(fab2);
            PrefabManager.Instance.AddPrefab(fab);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex.Message}");
            }
            finally
            {
                Jotunn.Logger.LogInfo("Casting Magik into ObjectDB");
                ItemManager.OnVanillaItemsAvailable -= loadenemy;
            }
        }
    }
}