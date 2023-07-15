using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EngineMount : MonoBehaviour
{
    private Vector2 targetScreenPosition = Vector2.zero;
    private Coroutine pointAtMouseRoutine = null;

    public void PointAtMouse(InputAction.CallbackContext context) {
        targetScreenPosition = context.ReadValue<Vector2>();
    }

    public void Activate() {
        pointAtMouseRoutine = StartCoroutine(PointAtMouse());
    }

    public void Deactivate() {
        if (pointAtMouseRoutine == null) {
            return;
        }
        StopCoroutine(pointAtMouseRoutine);
        pointAtMouseRoutine = null;

        // Return to default rotation
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator PointAtMouse() {
        while (true) {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(targetScreenPosition);
            Vector2 direction = worldPosition - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            yield return null;
        }
    }
}
