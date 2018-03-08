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
    [Weight(500)]                                          
    public partial class PreparedFishItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Prepared Fish"; } }
        public override string Description                      { get { return "Carefully portioned fish, ready to cook."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 6, Protein = 4, Vitamins = 0};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MeatPrepSkill), 2)]    
    public partial class PreparedFishRecipe : Recipe
    {
        public PreparedFishRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PreparedFishItem>(),
               
               new CraftingElement<FishScrapsItem>(2), 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(MeatPrepEfficiencySkill), 10, MeatPrepEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PreparedFishRecipe), Item.Get<PreparedFishItem>().UILink(), 2, typeof(MeatPrepSpeedSkill)); 
            this.Initialize("Prepared Fish", typeof(PreparedFishRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}