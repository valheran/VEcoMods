namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(MetallurgySkill), 4)]
    public partial class TailingsLeachateRecipe : Recipe
    {
        public TailingsLeachateRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TailingsLeachateItem>(),
                 new CraftingElement<StabilisedTailingsItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TailingsItem>(typeof(MetallurgyEfficiencySkill), 1, MetallurgyEfficiencySkill.AdditiveStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TailingsLeachateRecipe), Item.Get<TailingsLeachateItem>().UILink(), 4, typeof(MetallurgySpeedSkill));
            this.Initialize("Tailings Leachate", typeof(TailingsLeachateRecipe));

            CraftingComponent.AddRecipe(typeof(LeachingVatObject), this);
        }
    }


    [Serialized]
    [Weight(10000)]
    [Currency]
    public partial class TailingsLeachateItem :
    Item
    {
        public override string FriendlyName { get { return "Tailings Leachate"; } }
        public override string Description { get { return "Solution from reprocessed tailings, containing base and precious metals not extracted the first time"; } }

    }

}