using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public IEnumerator Execute()
    {
        Debug.Log("[EnemyController] Execute!");
        yield return new WaitForSeconds(1f);
    }
}
