
namespace Eco.Gameplay.Components
{
    using System;
    using System.Collections.Generic;
    using Auth;
    using Eco.Core.Controller;
    using Interactions;
    using Objects;
    using Items;
    using Players;
    using Property;
    using Shared.Math;
    using Shared.Networking;
    using Shared.Serialization;
    using Shared.Services;
    using Eco.World.Blocks;
    using Eco.World;
    using Systems.Chat;  // for debug assist

    [Serialized]
    //[RequireComponent(typeof(StatusComponent))]
    //[DefaultToUnlinked]
    public class DrillBlastComponent : WorldObjectComponent
    {
        [Serialized] private int drillDepth = 0;
        private Type[] blastingTypeList;
        public Item Explosive;
        public int ExplosiveSupply = 0;
        private string message;
        public int blastingDepth;
        private Inventory storage;
        private string style;

        public void Initialize(Type[] BlastingTypeList, Inventory Storage, String Style)
        {
            this.blastingTypeList = BlastingTypeList;
            this.storage = Storage;
            this.style = Style;
        }
        //drillhole depth handling
        public float DrillDepth
        {
            get { return this.drillDepth; }

        }
        public void DrillIncrement()
        {
            this.drillDepth = this.drillDepth + 1;
           // this.storage.AddRestriction(new StackLimitRestriction(this.drillDepth)); this seems to only apply once, or the first takes precedence
        }
           
        //Blasting
        public void BlockBreaker(Vector3i Pos, Vector3 Dir) // consider making this a bool to facilitate feedback?
        {
            //get collar position and facing direction
            Vector3i rung = Pos;
            Vector3i facing = StyleSet(this.style, Dir);






            //determine blast radius and depth
            if (this.Explosive.Fuel < 5)
            {
                //ChatManager.ServerMessageToAll("Column Mode", false);
                //step to next block until reach bottom - may want to invert this so that it blasts from the bottom up
                for (int a = 0; a < this.blastingDepth; a = a + 1)
                {
                    rung = rung + facing;
                    //break if mineable and replace with rubble
                    Block block = World.GetBlock(rung);
                    if (block.Is<Minable>())
                    {
                        World.DeleteBlock(rung);
                        RubbleObject.TrySpawnFromBlock(block.GetType(), rung);
                    }
                }
            }
            else if (this.Explosive.Fuel >5)
            {
                //ChatManager.ServerMessageToAll("cylinder Mode", false);
                DirectionAxis dir = LookupDirectionAxis(facing);


                for (int a = 0; a < this.blastingDepth; a = a + 1)
                {
                    rung = rung + facing;
                    Vector3i[] ring = DirectionExtensions.Get8Edges(dir);
                    //break if mineable and replace with rubble
                    Block block = World.GetBlock(rung);
                    if (block.Is<Minable>())
                    {
                        World.DeleteBlock(rung);
                        RubbleObject.TrySpawnFromBlock(block.GetType(), rung);
                    }
                    foreach (Vector3i neighbour in ring)
                    {
                       // ChatManager.ServerMessageToAll("neighbour mode", false);
                        Vector3i tar = rung + neighbour;
                        block = World.GetBlock(tar);
                        if (block.Is<Minable>())
                        {
                            World.DeleteBlock(tar);
                            RubbleObject.TrySpawnFromBlock(block.GetType(), tar);
                        }
                    }
                }
            }
            else
            {
                return;
            }
            
        }

