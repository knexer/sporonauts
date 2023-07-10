using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// On click and hold, inflate the balloon
// On release, pop the balloon and generate an impulse
public class PopLaunch : MonoBehaviour
{
    [SerializeField] private GameObject[] balloons;
    [SerializeField] private Rigidbody2D ship;
    [SerializeField] private float inflateTime = 1f;
    [SerializeField] private float inflateScale = 2f;
    [SerializeField] private float popForce = 10f;
    [SerializeField] private float refuelTimePerBalloon = 1f;

    public int NumBalloons() {
        // Return number of active balloons
        return balloons.Count(balloon => balloon.activeSelf);
    }

    private void Start() {
        // Subscribe to OnTriggerEnter2DEvent
        GetComponentInParent<CollisionDetector>().OnTriggerEnter2DEvent += OnTriggerEnter2D;
    }

    private void OnDestroy() {
        // Unsubscribe from OnTriggerEnter2DEvent
        GetComponentInParent<CollisionDetector>().OnTriggerEnter2DEvent -= OnTriggerEnter2D;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && NumBalloons() > 0)
        {
            StartCoroutine(Inflate(balloons[NumBalloons() - 1]));
        }
    }

    private IEnumerator Inflate(GameObject balloon)
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Min(timer, inflateTime);
            balloon.transform.localScale = Vector3.one * Mathf.Lerp(1f, inflateScale, timer / inflateTime);

            if (Input.GetMouseButtonUp(0))
            {
                Pop(balloon, timer / inflateTime);
                yield break;
            }
            yield return null;
        }
    }

    private void Pop(GameObject balloon, float inflatePercent)
    {
        Vector2 direction = -transform.up;
        ship.AddForce(direction * popForce * inflatePercent, ForceMode2D.Impulse);
        balloon.SetActive(false);
    }
    
    // Refuel when near a ResourceDeposit
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger");
        ResourceDeposit resourceDeposit = other.gameObject.GetComponent<ResourceDeposit>();
        if (!resourceDeposit) return;

        StartCoroutine(Refuel(other));
    }

    private IEnumerator Refuel(Collider2D resourceDeposit)
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        Collider2D[] colliders = rb.GetComponentsInChildren<Collider2D>();

        Debug.Log("Refueling");
        float timer = 0f;
        while (NumBalloons() < balloons.Length)
        {
            while (timer < refuelTimePerBalloon)
            {
                timer += Time.deltaTime;
                yield return null;

                // If we stop colliding with the ResourceDeposit, stop refueling
                if (!Enumerable.Any(colliders, collider => collider.IsTouching(resourceDeposit))) {
                    Debug.Log("Stopped refueling - left deposit.");
                    yield break;
                }
            }
            timer -= refuelTimePerBalloon;
            balloons[NumBalloons()].SetActive(true);
        }
        Debug.Log("Finished refueling");
    }
}
