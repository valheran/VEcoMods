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
    public partial class LobsterThermidorItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Lobster Thermidor"; } }
        public override string Description                      { get { return "Whole lobster gratin with matching sides."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 7, Fat = 16, Protein = 18, Vitamins = 8};
        public override float Calories                          { get { return 1080; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CulinaryArtsSkill), 4)] 
    public class LobsterThermidorRecipe : Recipe
    {
        public LobsterThermidorRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<LobsterThermidorItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FishScrapsItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<InfusedOilItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SashimiFishItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Lobster Thermidor", typeof(LobsterThermidorRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(LobsterThermidorRecipe), this.UILink(), 18, typeof(CulinaryArtsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}