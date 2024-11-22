using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [System.Serializable]
    public class TargetPrefabData
    {
        public GameObject prefab; // Prefab ของเป้าหมาย
    }

    public TargetPrefabData[] targetPrefabs; // เก็บ Prefab หลายชนิด
    private Dictionary<GameObject, List<Vector3>> prefabPositions = new Dictionary<GameObject, List<Vector3>>();

    void Start()
    {
        // บันทึกตำแหน่งเริ่มต้นของเป้าหมาย
        foreach (var prefabData in targetPrefabs)
        {
            prefabPositions[prefabData.prefab] = new List<Vector3>();
        }

        // ค้นหาเป้าหมายทั้งหมดในฉากตาม Tag "Target"
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            foreach (var prefabData in targetPrefabs)
            {
                if (target.name.StartsWith(prefabData.prefab.name)) // ใช้ StartsWith แทน Contains
                {
                    prefabPositions[prefabData.prefab].Add(target.transform.position);
                    Debug.Log($"Saved position {target.transform.position} for prefab {prefabData.prefab.name}");
                    break;
                }
            }
        }

        Debug.Log("Saved original positions for all targets.");
    }


    public void ResetTargets()
    {
        Debug.Log("ResetTargets called.");

        // ลบเป้าหมายทั้งหมด
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        Debug.Log($"Found {targets.Length} targets to destroy.");
        foreach (GameObject target in targets)
        {
            Debug.Log($"Destroying target: {target.name} at {target.transform.position}");
            Destroy(target);
        }

        // สร้างเป้าหมายใหม่จากตำแหน่งที่บันทึกไว้
        foreach (var entry in prefabPositions)
        {
            GameObject prefab = entry.Key;
            List<Vector3> positions = entry.Value;

            foreach (Vector3 position in positions)
            {
                GameObject newTarget = Instantiate(prefab, position, Quaternion.identity);
                newTarget.tag = "Target"; // ตั้งค่า Tag อีกครั้ง
                Debug.Log($"Instantiated new target: {newTarget.name} at {position}");
            }
        }

        // ตรวจสอบจำนวนเป้าหมายหลังรีเซ็ต
        GameObject[] newTargets = GameObject.FindGameObjectsWithTag("Target");
        Debug.Log($"New targets count after reset: {newTargets.Length}");
    }

}
