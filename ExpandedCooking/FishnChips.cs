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
    public partial class FishnChupsItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Fish n Chups"; } }
        public override string Description                      { get { return "Battered fish fillets with crispy chips"; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 11, Fat = 12, Protein = 15, Vitamins = 4};
        public override float Calories                          { get { return 630; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CulinaryArtsSkill), 2)] 
    public class FishnChupsRecipe : Recipe
    {
        public FishnChupsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<FishnChupsItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<OilItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CamasBulbItem>(typeof(CulinaryArtsEfficiencySkill), 24, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(CulinaryArtsEfficiencySkill), 12, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PreparedFishItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Fish n Chups", typeof(FishnChupsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishnChupsRecipe), this.UILink(), 5, typeof(CulinaryArtsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}