        //Inventory Check
        public bool DetectExplosive(Player player)
        {

            if (storage.IsEmpty)
            {
                player.SendTemporaryMessageLoc("You have no Explosives loaded", ChatCategory.Info);
                this.blastingDepth = 0;
                return false;
            }
            else
            {
                foreach (var stack in this.storage.NonEmptyStacks)
                {

                    this.ExplosiveSupply = stack.Quantity;
                    this.Explosive = stack.Item;

                }

                if (this.Explosive.IsFuel) //this shouldnt be necessary as there is inventory restrictions
                {
                    this.message = string.Format("You have loaded {0} units of {1}", this.ExplosiveSupply, this.Explosive.Type.Name);
                    player.SendTemporaryMessageLoc(this.message, ChatCategory.Info);
                    this.blastingDepth = this.ExplosiveSupply;
                    return true;
                }
                else
                {

                    player.SendTemporaryMessageLoc("You have no Explosives loaded", ChatCategory.Info);
                    this.blastingDepth = 0;
                    return false;
                }
            }
        }
        public static Vector3i StyleSet (String Style, Vector3 Dir)
        {
            if (Style == "hor") return Dir.Round;
            if (Style == "inc") return Vector3i.Up;
            if (Style == "dec") return Vector3i.Down;
            else return Vector3i.Forward;
        }
        public static DirectionAxis LookupDirectionAxis(Vector3i vec)
        {
                        
                if (vec.x == 1)     return DirectionAxis.Right;
                if (vec.x == -1)    return DirectionAxis.Left;
                if (vec.y == 1)     return DirectionAxis.Up;
                if (vec.y == -1)     return DirectionAxis.Down;
                if (vec.z == 1)     return DirectionAxis.Forward;
                if (vec.z == -1)     return DirectionAxis.Backward;
                else return DirectionAxis.Forward; //this is probably not ideal...



        }

            
    }
}


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
    [RequireComponent(typeof(DrillBlastComponent))]
    //[RequireComponent(typeof(BlastingSupplyComponent))]
    //[RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public partial class BlastingCollarObject : WorldObject
    {
        public override string FriendlyName { get { return "Blasting Collar"; } } 
        
      
        public bool IsDrillable;

        private static Type[] blastingTypeList = new Type[]
       {
            typeof(DynamiteItem),
            typeof(ANFOItem),
            
       };
        
        protected override void Initialize()
        {
            
            

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(1);
            
            storage.Storage.AddInvRestriction(new SpecificItemTypesRestriction(blastingTypeList));
            this.GetComponent<DrillBlastComponent>().Initialize(blastingTypeList, storage.Storage, "hor");
            //this.GetComponent<BlastingSupplyComponent>().Initialize(1, blastingTypeList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }
    [Serialized]
    public partial class BlastingCollarItem : WorldObjectItem<BlastingCollarObject>
    {
        public override string FriendlyName { get { return "Blasting Collar"; } } 
        public override string Description { get { return "Used to keep open and access drillholes for blasting."; } }

        static BlastingCollarItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(AdvancedMiningSkill), 1)]
    public partial class BlastingCollarRecipe : Recipe
    {
        public BlastingCollarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BlastingCollarItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronPipeItem>(typeof(AdvancedMiningEfficiencySkill), 2, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PitchItem>(typeof(AdvancedMiningEfficiencySkill), 5, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
                 
            };

            this.CraftMinutes = CreateCraftTimeValue(typeof(BlastingCollarRecipe), Item.Get<BlastingCollarItem>().UILink(), 2, typeof(AdvancedMiningSpeedSkill));
            this.Initialize("Blasting Collar", typeof(BlastingCollarRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(DrillBlastComponent))]
    //[RequireComponent(typeof(BlastingSupplyComponent))]
    //[RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public partial class BlastingCollarDecObject : WorldObject
    {
        public override string FriendlyName { get { return "Blasting Collar Declined"; } }


        public bool IsDrillable;

        private static Type[] blastingTypeList = new Type[]
       {
            typeof(DynamiteItem),
            typeof(ANFOItem),

       };

        protected override void Initialize()
        {



            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(1);

            storage.Storage.AddInvRestriction(new SpecificItemTypesRestriction(blastingTypeList));
            this.GetComponent<DrillBlastComponent>().Initialize(blastingTypeList, storage.Storage, "dec");
            //this.GetComponent<BlastingSupplyComponent>().Initialize(1, blastingTypeList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }
    [Serialized]
    public partial class BlastingCollarDecItem : WorldObjectItem<BlastingCollarDecObject>
    {
        public override string FriendlyName { get { return "Blasting Collar Declined"; } }
        public override string Description { get { return "Used to keep open and access drillholes for blasting downwards."; } }

        static BlastingCollarDecItem()
        {

        }

    }

    [RequireComponent(typeof(PublicStorageComponent))]
    [RequiresSkill(typeof(AdvancedMiningSkill), 1)]
    public partial class BlastingCollarDecRecipe : Recipe
    {
        public BlastingCollarDecRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BlastingCollarDecItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronPipeItem>(typeof(AdvancedMiningEfficiencySkill), 2, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PitchItem>(typeof(AdvancedMiningEfficiencySkill), 5, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BlastingCollarDecRecipe), Item.Get<BlastingCollarDecItem>().UILink(), 2, typeof(AdvancedMiningSpeedSkill));
            this.Initialize("Blasting Collar", typeof(BlastingCollarDecRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(DrillBlastComponent))]
    //[RequireComponent(typeof(BlastingSupplyComponent))]
    //[RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public partial class BlastingCollarIncObject : WorldObject
    {
        public override string FriendlyName { get { return "Blasting Collar Inclined"; } }


        public bool IsDrillable;

        private static Type[] blastingTypeList = new Type[]
       {
            typeof(DynamiteItem),
            typeof(ANFOItem),

       };

        protected override void Initialize()
        {



            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(1);

            storage.Storage.AddInvRestriction(new SpecificItemTypesRestriction(blastingTypeList));
            this.GetComponent<DrillBlastComponent>().Initialize(blastingTypeList, storage.Storage, "inc");
            //this.GetComponent<BlastingSupplyComponent>().Initialize(1, blastingTypeList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }
    [Serialized]
    public partial class BlastingCollarIncItem : WorldObjectItem<BlastingCollarIncObject>
    {
        public override string FriendlyName { get { return "Blasting Collar Inclined"; } }
        public override string Description { get { return "Used to keep open and access drillholes for blasting upwards."; } }

        static BlastingCollarIncItem()
        {

        }

    }


    [RequiresSkill(typeof(AdvancedMiningSkill), 1)]
    public partial class BlastingCollarIncRecipe : Recipe
    {
        public BlastingCollarIncRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BlastingCollarIncItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronPipeItem>(typeof(AdvancedMiningEfficiencySkill), 2, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PitchItem>(typeof(AdvancedMiningEfficiencySkill), 5, AdvancedMiningEfficiencySkill.MultiplicativeStrategy),

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BlastingCollarIncRecipe), Item.Get<BlastingCollarIncItem>().UILink(), 2, typeof(AdvancedMiningSpeedSkill));
            this.Initialize("Blasting Collar", typeof(BlastingCollarIncRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}