global using static ExtraCakeFlavours.Helper;
using ExtraCakeFlavours.Bakery;
using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Customs;
using KitchenLib.Event;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMods;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Kitchen.ItemGroupView;


// Namespace should have "Kitchen" in the beginning
namespace ExtraCakeFlavours
{
    public class Mod : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "com.sanoy.extracakeflavours";
        public const string MOD_NAME = "Extra Cake Flavours";
        public const string MOD_VERSION = "0.3.1";
        public const string MOD_AUTHOR = "sanoy";
        public const string MOD_GAMEVERSION = ">=1.1.9";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until

        // Boolean constant whose value depends on whether you built with DEBUG or RELEASE mode, useful for testing
#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = true;
#endif

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddFlavours()
        {
            AddFlavour("Ap"  , GetGDO<Item>(ItemReferences.AppleSlices));
            AddFlavour("Ba"  , GetGDO<Item>(ItemReferences.BambooCooked));
            AddFlavour("Br"  , GetGDO<Item>(ItemReferences.BroccoliChopped));
            AddFlavour("Ca"  , GetGDO<Item>(ItemReferences.CarrotChopped));
            //         "C"   , Coffee
            //         "Ch"  , Chocolate
            //         "Che" , Cherry
            AddFlavour("Chee", GetGDO<Item>(ItemReferences.CheeseGrated));
            AddFlavour("Eg"  , GetGDO<Item>(ItemReferences.EggCooked));
            AddFlavour("Fl"  , GetGDO<Item>(ItemReferences.Flour));
            AddFlavour("IC-C", GetGDO<Item>(ItemReferences.IceCreamChocolate));
            AddFlavour("IC-S", GetGDO<Item>(ItemReferences.IceCreamStrawberry));
            AddFlavour("IC-V", GetGDO<Item>(ItemReferences.IceCreamVanilla));
            //         "L"   , Lemon
            AddFlavour("Le"  , GetGDO<Item>(ItemReferences.LettuceChopped));
            AddFlavour("Ma"  , GetGDO<Item>(ItemReferences.Mayonnaise));
            AddFlavour("Man" , GetGDO<Item>(ItemReferences.MandarinSlice));
            AddFlavour("Me"  , GetGDO<Item>(ItemReferences.MeatChopped));
            AddFlavour("Mu"  , GetGDO<Item>(ItemReferences.MushroomChopped));
            AddFlavour("Nu"  , GetGDO<Item>(ItemReferences.NutsChopped));
            AddFlavour("Ol"  , GetGDO<Item>(ItemReferences.Olive));
            AddFlavour("On"  , GetGDO<Item>(ItemReferences.OnionChopped));
            AddFlavour("Po"  , GetGDO<Item>(ItemReferences.PotatoChopped));
            //         "Pu"  , Pumpkin
            AddFlavour("Ri"  , GetGDO<Item>(ItemReferences.Rice));
            AddFlavour("Su"  , GetGDO<Item>(ItemReferences.Sugar));
            AddFlavour("To"  , GetGDO<Item>(ItemReferences.TomatoChopped));
            
        }

        private void AddFlavour(string name, Item item)
        {
            GameObject prefab = getPrefab(item);
            AddFlavour(item, new(0, -0.06f, 0), new(), name,
                prefab, prefab, prefab, prefab, prefab
            );
        }

