using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    Fuel
}

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType type;
}
