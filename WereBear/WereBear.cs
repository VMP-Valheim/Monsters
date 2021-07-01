using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace WereBear
{
  [BepInPlugin(PluginId, "WereBearZZ", "0.0.7")]
  public class WereBear : BaseUnityPlugin
  {
    public const string PluginId = "WereBearZZ";
    private Harmony _harmony;

    private void Awake()
    {
      LoadAssets();
      _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginId);
    }

    private void LoadAssets()
    {
      var werebear = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("werebear", Assembly.GetExecutingAssembly());

      Debug.Log("Loading Bear1");
      var wereBear1 = werebear.LoadAsset<GameObject>("WereBearBlack");
      wereBear1.AddComponent<EvilWizard.MyCustomMonoBehaviour>();

      Debug.Log("Loading Bear2");
      var wereBear2 = werebear.LoadAsset<GameObject>("WereBearGray");
      wereBear2.AddComponent<EvilWizard.MyCustomMonoBehaviour>();

      Debug.Log("Loading Bear3");
      var wereBear3 = werebear.LoadAsset<GameObject>("WereBearRed");
      wereBear3.AddComponent<EvilWizard.MyCustomMonoBehaviour>();

      Jotunn.Managers.PrefabManager.Instance.AddPrefab(wereBear1);
      Jotunn.Managers.PrefabManager.Instance.AddPrefab(wereBear2);
      Jotunn.Managers.PrefabManager.Instance.AddPrefab(wereBear3);
    }

    private void OnDestroy()
    {
      _harmony?.UnpatchSelf();
    }
  }
}
