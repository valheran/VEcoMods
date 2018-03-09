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
    public partial class SeafoodMornayPieItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Seafood Mornay Pie"; } }
        public override string Description                      { get { return "This is what happens when you combine all the best seafood you can find with vegetables and a delicious mornay sauce."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 14, Fat = 14, Protein = 18, Vitamins = 14};
        public override float Calories                          { get { return 1350; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(LeavenedBakingSkill), 4)] 
    public class SeafoodMornayPieRecipe : Recipe
    {
        public SeafoodMornayPieRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SeafoodMornayPieItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(BasicBakingEfficiencySkill), 20, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<InfusedOilItem>(typeof(BasicBakingEfficiencySkill), 4, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClamItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<UrchinItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(BasicBakingEfficiencySkill), 5, BasicBakingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Seafood Mornay Pie", typeof(SeafoodMornayPieRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SeafoodMornayPieRecipe), this.UILink(), 18, typeof(BasicBakingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}