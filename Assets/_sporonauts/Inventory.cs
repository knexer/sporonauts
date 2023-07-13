using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 1;
    private List<Resource> items = new List<Resource>();

    public bool AddResource(Resource resource) {
        if (items.Count >= maxItems) {
            return false;
        }
        items.Add(resource);

        resource.transform.SetParent(transform);
        FixedJoint2D joint = resource.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = GetComponentInParent<Rigidbody2D>();
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = Vector2.zero;

        return true;
    }

    public void RemoveResource(Resource resource) {
        items.Remove(resource);

        // Remove the FixedJoint2D from the resource.
        resource.transform.SetParent(null);
        FixedJoint2D joint = resource.GetComponent<FixedJoint2D>();
        if (joint) {
            Destroy(joint);
        }
    }
}
