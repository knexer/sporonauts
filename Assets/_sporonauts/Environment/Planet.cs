using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] public float radius = 1f;
    [SerializeField] private GameObject[] surfaceObjectPrefabs;

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
        float gravity = this.gravity / (distance * distance);
        return direction.normalized * gravity;
    }

    public static (float distance, Planet planet) GetClosestPlanetCenter(Vector2 position) {
        return planets.Select(planet => (distance: Vector2.Distance(position, planet.transform.position), planet)).Min();
    }

    public static (float distance, Planet planet) GetClosestPlanetSurface(Vector2 position) {
        return planets.Select(planet => (distance: Vector2.Distance(position, planet.transform.position) - planet.radius, planet)).Min();
    }

    private void OnEnable() {
        planets.Add(this);
    }

    private void OnDestroy() {
        planets.Remove(this);
    }

    private void Awake() {
        SpawnSurfaceObjects();
    }

    private void SpawnSurfaceObjects() {
        // Spawn surface objects randomly. Don't let them get too close together.
        int numObjects = surfaceObjectPrefabs.Length;
        // Reserve half the circumference for minimum spacing between objects.
        float minRadiansBetweenObjects = Mathf.PI / numObjects;

        // Randomly partition the remaining half of the circumference.
        float[] offsets = new float[numObjects];
        for (int i = 0; i < numObjects; i++) {
            offsets[i] = Random.Range(0, Mathf.PI);
        }
        offsets = offsets.OrderBy(offset => offset).ToArray();

        for (int i = 0; i < numObjects; i++) {
            float angle = offsets[i] + minRadiansBetweenObjects * i;
            Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            GameObject spawnedObject = Instantiate(surfaceObjectPrefabs[i], transform);
            spawnedObject.transform.localPosition = position;
            spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        }
    }
}
