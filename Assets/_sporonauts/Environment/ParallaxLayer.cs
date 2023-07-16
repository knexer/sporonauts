using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxFactor;
    [SerializeField] private GameObject star;
    [SerializeField] private int numStarsAtMaxDistance = 100;
    [SerializeField] private float maxDistance = 10f;

    private Vector2 lastCameraPosition;

    private void Start() {
        lastCameraPosition = Camera.main.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, parallaxFactor);

        SpawnStars();
    }

    private void SpawnStars() {
        int numStars = (int)(numStarsAtMaxDistance);
        for (int i = 0; i < numStars; i++) {
            Vector3 position = new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance), parallaxFactor);
            GameObject spawnedStar = Instantiate(star, position, Quaternion.identity, transform);
            SpriteRenderer spriteRenderer = spawnedStar.GetComponentInChildren<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = Random.Range(0.1f, 1.0f);
            spriteRenderer.color = color;
        }
    }

    private void Update() {
        Vector2 cameraPosition = Camera.main.transform.position;
        Vector2 delta = cameraPosition - lastCameraPosition;
        transform.position += (Vector3)delta * parallaxFactor;
        lastCameraPosition = cameraPosition;

        // Wrap each star around once it's too far from the camera
        foreach (Transform child in transform) {
            // X wrap
            if (child.position.x > cameraPosition.x + maxDistance) {
                child.position -= new Vector3(2 * maxDistance, 0, parallaxFactor);
            } else if (child.position.x < cameraPosition.x - maxDistance) {
                child.position += new Vector3(2 * maxDistance, 0, parallaxFactor);
            }
            // Y wrap
            if (child.position.y > cameraPosition.y + maxDistance) {
                child.position -= new Vector3(0, 2 * maxDistance, parallaxFactor);
            } else if (child.position.y < cameraPosition.y - maxDistance) {
                child.position += new Vector3(0, 2 * maxDistance, parallaxFactor);
            }
        }

    }
}
