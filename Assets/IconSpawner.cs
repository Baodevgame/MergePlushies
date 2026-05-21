using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> iconList;
    [SerializeField] private List<float> spawnRates;
    [SerializeField] private Transform spawnPos;

    private void Start()
    {
        SpawnRandomIcon();
    }

    public void SpawnRandomIcon()
    {
        if (spawnPos == null || iconList.Count == 0)
        {
            Debug.LogWarning("spawnPos hoac iconList null");
            return;
        }

        GameObject prefab = GetRandomByRate();
        GameObject obj = MyPoolManager.Instance.Get(prefab, spawnPos.position);

        IconCtrl ctrl = obj.GetComponent<IconCtrl>();
        if (ctrl == null)
            ctrl = obj.AddComponent<IconCtrl>();

        ctrl.isReleased = false;
        ctrl.onDropCallback = () =>
        {
            StartCoroutine(SpawnAfterDelay(1f));
        };

        // Reset merge
        IconMerge merge = obj.GetComponent<IconMerge>();
        if (merge != null)
        {
            merge.isMerging = false;
            merge.enabled = true;
            merge.isPlaced = false;
        }

        // Reset Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private IEnumerator SpawnAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnRandomIcon();
    }

    private GameObject GetRandomByRate()
    {
        float total = 0;
        for (int i = 0; i < spawnRates.Count; i++)
            total += spawnRates[i];

        float rand = Random.Range(0, total);
        float curr = 0;

        for (int i = 0; i < iconList.Count; i++)
        {
            curr += spawnRates[i];
            if (rand <= curr) return iconList[i];
        }

        return iconList[iconList.Count - 1];
    }
}
