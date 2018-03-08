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
    public partial class TunaMornayPieItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Tuna Mornay Pie"; } }
        public override string Description                      { get { return "Creamy fish pie."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 6, Protein = 10, Vitamins = 4};
        public override float Calories                          { get { return 1170; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(BakingSkill), 4)] 
    public class TunaMornayPieRecipe : Recipe
    {
        public TunaMornayPieRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<TunaMornayPieItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PreparedFishItem>(typeof(BakingEfficiencySkill), 5, BakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(BakingEfficiencySkill), 10, BakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(BakingEfficiencySkill), 5, BakingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Tuna Mornay Pie", typeof(TunaMornayPieRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(TunaMornayPieRecipe), this.UILink(), 4, typeof(BakingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}