// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Interactions;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Items;
    using Eco.Shared.Services;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(AdvancedMiningSkill), 3)]   //_If_ReqLevel_
    [RepairRequiresSkill(typeof(AdvancedMiningSkill), 4)]
    public partial class ProspectorRecipe : Recipe
    {
        public ProspectorRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<ProspectorItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(AdvancedMiningEfficiencySkill), 40, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronPipeItem>(typeof(AdvancedMiningEfficiencySkill), 50, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ProspectorRecipe), Item.Get<ProspectorItem>().UILink(), 30, typeof(AdvancedMiningSpeedSkill));
            this.Initialize("Prospector", typeof(ProspectorRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(5000)]
    [Category("Tools")]
    public class ProspectorItem : ToolItem
    {
        public override string FriendlyName { get { return "Prospector Drill"; } }
        public override string Description { get { return "Drill to retrieve cores and identify rock. range 50 blocks"; } }
        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(50, AdvancedMiningSkill.MultiplicativeStrategy, typeof(AdvancedMiningSkill), Localizer.Do("repair cost"));
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        public override float DurabilityRate { get { return DurabilityMax / 50f; } }
        private static SkillModifiedValue caloriesBurn = CreateCalorieValue(500, typeof(AdvancedMiningEfficiencySkill), typeof(ProspectorItem), new ProspectorItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Item RepairItem { get { return Item.Get<IronPipeItem>(); } }
        public override int FullRepairAmount { get { return 50; } }


        public override InteractResult OnActLeft(InteractionContext context)
        {

            if (!context.HasBlock)
                return InteractResult.NoOp;

            Vector3i pos = context.BlockPosition.Value;
            StringBuilder text = new StringBuilder();
            Vector3i depth = pos;
            for (int a = 0; a < 50; a = a + 1)
            {
               
                Block block = World.GetBlock(depth);
                
                text.AppendLine(block.ToString().Split('.').Last() + " " + depth.y);
                //depth = depth + Vector3i.Down;
                depth = depth - context.Normal.Value;
            }
            context.Player.OpenInfoPanel("Drillhole result", text.ToString());
            this.BurnCalories(context.Player);
            return InteractResult.Success;
        }
    }
            
}