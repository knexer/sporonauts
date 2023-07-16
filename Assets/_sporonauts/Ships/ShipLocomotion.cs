using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// While landed, the ship can go left and right with player input.
// When there is no input, the ship will come to a stop.
public class ShipLocomotion : MonoBehaviour
{
    [SerializeField] private float landedDistanceThreshold;
    [SerializeField] private float stopThresholdVelocity;
    [SerializeField] private float slowThresholdVelocity;
    [SerializeField] private float moveForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody2D shipBody;
    private bool clockwise = false;
    private bool antiClockwise = false;

    private bool IsLanded() {
        return Planet.GetClosestPlanetSurface(transform.position).distance < landedDistanceThreshold;
    }

    private void FixedUpdate() {
        if (!IsLanded()){
            return;
        }

        if (!clockwise && !antiClockwise) {
            if (shipBody.velocity.magnitude < stopThresholdVelocity) {
                shipBody.velocity = Vector2.zero;
            } else if (shipBody.velocity.magnitude < slowThresholdVelocity) {
                shipBody.AddForce(-shipBody.velocity.normalized * moveForce);
            }
            return;
        }

        if (shipBody.velocity.magnitude > maxSpeed) {
            return;
        }

        (float _, Planet planet) = Planet.GetClosestPlanetSurface(transform.position);
        Vector2 towardsPlanet = (planet.transform.position - transform.position).normalized;
        Vector2 clockwiseAroundPlanet = new Vector2(-towardsPlanet.y, towardsPlanet.x);

        if (clockwise) {
            shipBody.AddForce(clockwiseAroundPlanet * moveForce);
        }

        if (antiClockwise) {
            shipBody.AddForce(-clockwiseAroundPlanet * moveForce);
        }
    }

    public void OnWalkClockwise(InputAction.CallbackContext context) {
        clockwise = context.ReadValueAsButton();
    }

    public void OnWalkAntiClockwise(InputAction.CallbackContext context) {
        antiClockwise = context.ReadValueAsButton();
    }
}