        private void AddFlavour(Item item, Vector3 PosOffset, Quaternion RotOffset, string ColourblindTag,
            GameObject cakePrefab, GameObject cupcakePrefab, GameObject donutPrefab, GameObject bakedCookiePrefab, GameObject cookiePrefab)
        {
            var addCookie = cookiePrefab != null && bakedCookiePrefab != null;
            var addDonut = donutPrefab != null;
            var addCupcake = cupcakePrefab != null;
            var addCake = cakePrefab != null;

            var mixingBowl = GetGDO<ItemGroup>(-705806008);
            var mixingView = mixingBowl.Prefab.GetComponent<ItemGroupView>();

            var cookieTray = GetGDO<ItemGroup>(-491299234);
            var bakedCookies = GetGDO<ItemGroup>(-502245988);
            var cookie = GetGDO<Item>(333230026);

            var donut = GetGDO<ItemGroup>(-1312823003);

            var cupcake = GetGDO<ItemGroup>(1366309564);

            var cakeTin = GetGDO<ItemGroup>(-1354941517);
            var cakeSlice = GetGDO<Item>(-1532306603);

            if (mixingBowl.DerivedSets[0].Items.Any(p => p == item))
                return;

            // Update GDO
            #region Mixing Bowl
            var mixingPrior = new List<Item>(mixingBowl.DerivedSets[0].Items) { item };
            mixingBowl.DerivedSets[0] = new()
            {
                Items = mixingPrior,
                Max = 1,
                Min = 1,
                IsMandatory = true
            };
            #endregion
            #region Cookies
            if (addCookie)
            {
                var cookiePrior = new List<Item>(cookieTray.DerivedSets[1].Items) { item };
                cookieTray.DerivedSets[1] = new()
                {
                    Items = cookiePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Donut
            if (addDonut)
            {
                var donutPrior = new List<Item>(donut.DerivedSets[1].Items) { item };
                donut.DerivedSets[1] = new()
                {
                    Items = donutPrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Cupcake
            if (addCupcake)
            {
                var cupcakePrior = new List<Item>(cupcake.DerivedSets[1].Items) { item };
                cupcake.DerivedSets[1] = new()
                {
                    Items = cupcakePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Cake
            if (addCake)
            {
                var cakePrior = new List<Item>(cakeTin.DerivedSets[1].Items) { item };
                cakeTin.DerivedSets[1] = new()
                {
                    Items = cakePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion

            if (mixingView.ComponentGroups.Any(c => c.Item == item))
                return;

            // Add prefabs
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            #region Mixing Bowl
            var itemPrefab = Object.Instantiate(item.Prefab);
            itemPrefab.transform.SetParent(mixingBowl.Prefab.transform.Find("Flavours"), false);
            itemPrefab.transform.localPosition = PosOffset;
            itemPrefab.transform.localRotation = RotOffset;
            itemPrefab.transform.localScale = Vector3.one / itemPrefab.transform.parent.localScale.x;
            #endregion
            #region Cookies
            if (addCookie)
            {
                // Unbaked
                for (int i = 0; i < cookieTray.Prefab.GetChildCount(); i++)
                {
                    var child = cookieTray.Prefab.GetChild(i);
                    if (child.name.Contains("Tray"))
                        continue;

                    SetupCookie(item, ColourblindTag, child, cookiePrefab);
                }
                // Baked
                for (int i = 0; i < bakedCookies.Prefab.GetChildCount(); i++)
                {
                    var child = bakedCookies.Prefab.GetChild(i);
                    if (child.name.Contains("Tray"))
                        continue;

                    SetupCookie(item, ColourblindTag, child, bakedCookiePrefab);
                }
                // Cookie
                SetupCookie(item, ColourblindTag, cookie.Prefab, bakedCookiePrefab);
            }
            #endregion
            #region Donut
            var donutPref = Object.Instantiate(donutPrefab);
            if (addDonut)
            {
                donutPref.transform.SetParent(donut.Prefab.transform.Find("Flavours"), false);
                donutPref.transform.localScale = Vector3.one / donutPref.transform.parent.localScale.x;
            }
            #endregion
            #region Cupcake
            var cupcakePref = Object.Instantiate(cupcakePrefab);
            if (addCupcake)
                cupcakePref.transform.SetParent(cupcake.Prefab.transform, false);
            #endregion
            #region Cake
            if (addCake)
            {
                for (int i = 0; i < cakeTin.Prefab.GetChild("Cake").GetChildCount(); i++)
                {
                    var child = cakeTin.Prefab.GetChild("Cake").GetChild(i);
                    if (!child.name.Contains("Cake"))
                        continue;

                    SetupCake(item, ColourblindTag, child, cakePrefab);
                }

                SetupCake(item, ColourblindTag, cakeSlice.Prefab, cakePrefab);
            }
            #endregion

            // Update ItemGroupViews
            #region Mixing Bowl
            mixingView.ComponentGroups.Add(new()
            {
                GameObject = itemPrefab,
                Item = item
            });
            #endregion
            #region Donut
            if (addDonut)
            {
                var donutView = donut.Prefab.GetComponent<ItemGroupView>();
                donutView.ComponentGroups.Add(new()
                {
                    GameObject = donutPref,
                    Item = item
                });
                List<ColourBlindLabel> donutLabels = (List<ColourBlindLabel>)componentLabels.GetValue(donutView);
                donutLabels.Add(new()
                {
                    Item = item,
                    Text = ColourblindTag
                });
                componentLabels.SetValue(donutView, donutLabels);
            }
            #endregion
            #region Cupcake
            if (addCupcake)
            {
                var cupcakeView = cupcake.Prefab.GetComponent<ItemGroupView>();
                cupcakeView.ComponentGroups.Add(new()
                {
                    GameObject = cupcakePref,
                    Item = item
                });
                List<ColourBlindLabel> cupcakeLabels = (List<ColourBlindLabel>)componentLabels.GetValue(cupcakeView);
                cupcakeLabels.Add(new()
                {
                    Item = item,
                    Text = ColourblindTag
                });
                componentLabels.SetValue(cupcakeView, cupcakeLabels);
            }
            #endregion
        }

        private void SetupCookie(Item item, string tag, GameObject baseObject, GameObject toInstantiate)
        {
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            var prefab = Object.Instantiate(toInstantiate);
            prefab.transform.SetParent(baseObject.transform.Find("Flavours"), false);
            prefab.transform.localScale = Vector3.one / prefab.transform.parent.localScale.x;

            var cookieView = baseObject.GetComponent<ItemGroupView>();
            cookieView.ComponentGroups.Add(new()
            {
                Item = item,
                GameObject = prefab
            });
            List<ColourBlindLabel> labels = (List<ColourBlindLabel>)componentLabels.GetValue(cookieView);
            labels.Add(new()
            {
                Item = item,
                Text = tag
            });
            componentLabels.SetValue(cookieView, labels);
        }

        private void SetupCake(Item item, string tag, GameObject baseObject, GameObject toInstantiate)
        {
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            var prefab = Object.Instantiate(toInstantiate);
            prefab.transform.SetParent(baseObject.transform, false);
            prefab.transform.localScale = Vector3.one;

            var cakeView = baseObject.GetComponent<ItemGroupView>();
            cakeView.ComponentGroups.Add(new()
            {
                Item = item,
                GameObject = prefab
            });
            List<ColourBlindLabel> labels = (List<ColourBlindLabel>)componentLabels.GetValue(cakeView);
            labels.Add(new()
            {
                Item = item,
                Text = tag
            });
            componentLabels.SetValue(cakeView, labels);
        }

        private void AddGameData()
        {
            MethodInfo AddGDOMethod = typeof(BaseMod).GetMethod(nameof(BaseMod.AddGameDataObject));
            int counter = 0;
            Log("Registering GameDataObjects.");
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract || typeof(IWontRegister).IsAssignableFrom(type))
                    continue;

                if (!typeof(CustomGameDataObject).IsAssignableFrom(type))
                    continue;

                MethodInfo generic = AddGDOMethod.MakeGenericMethod(type);
                generic.Invoke(this, null);
                counter++;
            }
            Log($"Registered {counter} GameDataObjects.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            AddGameData();

            Events.BuildGameDataEvent += (s, args) =>
            {
                AddFlavours();

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }
        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
