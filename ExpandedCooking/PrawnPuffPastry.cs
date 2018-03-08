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
    public partial class PrawnPuffPastryItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Prawn Puff Pastry"; } }
        public override string Description                      { get { return "Delicate puff pastry stuffed with king prawns."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat =10, Protein = 18, Vitamins = 8};
        public override float Calories                          { get { return 1260; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(LeavenedBakingSkill), 4)] 
    public class PrawnPuffPastryRecipe : Recipe
    {
        public PrawnPuffPastryRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<PrawnPuffPastryItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(LeavenedBakingEfficiencySkill), 5, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(LeavenedBakingEfficiencySkill), 15, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(LeavenedBakingEfficiencySkill), 5, LeavenedBakingEfficiencySkill.MultiplicativeStrategy),

            };
            this.Initialize("Prawn Puff Pastry", typeof(PrawnPuffPastryRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(PrawnPuffPastryRecipe), this.UILink(), 7, typeof(LeavenedBakingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}