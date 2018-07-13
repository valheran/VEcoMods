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
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    /*[RequiresSkill(typeof(ChloralkaliSkill), 1)]   
    public partial class GlycerolRecipe : Recipe
    {
        public GlycerolRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GlycerolItem>(4),          
           
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<KelpItem>(typeof(ChloralkaliEfficiencySkill), 10, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(ChloralkaliEfficiencySkill), 10, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GlycerolRecipe), Item.Get<GlycerolItem>().UILink(), 2, typeof(ChloralkaliSpeedSkill));    
            this.Initialize("Glycerol", typeof(GlycerolRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
    */

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class GlycerolItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Glycerol"; } }
        public override string Description { get { return "Oily substance formed as a byproduct of splitting bio-oils. Quite useful...."; } }

    }

}