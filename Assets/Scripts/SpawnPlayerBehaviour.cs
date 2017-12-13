using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerBehaviour : MonoBehaviour 
{

    [SerializeField]
    private GameObject Player;

	void Start () 
    {
        StartCoroutine(LazySpawnPlayer());
	}

    IEnumerator LazySpawnPlayer ()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(Player, gameObject.transform.position, Quaternion.identity);
    }
	
}
