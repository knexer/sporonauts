using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transfers resources from a source inventory to all inventories in this object's children.
public class ResourceAutoTransfer : MonoBehaviour
{
    [SerializeField] private Inventory sourceInventory;
    private List<Inventory> inventories = new List<Inventory>();
    private bool isTransferring = false;

    private void Awake() {
        sourceInventory.OnContentsChanged += TransferResources;
        inventories.AddRange(GetComponentsInChildren<Inventory>());
        foreach (Inventory inventory in inventories) {
            inventory.OnContentsChanged += TransferResources;
        }
    }

    private void TransferResources() {
        if (isTransferring) {
            return;
        }
        isTransferring = true;
        Resource toTransfer = null;
        Inventory targetInventory = null;
        foreach (Inventory inventory in inventories) {
            if (inventory == sourceInventory) {
                continue;
            }

            foreach (Resource resource in sourceInventory.GetContents()) {
                if (inventory.CanAddResource(resource)) {
                    targetInventory = inventory;
                    toTransfer = resource;
                    // Break is valid here as long as only one resource is added/removed to an inventory at a time.
                    break;
                }
            }
            if (toTransfer != null) {
                break;
            }
        }
        if (toTransfer == null) {
            isTransferring = false;
            return;
        }

        sourceInventory.RemoveResource(toTransfer);
        targetInventory.AddResource(toTransfer);
        isTransferring = false;
    }
}
