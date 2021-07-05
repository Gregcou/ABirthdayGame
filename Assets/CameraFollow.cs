using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public GameObject tomatoPrefab;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("A");
        StartCoroutine(spawnTomato());
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -5);
        
    }

    private IEnumerator spawnTomato()
    {
        while(true)
        {
            yield return new WaitForSeconds(4);
            if (Random.Range(1,3) == 1)
            {
                Instantiate(tomatoPrefab, new Vector3(25, 12, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(tomatoPrefab, new Vector3(30, 15, 0), Quaternion.identity);
            }

            if (Random.Range(1, 3) == 1)
            {
                Instantiate(tomatoPrefab, new Vector3(40, 12, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(tomatoPrefab, new Vector3(50, 15, 0), Quaternion.identity);
            }

            if (Random.Range(1, 3) == 1)
            {
                Instantiate(tomatoPrefab, new Vector3(70, 12, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(tomatoPrefab, new Vector3(80, 15, 0), Quaternion.identity);
            }

            if (Random.Range(1, 3) == 1)
            {
                Instantiate(tomatoPrefab, new Vector3(90, 12, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(tomatoPrefab, new Vector3(100, 15, 0), Quaternion.identity);
            }

        }
        
    }
}
