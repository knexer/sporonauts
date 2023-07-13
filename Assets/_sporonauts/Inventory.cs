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

    public bool AddResource(Resource resource) {
        if (items.Count >= maxItems) {
            return false;
        }
        items.Add(resource);

        resource.transform.SetParent(transform);

        SpringJoint2D joint = resource.gameObject.AddComponent<SpringJoint2D>();
        joint.frequency = 10f;
        joint.dampingRatio = .9f;

        joint.autoConfigureConnectedAnchor = true;
        joint.connectedBody = GetComponentInParent<Rigidbody2D>();
        joint.anchor = Vector2.zero;
        joint.autoConfigureDistance = false;
        joint.autoConfigureConnectedAnchor = false;

        OnContentsChanged?.Invoke();
        return true;
    }

    public void RemoveResource(Resource resource) {
        items.Remove(resource);

        // Remove the FixedJoint2D from the resource.
        resource.transform.SetParent(null);
        SpringJoint2D joint = resource.GetComponent<SpringJoint2D>();
        if (joint) {
            Destroy(joint);
        }

        OnContentsChanged?.Invoke();
    }
}
