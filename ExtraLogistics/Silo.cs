namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    //[RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class SiloObject : WorldObject
    {
        public override string FriendlyName { get { return "Storage Silo"; } } 


        protected override void Initialize()
        {
            //this.GetComponent<MinimapComponent>().Initialize("Storage");                                 

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(1);
            storage.Storage.AddRestriction(new StackLimitRestriction(1000));


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class SiloItem : WorldObjectItem<SiloObject>
    {
        public override string FriendlyName { get { return "Silo"; } } 
        public override string Description { get { return "Large storage for a single item type"; } }

        static SiloItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(BasicCraftingSkill), 1)]
    public partial class SiloRecipe : Recipe
    {
        public SiloRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SiloItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(BasicCraftingEfficiencySkill), 10, BasicCraftingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(2, BasicCraftingSpeedSkill.MultiplicativeStrategy, typeof(BasicCraftingSpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(SiloRecipe), Item.Get<SiloItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SiloItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Silo", typeof(SiloRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}