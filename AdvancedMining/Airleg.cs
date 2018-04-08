// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
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
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(DrillingSkill), 2)]   //_If_ReqLevel_
    [RepairRequiresSkill(typeof(DrillingSkill), 2)]
    public partial class AirlegRecipe : Recipe
    {
        public AirlegRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<AirlegItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(DrillingEfficiencySkill), 40, DrillingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperIngotItem>(typeof(DrillingEfficiencySkill), 10, DrillingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(AirlegRecipe), Item.Get<AirlegItem>().UILink(), 30, typeof(DrillingSpeedSkill));
            this.Initialize("Airleg", typeof(AirlegRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(5000)]
    [Category("Tools")]
    public class AirlegItem : ToolItem
    {
        public override string FriendlyName { get { return "Airleg Drill"; } }
        public override string Description { get { return "Powered drill for boring into rock"; } }
        public override float DurabilityRate { get { return DurabilityMax / 500f; } }
        private float MaxDrill = 10; //maximum drill range  may be able to hook this in with drilling efficieny additive strategy at some point to extend range
        private static SkillModifiedValue caloriesBurn = CreateCalorieValue(20, typeof(DrillingEfficiencySkill), typeof(AirlegItem), new AirlegItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Item RepairItem { get { return Item.Get<IronIngotItem>(); } }
        public override int FullRepairAmount { get { return 20; } }
        private float depth;
        private string message;
        public override string LeftActionDescription { get { return "Drill"; } }
        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }



        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (context.Target is WorldObject)
            {
                if ((context.Target as WorldObject).HasComponent<DrillBlastComponent>())
                {
                    this.depth = (context.Target as WorldObject).GetComponent<DrillBlastComponent>().DrillDepth;


                    if (this.depth < this.MaxDrill)
                    {
                        (context.Target as WorldObject).GetComponent<DrillBlastComponent>().DrillIncrement();
                        this.depth = (context.Target as WorldObject).GetComponent<DrillBlastComponent>().DrillDepth;
                        this.message = string.Format("Drilled to {0}", this.depth);
                        this.BurnCalories(context.Player);
                        context.Player.SendTemporaryMessageLoc(this.message, ChatCategory.Info);
                    }
                    else
                    {
                        context.Player.SendTemporaryMessageLoc("You have drilled as far as you can go", ChatCategory.Info);
                        return InteractResult.NoOp;
                    }

                }

                else
                    return InteractResult.NoOp;

            }
            else
                return InteractResult.NoOp;



            return InteractResult.Success;
        }
        /* this was from prototyping
        public override InteractResult OnActRight(InteractionContext context)
        {
            if (context.Target is WorldObject)
            {
                if ((context.Target as WorldObject).HasComponent<DrillBlastComponent>())
                {
                    var facing = (context.Target as WorldObject).Rotation.Forward;
                    if ((context.Target as WorldObject).GetComponent<DrillBlastComponent>().DetectExplosive(context.Player))
                        {
                            (context.Target as WorldObject).GetComponent<DrillBlastComponent>().BlockBreaker((context.Target as WorldObject).Position3i, facing); //break the rock
                            (context.Target as WorldObject).Destroy(); //destroy the collar and contained explosive
                            return InteractResult.Success;
                        }
                    else
                        return InteractResult.NoOp;
                }
                else
                    return InteractResult.NoOp;
            }
            else
                return InteractResult.NoOp;
        }*/
        
    }
}