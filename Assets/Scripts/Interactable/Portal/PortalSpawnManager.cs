using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnManager : MonoBehaviour
{
    public GameObject portal;
    public Transform[] portalSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        int randomNum1 = Random.Range(0,portalSpawnPoint.Length);
        int randomNum2 = Random.Range(0, portalSpawnPoint.Length);
        int randomNum3 = Random.Range(0, portalSpawnPoint.Length);
        while (randomNum1 == randomNum2 || randomNum2 == randomNum3 || randomNum3==randomNum1)
        {
            if(randomNum1 == randomNum2)
            {
                randomNum2 = Random.Range(0, portalSpawnPoint.Length);
            }
            else if(randomNum1 == randomNum3)
            {
                randomNum3 = Random.Range(0, portalSpawnPoint.Length);
            }
            else if( randomNum2 == randomNum3)
            {
                randomNum3 = Random.Range(0, portalSpawnPoint.Length);
            }
            else
            {
                randomNum2 = Random.Range(0, portalSpawnPoint.Length);
                randomNum3 = Random.Range(0, portalSpawnPoint.Length);
            }
        }
        Instantiate(portal, portalSpawnPoint[randomNum1].transform.position, portalSpawnPoint[randomNum1].transform.rotation);
        Instantiate(portal, portalSpawnPoint[randomNum2].transform.position, portalSpawnPoint[randomNum2].transform.rotation);
        Instantiate(portal, portalSpawnPoint[randomNum3].transform.position, portalSpawnPoint[randomNum3].transform.rotation);
    }
}
