using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundation : MonoBehaviour
{
    [SerializeField] private Transform buildAnchor;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private ResourceType[] requiredResources;
    [SerializeField] private Inventory buildResourceInventory;
    
    private void Awake() {
        buildResourceInventory.acceptedResourceTypesExactCount = requiredResources;
        buildResourceInventory.OnContentsChanged += OnBuildResourceInventoryChanged;
    }

    private void OnBuildResourceInventoryChanged() {
        if (buildResourceInventory.HasItems(requiredResources)) {
            Build();
        }
    }

    private void Build() {
        foreach (ResourceType type in requiredResources) {
            Destroy(buildResourceInventory.RemoveResourceOfType(type).gameObject);
        }
        Instantiate(buildingPrefab, buildAnchor.position, Quaternion.identity, buildAnchor);

        buildResourceInventory.OnContentsChanged -= OnBuildResourceInventoryChanged;
        Destroy(buildResourceInventory.gameObject);
    }
}
