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
        for (int i = 0; i < 3; i++)
        {
            int randomNum = Random.Range(0, portalSpawnPoint.Length);
            Instantiate(portal, portalSpawnPoint[randomNum].transform.position, portalSpawnPoint[randomNum].transform.rotation);
        }
    }
}
