using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] public float radius = 1f;

    static public List<Planet> planets = new List<Planet>();

    public static Vector2 CalculateNetGravity(Vector2 position) {
        Vector2 gravity = Vector2.zero;
        foreach (Planet planet in planets) {
            gravity += planet.CalculateGravity(position);
        }
        return gravity;
    }

    public Vector2 CalculateGravity(Vector2 position) {
        Vector2 direction = (Vector2)transform.position - position;
        float distance = direction.magnitude;
        float gravity = this.gravity * radius / (distance * distance);
        return direction.normalized * gravity;
    }

    public static (float distance, Planet planet) GetClosestPlanetCenter(Vector2 position) {
        return planets.Select(planet => (distance: Vector2.Distance(position, planet.transform.position), planet)).Min();
    }

    public static (float distance, Planet planet) GetClosestPlanetSurface(Vector2 position) {
        return planets.Select(planet => (distance: Vector2.Distance(position, planet.transform.position) - planet.radius, planet)).Min();
    }

    private void Awake() {
        planets.Add(this);
    }

    private void OnDestroy() {
        planets.Remove(this);
    }
}
