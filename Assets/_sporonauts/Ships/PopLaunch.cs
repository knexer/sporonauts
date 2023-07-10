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
    [SerializeField] private int startingFuel = 1;

    private int fuel;
    private int Fuel
    {
        get => fuel;
        set
        {
            fuel = value;
            if (fuel < 0) fuel = 0;
            if (fuel > balloons.Length) fuel = balloons.Length;

            for (int i = 0; i < balloons.Length; i++)
            {
                balloons[i].SetActive(i < fuel);
            }
        }
    }

    //Reference to the refuel coroutine
    private Coroutine refuelCoroutine;

    public int NumBalloons() {
        // Return number of active balloons
        return balloons.Count(balloon => balloon.activeSelf);
    }

    private void Start() {
        GetComponentInParent<CollisionDetector>().OnTriggerEnter2DEvent += OnTriggerEnter2D;
        Fuel = startingFuel;
    }

    private void OnDestroy() {
        var detector = GetComponentInParent<CollisionDetector>();
        if (detector) detector.OnTriggerEnter2DEvent -= OnTriggerEnter2D;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && NumBalloons() > 0)
        {
            StartCoroutine(Inflate());
        }
    }

    private IEnumerator Inflate()
    {
        float timer = 0f;
        while (!Input.GetMouseButtonUp(0))
        {
            timer += Time.deltaTime;
            timer = Mathf.Min(timer, inflateTime);
            for (int i = 0; i < balloons.Length; i++)
            {
                balloons[i].transform.localScale = i == Fuel - 1 ?
                        Vector3.one * Mathf.Lerp(1f, inflateScale, timer / inflateTime) :
                        Vector3.one;
            }
            yield return null;
        }

        Pop(timer / inflateTime);
    }

    private void Pop(float inflatePercent)
    {
        Vector2 direction = -transform.up;
        ship.AddForce(direction * popForce * inflatePercent, ForceMode2D.Impulse);

        GameObject balloon = balloons[Fuel - 1];
        balloon.transform.localScale = Vector3.one;
        Fuel--;
    }
    
    // Refuel when near a ResourceDeposit
    private void OnTriggerEnter2D(Collider2D other)
    {
        ResourceDeposit resourceDeposit = other.gameObject.GetComponent<ResourceDeposit>();
        if (!resourceDeposit) return;

        if (refuelCoroutine == null) refuelCoroutine = StartCoroutine(Refuel(other));
    }

    private IEnumerator Refuel(Collider2D resourceDeposit)
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        Collider2D[] colliders = rb.GetComponentsInChildren<Collider2D>();

        while (Enumerable.Any(colliders, collider => collider.IsTouching(resourceDeposit)))
        {
            while (Fuel >= balloons.Length) yield return null;

            Debug.Log("Refueling");
            float timer = 0f;
            while (timer < refuelTimePerBalloon)
            {
                timer += Time.deltaTime;
                yield return null;

                // If we stop colliding with the ResourceDeposit, stop refueling
                if (!Enumerable.Any(colliders, collider => collider.IsTouching(resourceDeposit))) {
                    Debug.Log("Stopped refueling - left deposit.");
                    refuelCoroutine = null;
                    yield break;
                }
            }
            timer -= refuelTimePerBalloon;
            Fuel++;
        }
        Debug.Log("Finished refueling");
        refuelCoroutine = null;
    }
}
