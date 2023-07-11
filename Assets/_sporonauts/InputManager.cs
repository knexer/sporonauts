using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles top level game state, routing input to the appropriate object
// Overall states:
// - Controlling the ship
// - Managing inventory for a ship
// - Selecting a ship

// So, suppose the base state is 'nothing is selected'.
// Then we can select a ship, and the state becomes 'ship is selected'.
// Input is routed to the ship, and the ship can be controlled.
// The ship can swap modes, and the state becomes 'ship is in inventory mode'.

// This naturally decomposes into two parts:
// - InputManager, which routes input to the selected object
// - Ship, which handles the state of the ship
public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
