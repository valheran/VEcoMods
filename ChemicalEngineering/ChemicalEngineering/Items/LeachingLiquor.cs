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

    [RequiresSkill(typeof(IndustrialChemistrySkill), 2)]
    public partial class LeachingLiquorRecipe : Recipe
    {
        public LeachingLiquorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LeachingLiquorItem>(),
               

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodPulpItem>(typeof(IndustrialChemistryEfficiencySkill), 10, IndustrialChemistryEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PitchItem>(typeof(IndustrialChemistryEfficiencySkill), 10, IndustrialChemistryEfficiencySkill.MultiplicativeStrategy),//placeholder, maybe try for making acid out of smelter gas
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LeachingLiquorRecipe), Item.Get<LeachingLiquorItem>().UILink(), 2, typeof(IndustrialChemistrySpeedSkill));
            this.Initialize("Leaching Liquor", typeof(LeachingLiquorRecipe));

            CraftingComponent.AddRecipe(typeof(ReactorVesselObject), this);
        }
    }


    [Serialized]
    [Weight(5000)]
    [Currency]
    public partial class LeachingLiquorItem :
    Item
    {
        public override string FriendlyName { get { return "LeachingLiquor"; } }
        public override string Description { get { return "Reagent used to dissolve minerals into solution"; } }

    }

}