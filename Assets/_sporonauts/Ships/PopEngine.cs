using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEngine : MonoBehaviour, IShipComponent
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private float inflateTime = 1f;
    [SerializeField] private float inflateScale = 2f;
    [SerializeField] private float popForce = 10f;
    [SerializeField] private GameObject balloon;

    private Coroutine inflateCoroutine;

    private void Awake() {
        inventory.OnContentsChanged += ToggleBalloon;
        ToggleBalloon();
    }

    public void Activate() {
        if (inventory.NumItems() > 0) {
            inflateCoroutine = StartCoroutine(Inflate());
        }
    }

    public void Deactivate() {
        if (inflateCoroutine == null) {
            return;
        }

        StopCoroutine(inflateCoroutine);
        inflateCoroutine = null;

        Resource fuel = inventory.GetContents()[0];
        inventory.RemoveResource(fuel);
        Destroy(fuel.gameObject);

        float inflatePercent = (balloon.transform.localScale.x - 1) / (inflateScale - 1);
        Vector2 direction = -transform.up;
        GetComponentInParent<Rigidbody2D>().AddForce(direction * popForce * inflatePercent, ForceMode2D.Impulse);
    }

    private IEnumerator Inflate()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Min(timer, inflateTime);
            balloon.transform.localScale = 
                        Vector3.one * Mathf.Lerp(1f, inflateScale, timer / inflateTime);
            yield return null;
        }
    }

    private void ToggleBalloon() {
        balloon.SetActive(inventory.NumItems() > 0);
        if (balloon.activeSelf) {
            inventory.GetContents()[0].GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        }
    }
}
