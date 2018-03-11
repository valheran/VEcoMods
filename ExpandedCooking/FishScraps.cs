namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(10)]                                          
    public partial class FishScrapsItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Fish Scraps"; } }
        public override string Description                      { get { return "Pieces of leftover fish and frames."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 5, Protein = 5, Vitamins = 0};
        public override float Calories                          { get { return 45; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(FishingSkill), 2)]    
    public partial class FishScrapsRecipe : Recipe
    {
        public FishScrapsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FishScrapsItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(MeatPrepEfficiencySkill), 1, MeatPrepEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishScrapsRecipe), Item.Get<FishScrapsItem>().UILink(), 2, typeof(MeatPrepSpeedSkill)); 
            this.Initialize("Fish Scraps", typeof(FishScrapsRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}