using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arm : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject Limb;
    [SerializeField] private float ArmScaleFactor = 10f;
    private Vector2 restPosition;
    private Transform rangeOrigin;
    private float maxRange;

    private Vector2 targetScreenPosition = Vector2.zero;
    private Coroutine trackMouseRoutine = null;

    private void Start() {
        restPosition = Hand.transform.localPosition;
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


        Hand.transform.localRotation = localRotation;
        Hand.transform.localPosition = localPosition;
        Limb.transform.localPosition = localPosition / 2;
        Limb.transform.localRotation = localRotation;
        Limb.transform.localScale = new Vector3(1, localPosition.magnitude / 2 * ArmScaleFactor, 1);
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
