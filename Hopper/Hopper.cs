namespace Eco.Gameplay.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items;
    using Eco.Shared.Utils;
    using Eco.World.Blocks;
    using Objects;
    using Property;
    using Shared.Math;
    using Shared.Networking;
    using Shared.Serialization;
    using Utils;
    using World;

    [Serialized]
    public class CollectorToolComponent : WorldObjectComponent //, INetObject
    {
        public int  ID      { get { throw new NotImplementedException(); } }
        public bool Active  { get { throw new NotImplementedException(); } }
        private Inventory ToolInventory;
        //[SyncToView] public SelectionInventory ToolActions { get; set; }
        //[Serialized, SyncToView] public Inventory Inventory { get; set; }


        //private VehicleComponent vehicle;
        //private FuelSupplyComponent fuelSupply;
        //private Item defaultItem;
        //private float joulesPerDigBlock = 0f;
        //private float joulesPerMineBlock = 0f;

        public CollectorToolComponent() { }

        public void Initialize(Inventory Storage)
        {
            //this.defaultItem            = defaultBlockItem;
            //this.joulesPerDigBlock      = joulesPerDigBlock;
            //this.joulesPerMineBlock     = joulesPerMineBlock;
            // this.vehicle                = this.Parent.GetComponent<VehicleComponent>();
            // this.fuelSupply             = this.Parent.GetComponent<FuelSupplyComponent>();

            this.ToolInventory = Storage;
            /*if (this.ToolActions == null)
            {
                this.ToolActions = new SelectionInventory(1);
               // this.ToolActions.AddItem(new VehicleToggleItem(this));
            }
*/
/*
            if (maxWeight != 0f)
                WeightRestriction.Add(this.ToolInventory, maxWeight);
            */
        }

      
        [RPC]
        public void Collect(List<int> targetObjects)
        {
            foreach (var id in targetObjects)
            {
                var obj = NetObjectManager.GetObject<INetObject>(id);
                if (obj is RubbleObject)
                {
                    var rubble = obj as RubbleObject;
                    rubble.TryPickup(this.ToolInventory);
                }
            }
        }

        
/*
        #region INetObject
        private void PackageToolAngles(BSONObject bsonObj)
        {
            if (this.ToolAngles != null)
            {
                var toolArray = BSONArray.New;
                foreach (var angle in this.ToolAngles)
                    toolArray.Add(BSONValue.NewBSONValue(angle));

                bsonObj["tool"] = toolArray;
            }
        }

        public void SendInitialState(BSONObject bsonObj, INetObjectViewer viewer)
        {
            this.PackageToolAngles(bsonObj);
        }

        public void SendUpdate(BSONObject bsonObj, INetObjectViewer viewer)
        {
            this.PackageToolAngles(bsonObj);
        }

        public void ReceiveUpdate(BSONObject bsonObj)
        {
            if (bsonObj.ContainsKey("tool"))
            {
                var angles = bsonObj["tool"].ArrayValue;
                this.ToolAngles = angles.Select(value => value.FloatValue).ToArray();
            }
        }

        public void ReceiveInitialState(BSONObject bsonObj) { throw new NotImplementedException(); }

        public bool IsRelevant(INetObjectViewer viewer) { throw new NotImplementedException(); }
        public bool IsNotRelevant(INetObjectViewer viewer) { throw new NotImplementedException(); }
        public bool IsUpdated(INetObjectViewer viewer) { throw new NotImplementedException(); }
        #endregion
*/
/*
        #region Storage
        public override Inventory Inventory => this.ToolInventory;
        #endregion
        */
    }
}



namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;
    using Gameplay.Components.Auth;

    [Serialized]
    //[RequireComponent(typeof(StandaloneAuthComponent))] 
    //[RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(CollectorToolComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public class HopperObject : WorldObject
    {
        
       
        protected override void Initialize()
        {
            base.Initialize();

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(5);
            this.GetComponent<CollectorToolComponent>().Initialize(storage.Storage);
        }
    }
    
    [Serialized]
    public partial class HopperItem : WorldObjectItem<HopperObject>
    {
        public override string FriendlyName { get { return "Hopper"; } } 
        public override string Description { get { return "Funnel loose objects with this"; } }

        static HopperItem()
        {
            
        }
        
    }
}
