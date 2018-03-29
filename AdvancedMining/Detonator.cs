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


    [RequiresSkill(typeof(BlastingSkill), 2)]   //_If_ReqLevel_
    [RepairRequiresSkill(typeof(BlastingSkill), 2)]
    public partial class DetonatorRecipe : Recipe
    {
        public DetonatorRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<DetonatorItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(BlastingEfficiencySkill), 40, BlastingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperIngotItem>(typeof(BlastingEfficiencySkill), 10, BlastingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(DetonatorRecipe), Item.Get<DetonatorItem>().UILink(), 30, typeof(BlastingSpeedSkill));
            this.Initialize("Detonator", typeof(DetonatorRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
    [Serialized]
    [Category("Tools")]
    public class DetonatorItem : ToolItem
    {
        public override string FriendlyName { get { return "Detonator"; } }
        public override string Description { get { return "Control system for letting off the bang."; } }
        public override float DurabilityRate { get { return 0; } }
        //public override string LeftActionDescription { get { return "Check Loadout"; } }
        //public override string RighttActionDescription { get { return "Detonate"; } }
        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (context.Target is WorldObject)
            {
                if ((context.Target as WorldObject).HasComponent<DrillBlastComponent>())
                {

                    (context.Target as WorldObject).GetComponent<DrillBlastComponent>().DetectExplosive(context.Player);
                    this.BurnCalories(context.Player);
                    return InteractResult.Success;

                }
                else
                    return InteractResult.NoOp;
            }
            else
                return InteractResult.NoOp;

           
        }

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
        }
    }
}