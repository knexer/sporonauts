using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocuser : MonoBehaviour
{
    [SerializeField] private Transform cameraDefaultFollowTarget;
    [SerializeField] private Camera controlledCamera;
    private float defaultZoom;
    private Transform target;
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
    }

    public void Focus(GameObject target) {
        this.target = target.transform;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(Zoom());
    }

    public void Unfocus() {
        this.target = cameraDefaultFollowTarget;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(Zoom());
    }

    private IEnumerator Zoom() {
        float startZoom = controlledCamera.orthographicSize;
        Vector2 startPosition = transform.position;
        // TODO zoom to non-default targets based on their size
        float endZoom = target == cameraDefaultFollowTarget ? defaultZoom : defaultZoom / 2f;
        while (true) {
            // Zoom in based on how close we are to the target
            float distanceCoveredRatio = 1 - Vector2.Distance(transform.position, target.position) / Vector2.Distance(startPosition, target.position);
            controlledCamera.orthographicSize = Mathf.Lerp(startZoom, endZoom, distanceCoveredRatio);
            if (distanceCoveredRatio >= .99f) {
                break;
            }
            yield return null;
        }

        controlledCamera.orthographicSize = endZoom;
        zoomCoroutine = null;
    }
}
