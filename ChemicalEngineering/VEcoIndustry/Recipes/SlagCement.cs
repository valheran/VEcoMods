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

    [RequiresSkill(typeof(CementSkill), 3)]
    public partial class SlagCementRecipe : Recipe
    {
        public SlagCementRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(CementProductionEfficiencySkill), 20, CementProductionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SlagItem>(typeof(CementProductionEfficiencySkill), 20, CementProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ConcreteRecipe), Item.Get<ConcreteItem>().UILink(), 2, typeof(CementProductionSpeedSkill));
            this.Initialize("Concrete", typeof(ConcreteRecipe));

            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }


}