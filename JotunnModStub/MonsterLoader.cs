using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("Monsterzz", "Monsterzz", "1.0.0")]
[BepInProcess("valheim.exe")]
public class Setup : BaseUnityPlugin
{
	[HarmonyPatch(typeof(ZNetScene), "Awake")]
	public static class ZNetScene_Awake_Patch
	{
		public static void Prefix(ZNetScene __instance)
		{
			if (!(__instance == null))
			{
				__instance.m_prefabs.AddRange(NewPrefabs);
			}
		}
	}

	[HarmonyPatch(typeof(ObjectDB), "CopyOtherDB")]
	public static class ObjectDB_CopyOtherDB_Patch
	{
		public static void Postfix()
		{
			AddNewPrefabs();
		}
	}

	[HarmonyPatch(typeof(ObjectDB), "Awake")]
	public static class ObjectDB_Awake_Patch
	{
		public static void Postfix()
		{
			AddNewPrefabs();
		}
	}

	private readonly Harmony harmony = new Harmony("Monsterzz");

	private static readonly List<GameObject> NewPrefabs = new List<GameObject>();

	private void Awake()
	{
		AssetBundle assetBundleFromResources = GetAssetBundleFromResources("yetiboy");
		List<string> list = new List<string>
		{
			"Assets/Yeti/YetiboyFenring.prefab",
			"Assets/Yeti/Yeti_attack_claw.prefab",
			"Assets/Yeti/Yeti_attack_jump.prefab",
			"Assets/Yeti/Yeti_taunt.prefab"
		};
		foreach (string item in list)
		{
			NewPrefabs.Add(assetBundleFromResources.LoadAsset<GameObject>(item));
		}
		harmony.PatchAll();
	}

	private void OnDestroy()
	{
		harmony.UnpatchSelf();
	}

	public static AssetBundle GetAssetBundleFromResources(string fileName)
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
        Stream stream = executingAssembly.GetManifestResourceStream(text);
		return AssetBundle.LoadFromStream(stream);
	}

	private static void AddNewPrefabs()
	{
		if (ObjectDB.instance == null || ObjectDB.instance.m_items.Count == 0 || ObjectDB.instance.GetItemPrefab("Amber") == null)
		{
			return;
		}
        foreach (GameObject newPrefab in NewPrefabs)
        {
            ItemDrop component = newPrefab.GetComponent<ItemDrop>();
            if (component != null && ObjectDB.instance.GetItemPrefab(newPrefab.name.GetStableHashCode()) == null)
            {
                ObjectDB.instance.m_items.Add(newPrefab);
                Dictionary<int, GameObject> dictionary = (Dictionary<int, GameObject>)typeof(ObjectDB).GetField("m_itemByHash", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ObjectDB.instance);
                dictionary[newPrefab.name.GetStableHashCode()] = newPrefab;
            }
        }
    }
}