using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaintainScatter : MonoBehaviour
{
    [SerializeField] private GameObject scatterPrefab;
    [SerializeField] private int numScatter = 10;
    [SerializeField] private float scatterIntervalSeconds = 5f;

    private Planet planet;

    private void Awake() {
        planet = GetComponent<Planet>();
        ScatterAll();
        StartCoroutine(ScatterCoro());
    }

    private void ScatterAll() {
        for (int i = 0; i < numScatter; i++) {
            Scatter();
        }
    }

    private IEnumerator ScatterCoro() {
        while (true) {
            yield return new WaitForSeconds(scatterIntervalSeconds);
            if (gameObject.GetComponentsInChildren<Scattered>().Length < numScatter){
                Scatter();
            }
        }
    }

    private void Scatter() {
        Vector2 position = Random.insideUnitCircle.normalized * planet.radius;
        GameObject scatter = Instantiate(scatterPrefab, transform, false);
        scatter.AddComponent<Scattered>();
        scatter.transform.localPosition = position;
        scatter.transform.localRotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, position));
    }

    private class Scattered : MonoBehaviour {}
}
