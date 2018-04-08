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
    public partial class XanthateRecipe : Recipe
    {
        public XanthateRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<XanthateItem>(),
               

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(IndustrialChemistryEfficiencySkill), 1, IndustrialChemistryEfficiencySkill.MultiplicativeStrategy), //add sulphuric if ever implement
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(XanthateRecipe), Item.Get<XanthateItem>().UILink(), 2, typeof(IndustrialChemistrySpeedSkill));
            this.Initialize("Xanthate", typeof(XanthateRecipe));

            CraftingComponent.AddRecipe(typeof(ReactorVesselObject), this);
        }
    }


    [Serialized]
    [Weight(5000)]
    [Currency]
    public partial class XanthateItem :
    Item
    {
        public override string FriendlyName { get { return "Xanthate"; } }
        public override string Description { get { return "Reagent used to make minerals stick to bubbles for separation"; } }

    }

}