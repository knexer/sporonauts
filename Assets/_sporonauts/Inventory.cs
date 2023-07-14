using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 1;
    [SerializeField] private Resource[] startingItemPrefabs;

    private List<Resource> items = new List<Resource>();

    public int NumItems() {
        return items.Count;
    }

    public ReadOnlyCollection<Resource> GetContents() {
        return items.AsReadOnly();
    }

    public event System.Action OnContentsChanged;

    private void Awake() {
        foreach (Resource item in startingItemPrefabs) {
            AddResource(Instantiate(item, transform.position, Quaternion.identity));
        }
    }

    public bool CanAddResource(Resource resource){
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

    public void RemoveResource(Resource resource) {
        items.Remove(resource);

        resource.transform.SetParent(null);
        resource.OnRemovedFromInventory(this);

        OnContentsChanged?.Invoke();
    }
}
