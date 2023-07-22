using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocuser : MonoBehaviour
{
    [SerializeField] private CameraTarget cameraDefaultFollowTarget;
    [SerializeField] private Camera controlledCamera;
    [SerializeField] private float smoothTime = .1f;
    private CameraTarget target;
    private Vector3 velocity = Vector3.zero;
    private float angleVelocity = 0;
    private float zoomVelocity = 0;
    private Coroutine zoomCoroutine;

    private void Awake() {
        target = cameraDefaultFollowTarget;
        controlledCamera.orthographicSize = target.CameraOrthographicSize;
        transform.position = target.transform.position;
    }

    public void Focus(CameraTarget target) {
        this.target = target;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        // zoomCoroutine = StartCoroutine(Zoom());
    }

    public void Unfocus() {
        this.target = cameraDefaultFollowTarget;
        if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
        // zoomCoroutine = StartCoroutine(Zoom());
    }

    private void Update() {
        Vector3 targetPosition = target.transform.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        float targetAngle = target.transform.rotation.eulerAngles.z;
        if (target.OrientToLocalGravity) {
            // Rotate to local 'up' based on target's gravity.
            Vector2 gravityDirection = Planet.CalculateNetGravity(target.transform.position);
            targetAngle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg + 90;
        }
        transform.rotation = Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, targetAngle, ref angleVelocity, smoothTime));

        float targetZoom = target.CameraOrthographicSize;
        controlledCamera.orthographicSize = Mathf.SmoothDamp(controlledCamera.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
    }
}
