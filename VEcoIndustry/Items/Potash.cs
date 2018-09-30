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

    [RequiresSkill(typeof(ChloralkaliSkill), 1)]   
    public partial class PotashRecipe : Recipe
    {
        public PotashRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PotashItem>(4),          
           
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<KelpItem>(typeof(ChloralkaliEfficiencySkill), 10, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(ChloralkaliEfficiencySkill), 10, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PotashRecipe), Item.Get<PotashItem>().UILink(), 2, typeof(ChloralkaliSpeedSkill));    
            this.Initialize("Potash", typeof(PotashRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }


    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class PotashItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Potash"; } }
        public override string Description { get { return "Concentrated Alkali from seaweed and wood ash"; } }

    }

}