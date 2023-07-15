using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocuser : MonoBehaviour
{
    [SerializeField] private Transform cameraDefaultFollowTarget;
    [SerializeField] private Camera controlledCamera;
    private float defaultZoom;
    private Transform target;
    private float zoom;
    private Vector3 velocity = Vector3.zero;
    private Coroutine zoomCoroutine;

    private void Awake() {
        target = cameraDefaultFollowTarget;
        defaultZoom = controlledCamera.orthographicSize;
    }

    private void Update() {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, .1f);
        // Rotate based on the local direction of gravity
        Vector2 gravityDirection = Planet.CalculateNetGravity(transform.position);
        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    public void Focus(GameObject target, float zoom) {
        this.target = target.transform;
        this.zoom = zoom;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(Zoom());
    }

    public void Unfocus() {
        this.target = cameraDefaultFollowTarget;
        this.zoom = defaultZoom;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(Zoom());
    }

    private IEnumerator Zoom() {
        float startZoom = controlledCamera.orthographicSize;
        Vector2 startPosition = transform.position;
        while (true) {
            // Zoom in based on how close we are to the target
            float distanceCoveredRatio = 1 - Vector2.Distance(transform.position, target.position) / Vector2.Distance(startPosition, target.position);
            controlledCamera.orthographicSize = Mathf.Lerp(startZoom, zoom, distanceCoveredRatio);
            if (distanceCoveredRatio >= .99f) {
                break;
            }
            yield return null;
        }

        controlledCamera.orthographicSize = zoom;
        zoomCoroutine = null;
    }
}
