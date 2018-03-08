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

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 5, Protein = 13, Vitamins = 8};
        public override float Calories                          { get { return 1080; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CampfireCreationsSkill), 4)] 
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
                new CraftingElement<FishScrapsItem>(typeof(CampfireCreationsEfficiencySkill), 30, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CamasBulbItem>(typeof(CampfireCreationsEfficiencySkill), 10, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CornItem>(typeof(CampfireCreationsEfficiencySkill), 10, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(CampfireCreationsEfficiencySkill), 40, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Fish n Chups", typeof(FishnChupsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishnChupsRecipe), this.UILink(), 9, typeof(CampfireCreationsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}