﻿using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static ExtraCakeFlavours.Helper;
using System.Drawing;

namespace ExtraCakeFlavours.Bakery
{
    public class BroccoliFlavour : CustomItem
    {
        public override string UniqueNameID => "Broccoli Flavour";
        public override GameObject Prefab => getPrefab(GetGDO<Item>(ItemReferences.BroccoliChopped));
        public override string ColourBlindTag => "Br";

        public override void OnRegister(Item gdo)
        {
            gdo.SatisfiedBy = new()
            {
                GetGDO<Item>(333230026), // cookie
                GetGDO<ItemGroup>(-1312823003), // donut
                GetGDO<ItemGroup>(1366309564), // cupcake
                GetGDO<Item>(-1532306603) // cake
            };

            gdo.NeedsIngredients = new()
            {
                GetGDO<Item>(ItemReferences.BroccoliChopped)
            };
        }
    }
}