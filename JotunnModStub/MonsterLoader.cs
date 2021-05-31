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

namespace MonsterLoader
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class MonsterLoader : BaseUnityPlugin
    {
        public const string PluginGUID = "com.zarboz.MonsterLoader";
        public const string PluginName = "MonsterLoader";
        public const string PluginVersion = "0.0.1";
        private AssetBundle assetBundle;
        private static GameObject fab;

        private void Awake()
        {
            loadassets();
            loadenemy();
        } 

        private void loadassets()
        {
            assetBundle = AssetUtils.LoadAssetBundleFromResources("earthtroll", typeof(MonsterLoader).Assembly);
        }

        private void loadenemy()
        {
            var fab = assetBundle.LoadAsset<GameObject>("EarthTroll");
            PrefabManager.Instance.AddPrefab(fab);

            var sfx_troll_hit = PrefabManager.Cache.GetPrefab<GameObject>("sfx_troll_hit");
            var sfx_troll_death = PrefabManager.Cache.GetPrefab<GameObject>("sfx_troll_death");
            var sfx_troll_alerted = PrefabManager.Cache.GetPrefab<GameObject>("sfx_troll_alerted");
            var sfx_troll_idle = PrefabManager.Cache.GetPrefab<GameObject>("sfx_troll_idle");
            //var troll_punch = PrefabManager.Cache.GetPrefab<GameObject>("troll_punch");
            //var troll_groundslam = PrefabManager.Cache.GetPrefab<GameObject>("troll_groundslam");
            //var troll_throw = PrefabManager.Cache.GetPrefab<GameObject>("troll_throw");
            //var troll_log_swing_v = PrefabManager.Cache.GetPrefab<GameObject>("troll_log_swing_v");
            //var troll_log_swing_h = PrefabManager.Cache.GetPrefab<GameObject>("troll_log_swing_h");
            var vfx_foresttroll_hit = PrefabManager.Cache.GetPrefab<GameObject>("vfx_forresttroll_hit");
            var vfx_foresttroll_death = PrefabManager.Cache.GetPrefab<GameObject>("vfx_foresttroll_death");
            EffectList hitfx = new EffectList { m_effectPrefabs = new EffectList.EffectData[2] { new EffectList.EffectData { m_prefab = sfx_troll_hit }, new EffectList.EffectData { m_prefab = vfx_foresttroll_hit } } };
            EffectList deathfx = new EffectList { m_effectPrefabs = new EffectList.EffectData[2] { new EffectList.EffectData { m_prefab = sfx_troll_death }, new EffectList.EffectData { m_prefab = vfx_foresttroll_death } } };
            EffectList idlefx = new EffectList { m_effectPrefabs = new EffectList.EffectData[1] { new EffectList.EffectData { m_prefab = sfx_troll_idle } } };
            EffectList alertfx = new EffectList { m_effectPrefabs = new EffectList.EffectData[1] { new EffectList.EffectData { m_prefab = sfx_troll_alerted } } };


            //Humanoid.ItemSet itemSet = new Humanoid.ItemSet { m_items = new Humanoid.ItemSet[1] { new Humanoid.ItemSet[3] { new Humanoid.ItemSet { m_name = "Unarmed", m_items = troll_punch }, new Humanoid.ItemSet { m_name = "Unarmed", m_items = troll_groundslam }, new Humanoid.ItemSet { m_name = "Unarmed", m_items = troll_throw } };
            //EffectList Unarmed = new EffectList { m_effectPrefabs = new EffectList[2] {new EffectList.EffectData  } }
            var humanoid = fab.GetComponent<Humanoid>();
            //randomset
            humanoid.m_hitEffects = hitfx;
            humanoid.m_deathEffects = deathfx;
            humanoid.m_health = 1500;

            //var CharacterDrop = fab.AddComponent<CharacterDrop>();
            //todo add some characterdropfabs pulled from cash like coins and blackmetal scrap

            var monsterAI = fab.GetComponent<MonsterAI>();
            monsterAI.m_alertedEffects = alertfx;
            monsterAI.m_idleSound = idlefx;
            
    }
}