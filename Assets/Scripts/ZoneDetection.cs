using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float zoneRadius = 10f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float checkInterval = 0.5f;
    [SerializeField] private Transform[] zoneCenters; // Zonas para realizar la detección
    [SerializeField] NavMeshController navMeshController;

    private float timeCheck;
    private bool playerDetected = false;

    private void Update()
    {
        timeCheck += Time.deltaTime;

        if (timeCheck >= checkInterval)
        {
            bool detected = false;

            foreach (Transform zoneCenter in zoneCenters)
            {
                if (Vector3.Distance(transform.position, zoneCenter.position) < detectionRadius)
                {   
                    detected = DetectPlayer(zoneCenter.position);
                    if (detected)
                    {
                        // Detener la comprobación si ya se detectó al jugador
                        break; 
                    }
                }
            }

            //Jugador no detectado
            if (playerDetected && !detected)
            {
                playerDetected = false;
                if (navMeshController)
                {
                    navMeshController.PlayerDetected(false);
                }
            }

            timeCheck = 0f;
        }


    }

    bool DetectPlayer(Vector3 zoneCenter)
    {
        Collider[] hitColliders = Physics.OverlapSphere(zoneCenter, zoneRadius, playerMask);

        foreach (Collider collider in hitColliders)
        {
            if (collider.transform == player)
            {
                Debug.Log("Jugador detectado en zona");

                if (!playerDetected)
                {
                    playerDetected = true;

                    if(navMeshController != null)
                    {
                        navMeshController.PlayerDetected(true);
                    }
                }
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
}
