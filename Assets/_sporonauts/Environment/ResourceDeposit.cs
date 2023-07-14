using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private GameObject resourcePrefab;
    public GameObject MakeResource() {
        return Instantiate(resourcePrefab, transform.position, Quaternion.identity);
    }
}
