using UnityEngine;
using UnityEngine.AI;

public class NavMeshChecker : MonoBehaviour
{
    void Start()
    {
        if (HasNavMesh())
        {
            Debug.Log("✅ يوجد NavMesh في المشهد");
        }
        else
        {
            Debug.Log("❌ لا يوجد NavMesh في المشهد");
        }
    }

    bool HasNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        return navMeshData.indices != null && navMeshData.indices.Length > 0;
    }
}
