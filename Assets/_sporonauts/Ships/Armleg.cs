using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Armleg : MonoBehaviour
{
    [SerializeField] private GameObject HandFoot;
    [SerializeField] private GameObject ArmLeg;
    [SerializeField] private float ArmLegScaleFactor = 10f;
    private Vector2 restPosition;
    private Transform rangeOrigin;
    private float maxRange;

    private Vector2 targetScreenPosition = Vector2.zero;
    private Coroutine trackMouseRoutine = null;

    private void Start() {
        restPosition = HandFoot.transform.localPosition;
        SetHandFootPosition(restPosition);
    }

    public void SetRange(DragAndDrop dragAndDrop, float maxRange) {
        this.maxRange = maxRange;
        rangeOrigin = dragAndDrop.transform;
    }
    
    private void SetHandFootPosition(Vector2 localPosition) {
        // Clamp to max range (in range origin space)
        Vector2 rangeOriginLocalPosition = rangeOrigin.InverseTransformPoint(transform.TransformPoint(localPosition));
        rangeOriginLocalPosition = Vector2.ClampMagnitude(rangeOriginLocalPosition, maxRange);
        localPosition = transform.InverseTransformPoint(rangeOrigin.TransformPoint(rangeOriginLocalPosition));
        Quaternion localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, localPosition));


        HandFoot.transform.localPosition = localPosition;
        HandFoot.transform.localRotation = localRotation;
        ArmLeg.transform.localPosition = localPosition / 2;
        ArmLeg.transform.localRotation = localRotation;
        ArmLeg.transform.localScale = new Vector3(1, localPosition.magnitude / 2 * ArmLegScaleFactor, 1);
    }

    public void TrackMouse(InputAction.CallbackContext context) {
        targetScreenPosition = context.ReadValue<Vector2>();
    }

    public void Activate() {
        trackMouseRoutine = StartCoroutine(TrackMouse());
    }

    public void Deactivate() {
        if (trackMouseRoutine == null) {
            return;
        }
        StopCoroutine(trackMouseRoutine);
        trackMouseRoutine = null;

        // Return to rest position
        SetHandFootPosition(restPosition);
    }

    private IEnumerator TrackMouse() {
        while (true) {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(targetScreenPosition);
            SetHandFootPosition(transform.InverseTransformPoint(worldPosition));
            yield return null;
        }
    }
}
