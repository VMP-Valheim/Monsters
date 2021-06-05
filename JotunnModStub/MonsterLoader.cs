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
using System.Collections.Generic;

namespace MonsterLoader
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.None)]
    internal class MonsterLoader : BaseUnityPlugin
    {
        public const string PluginGUID = "com.zarboz.MonsterLoader";
        public const string PluginName = "MonsterLoader";
        public const string PluginVersion = "0.0.1";
        private AssetBundle assetBundle;
        private AssetBundle yetiboy;

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
            yetiboy = AssetUtils.LoadAssetBundleFromResources("yetiboy", typeof(MonsterLoader).Assembly);
        }

         
        private void loadenemy()
        {
            try { 

            var fab = assetBundle.LoadAsset<GameObject>("EarthTroll");
            var humanoid = fab.GetComponent<Humanoid>();
            var fuckstain = PrefabManager.Cache.GetPrefab<GameObject>("Coins");
            List<CharacterDrop.Drop> fuckingshit = new List<CharacterDrop.Drop>
            {
                new CharacterDrop.Drop
                {
                    m_prefab = fuckstain,
                }
            };
                humanoid.m_health = 1500;

                var fuckingshitballs = fab.AddComponent<CharacterDrop>();
                fuckingshitballs.m_drops = fuckingshit;
                fuckingshitballs.m_dropsEnabled = true;
                var fab2 = assetBundle.LoadAsset<GameObject>("CrazyTroll");
                var eatdickbro = fab2.AddComponent<CharacterDrop>();
                eatdickbro.m_drops = fuckingshit;
                eatdickbro.m_dropsEnabled = true;

                var stupidyeti = yetiboy.LoadAsset<GameObject>("YetiBoy");
                var stupidfuck = stupidyeti.AddComponent<CharacterDrop>();
                stupidfuck.m_drops = fuckingshit;
                stupidfuck.m_dropsEnabled = true;
                var YetiboyFenring = yetiboy.LoadAsset<GameObject>("YetiboyFenring");
                var YetiboyFenring2 = stupidyeti.AddComponent<CharacterDrop>();
                YetiboyFenring2.m_drops = fuckingshit;
                YetiboyFenring2.m_dropsEnabled = true;
                Jotunn.Logger.LogMessage("Character drops added....for RRR I guess?");
            PrefabManager.Instance.AddPrefab(fab2);
            PrefabManager.Instance.AddPrefab(fab);
            PrefabManager.Instance.AddPrefab(stupidyeti);
            PrefabManager.Instance.AddPrefab(YetiboyFenring);
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