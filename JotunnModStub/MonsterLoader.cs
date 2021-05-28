// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 
// File:    JotunnModStub.cs
// Project: JotunnModStub

using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using Jotunn.Utils;

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

        private void Awake()
        {

        }


    }
}