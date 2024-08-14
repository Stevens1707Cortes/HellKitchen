using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float zoneRadius = 10f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float checkInterval = 0.5f;

    [SerializeField] private Transform[] zoneCenters; // Zonas para realizar la detección
    [SerializeField] private string[] zoneTags; 
    [SerializeField] private NavMeshController[] navMeshControllers; 

    private float timeCheck;
    private string detectedZoneTag = "";

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        timeCheck += Time.deltaTime;

        if (timeCheck >= checkInterval)
        {
            bool detected = false;
            string newDetectedZoneTag = "";

            for (int i = 0; i < zoneCenters.Length; i++)
            {
                if (Vector3.Distance(transform.position, zoneCenters[i].position) < detectionRadius)
                {
                    if (DetectPlayer(zoneCenters[i].position, zoneTags[i]))
                    {
                        detected = true;
                        newDetectedZoneTag = zoneTags[i];
                        break;
                    }
                }
            }

            // Si se detectó al jugador en una nueva zona, notificar al NavMeshController
            if (detected && detectedZoneTag != newDetectedZoneTag)
            {
                detectedZoneTag = newDetectedZoneTag;
                NotifyNavMeshControllers(true, detectedZoneTag);
            }
            // Si el jugador fue detectado pero ya no está en ninguna zona, notificar al NavMeshController
            else if (!detected && detectedZoneTag != "")
            {
                detectedZoneTag = "";
                NotifyNavMeshControllers(false);
            }

            timeCheck = 0f;
        }


    }

    bool DetectPlayer(Vector3 zoneCenter, string zoneTag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(zoneCenter, zoneRadius, playerMask);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player") && collider.transform == player)
            {
                //Debug.Log("Jugador detectado en zona");

                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        // Dibuja esferas en cada zona de detección
        Gizmos.color = Color.blue; 

        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        foreach (Transform zoneCenter in zoneCenters)
        {
            Gizmos.DrawWireSphere(zoneCenter.position, zoneRadius);
        }
    }

    void NotifyNavMeshControllers(bool detected, string zoneTag = "")
    {
        foreach (var controller in navMeshControllers)
        {
            controller.PlayerDetected(detected, zoneTag);
        }
    }
}
