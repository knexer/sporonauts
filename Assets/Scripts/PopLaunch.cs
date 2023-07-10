using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On click and hold, inflate the balloon
// On release, pop the balloon and generate an impulse
public class PopLaunch : MonoBehaviour
{
    [SerializeField] private GameObject balloon;
    [SerializeField] private Rigidbody2D ship;
    [SerializeField] private float inflateTime = 1f;
    [SerializeField] private float inflateScale = 2f;
    [SerializeField] private float popForce = 10f;
    
    private bool isInflating = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isInflating = true;
            StartCoroutine(Inflate());
        }
    }

    private IEnumerator Inflate()
    {
        float timer = 0f;
        while (isInflating && balloon != null)
        {
            timer += Time.deltaTime;
            timer = Mathf.Min(timer, inflateTime);
            balloon.transform.localScale = Vector3.one * Mathf.Lerp(1f, inflateScale, timer / inflateTime);

            if (Input.GetMouseButtonUp(0))
            {
                isInflating = false;
                Pop(timer / inflateTime);
                yield break;
            }
            yield return null;
        }
    }

    private void Pop(float inflatePercent)
    {
        if (balloon == null) return;

        // Launch the ship in this object's local down direction
        Vector2 direction = -transform.up;
        ship.AddForce(direction * popForce * inflatePercent, ForceMode2D.Impulse);
        Destroy(balloon);
    }
}
