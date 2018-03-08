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
    public partial class FoilBakedSalmonItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Foil Baked Salmon"; } }
        public override string Description                      { get { return "Whole salmon baked with a generous pat of butter."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 10, Protein = 14, Vitamins = 0};
        public override float Calories                          { get { return 630; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(BasicBakingSkill), 2)] 
    public class FoilBakedSalmonRecipe : Recipe
    {
        public FoilBakedSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<FoilBakedSalmonItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(BasicBakingEfficiencySkill), 1, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Foil Baked Salmon", typeof(FoilBakedSalmonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(FoilBakedSalmonRecipe), this.UILink(), 5, typeof(BasicBakingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}