using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 1;
    [SerializeField] private Resource[] startingItemPrefabs;

    // The types of resources that can be added to this inventory. Empty array means any resource can be added.
    [SerializeField] public ResourceType[] acceptedResourceTypes;
    // The exact count of each resource type that can be added to this inventory. Empty array means any number of each resource can be added.
    [SerializeField] public ResourceType[] acceptedResourceTypesExactCount;

    private List<Resource> items = new List<Resource>();

    public int NumItems() {
        return items.Count;
    }

    public ReadOnlyCollection<Resource> GetContents() {
        return items.AsReadOnly();
    }

    public bool HasItems(ResourceType[] types) {
        List<Resource> availableResources = new List<Resource>(items);
        foreach (ResourceType type in types) {
            bool found = false;
            foreach (Resource resource in availableResources) {
                if (resource.GetResourceType() == type) {
                    found = true;
                    availableResources.Remove(resource);
                    break;
                }
            }
            if (!found) {
                return false;
            }
        }

        return true;
    }

    public float GetContentsMass() {
        float mass = 0f;
        foreach (Resource item in items) {
            mass += item.GetMass();
        }
        return mass;
    }

    public event System.Action OnContentsChanged;

    private void Awake() {
        foreach (Resource item in startingItemPrefabs) {
            AddResource(Instantiate(item, transform.position, Quaternion.identity));
        }
    }

    public bool CanAddResource(Resource resource){
        if (items.Count >= maxItems) {
            return false;
        }
        if (acceptedResourceTypes.Length > 0){
            return Array.Exists(acceptedResourceTypes, type => type == resource.GetResourceType());
        }
        if (acceptedResourceTypesExactCount.Length > 0){
            int count = 0;
            foreach (Resource item in items) {
                if (item.GetResourceType() == resource.GetResourceType()) {
                    count++;
                }
            }
            int allowedCount = acceptedResourceTypesExactCount.Count(type => type == resource.GetResourceType());
            return count < allowedCount;
        }
        return items.Count < maxItems;
    }

    public bool AddResource(Resource resource, bool worldPositionStays = false) {
        if (!CanAddResource(resource)) {
            return false;
        }
        items.Add(resource);

        resource.transform.SetParent(transform, worldPositionStays);
        resource.OnAddedToInventory(this);

        OnContentsChanged?.Invoke();
        return true;
    }

    public Resource RemoveResource(Resource resource) {
        items.Remove(resource);

        resource.transform.SetParent(null);
        resource.OnRemovedFromInventory(this);

        OnContentsChanged?.Invoke();
        return resource;
    }

    public Resource RemoveResourceOfType(ResourceType type) {
        foreach (Resource resource in items) {
            if (resource.GetResourceType() == type) {
                return RemoveResource(resource);
            }
        }
        return null;
    }
}
