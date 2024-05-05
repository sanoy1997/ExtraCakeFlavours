using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static Kitchen.ItemGroupView;
using static KitchenLib.Utils.GDOUtils;

namespace ExtraCakeFlavours
{
    public static class Helper
    {
        public static T GetGDO<T>(int id) where T : GameDataObject => GetExistingGDO(id) as T;
        public interface IWontRegister { }
        public static GameObject getPrefab(Item item)
        {
            GameObject gameObject = new();
            gameObject.SetActive(false);
            var prefab = Object.Instantiate(item.Prefab);
            prefab.transform.ParentTo(gameObject);
            return prefab;
        }
    }
}
