namespace Eco.Gameplay.Items
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Actionbar;
    using Core.Controller;
    using Core.Utils;
    using Core.Utils.AtomicAction;
    //using Eco.Core.Localization;
    using Eco.Gameplay.Systems.Tooltip;
    //using Eco.Shared.Localization;
    using Players;
    using Shared.Networking;
    using Shared.Serialization;
    using Shared.Utils;
    using Systems.TextLinks;


    public abstract class BigInventory : Inventory
    {
        public new int GetMaxAccepted(Item item, int currentQuantity)
        {
            if (item == null)
                return 0;

            if (this.Restrictions.Any())
                return  this.Restrictions.Min(restriction => restriction.MaxAccepted(item, currentQuantity));
            else
                return item.MaxStackSize;
        }
    }


}

namespace Eco.Gameplay.Components
{
    using System.Linq;
    using Items;
    using Shared.Serialization;
    using System.Collections.Generic;
    using Core.Controller;
    using Core.Utils.AtomicAction;
    using Eco.Core.Utils;
    using Eco.Gameplay.Economy;
    using Interactions;
    using Objects;
    using Players;
    using Shared.Utils;

    [Serialized]
    public class BulkPublicStorageComponent : BulkStorageComponent
    {
        [Serialized] public BigInventory Storage { get; private set; }

        public override Inventory GetInventory()
        {
            return this.Storage;
        }

        public BulkPublicStorageComponent()
        { }

        public BulkPublicStorageComponent(int numSlots)
        {
            this.Initialize(numSlots);
        }

        public BulkPublicStorageComponent(int numSlots, int maxWeight)
        {
            this.Initialize(numSlots, maxWeight);
        }

        public override void Initialize()
        {
            if (!(this.Storage is AuthorizationInventory))
            {
                // ensure the inventory type is authorization inventory (migration)
                var newInventory = new AuthorizationInventory(
                    this.Storage.Stacks.Count(),
                    this.Parent,
                    AuthorizationInventory.AuthroizationFlags.AuthedMayAdd | AuthorizationInventory.AuthroizationFlags.AuthedMayRemove);
                newInventory.ReplaceStacks(this.Storage.Stacks);
                this.Storage = newInventory;
            }
            base.Initialize();
        }

        public void Initialize(int numSlots, int maxWeight)
        {
            this.Initialize(numSlots);
            WeightRestriction.Add(this.Storage, maxWeight);
        }

        public void Initialize(int numSlots)
        {
            if (this.Storage == null)
                this.Storage = new AuthorizationInventory(
                    numSlots,
                    this.Parent,
                    AuthorizationInventory.AuthroizationFlags.AuthedMayAdd | AuthorizationInventory.AuthroizationFlags.AuthedMayRemove);
        }
    }

  

    [Serialized]
    public abstract class BulkStorageComponent : WorldObjectComponent, IInteractable, IInventoryWorldObjectComponent
    {
        [SyncToView] public abstract BigInventory Inventory { get; }

        public IEnumerable<Inventory> GetInventories()
        {
            return this.Inventory.SingleItemAsEnumerable();
        }

        public override void Initialize()
        {
            this.Parent.AddAsPOI(PointOfInterestCategory.Container);
        }
        public override void Destroy()
        {
            return this.Parent.RemoveAsPOI(PointOfInterestCategory.Container);
        }

        internal override IEnumerable<IAtomicAction> OnPickUp(Player player, InventoryChangeSet playerInvChanges)
        {
            foreach (var kvp in this.Inventory.TypeToCount)
                playerInvChanges.MoveItems(kvp.Key, kvp.Value, this.Inventory, player.User.Inventory);
            return base.OnPickUp(player, playerInvChanges);
        }

        // Left click to withdraw
        public InteractResult OnActLeft(InteractionContext context)
        {
            // Some tools can't pull from containers.
            if (context.SelectedItem != null)
            {
                var canPull = ItemAttribute.Get<PullFromChest>(context.SelectedItem.Type)?.CanPull ?? true;
                if (!canPull)
                    return InteractResult.NoOp;
            }

            ToolItem selectedTool = context.SelectedItem as ToolItem;
            ValResult<int> result = this.Inventory.MoveAsManyItemsAsPossible(context.Player.User.Inventory.Carried, itemStack =>
            {
                var requiresTool = ItemAttribute.Get<RequiresToolAttribute>(itemStack.Item.Type);
                return requiresTool?.IsValidTool(context.SelectedItem?.Type) ?? true;
            }, context.Player.User);

            if (result.Success)
            {
                if (result.Val > 0)
                    return InteractResult.Success;
                else
                    return InteractResult.NoOp;
            }
            else
                return InteractResult.Failure(result.Message);
        }

        // right click to place
        public InteractResult OnActRight(InteractionContext context)
        {
            User user = context.Player.User;
            ValResult<int> invResult;

            if (user.Inventory.Carried.Stacks.Any(stack => stack.Quantity > 0))
                invResult = user.Inventory.Carried.MoveAsManyItemsAsPossible(this.Inventory, user);
            else if (context.SelectedItem is ToolItem)
                return InteractResult.NoOp;
            else
                invResult = user.Inventory.Toolbar.MoveAsManyItemsAsPossible(this.Inventory, stack => stack == user.Inventory.Toolbar.SelectedStack, user);

            if (invResult.Success)
            {
                if (invResult.Val > 0)
                    return InteractResult.Success;
                else
                    return InteractResult.NoOp;
            }
            else
                return InteractResult.Failure(invResult.Message);
        }

        public Item FindItemCantContain(IEnumerable<ItemStack> stacks)
        {
            // Could make this better by seeing if it can fit all the listed items.
            var unacceptableItem = stacks.FirstOrDefault(stack => stack.Item != null && this.Inventory.GetMaxAccepted(stack.Item, 1) == 0)?.Item;
            return unacceptableItem;
        }

        public InteractResult OnActInteract(InteractionContext context) { return InteractResult.NoOp; }
    }
}
