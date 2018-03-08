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
    public partial class TunaTacoItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Tuna Taco"; } }
        public override string Description                      { get { return "Because everything is better in a taco."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 5, Protein = 10, Vitamins = 13};
        public override float Calories                          { get { return 585; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CulinaryArtsSkill), 1)] 
    public class TunaTacoRecipe : Recipe
    {
        public TunaTacoRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<TunaTacoItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FishScrapsItem>(typeof(CulinaryArtsEfficiencySkill), 30, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WildMixItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TortillaItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
              
            };
            this.Initialize("Tuna Taco", typeof(TunaTacoRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(TunaTacoRecipe), this.UILink(), 15, typeof(CulinaryArtsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}