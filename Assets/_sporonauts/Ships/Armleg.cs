using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armleg : MonoBehaviour
{
    [SerializeField] private GameObject HandFoot;
    [SerializeField] private GameObject ArmLeg;
    [SerializeField] private float ArmLegScaleFactor = 10f;

    private void Awake() {
        SetHandFootPosition(HandFoot.transform.localPosition);
    }
    
    private void SetHandFootPosition(Vector2 localPosition) {
        HandFoot.transform.localPosition = localPosition;
        ArmLeg.transform.localPosition = localPosition / 2;
        ArmLeg.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, localPosition));
        ArmLeg.transform.localScale = new Vector3(1, localPosition.magnitude / 2 * ArmLegScaleFactor, 1);
    }
}
