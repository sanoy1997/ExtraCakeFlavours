using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static ExtraCakeFlavours.Helper;
using System.Drawing;
using Kitchen;
using static Kitchen.ItemGroupView;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Common;

namespace ExtraCakeFlavours.Bakery
{
    public class SugarFlavour : CustomItem
    {
        public override string UniqueNameID => "Sugar Flavour";
        public override GameObject Prefab => getPrefab(GetGDO<Item>(ItemReferences.Sugar));
        public override string ColourBlindTag => "Su";

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
                GetGDO<Item>(ItemReferences.Sugar)
            };
        }

    }
}