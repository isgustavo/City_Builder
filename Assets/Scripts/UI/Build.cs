using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    public bool isNew;

    public void Start()
    {
        Debug.Log("Start");
        isNew = true;
        StartCoroutine(FixBuildTime());
    }

    IEnumerator FixBuildTime ()
    {
        yield return new WaitForSeconds(.1f);
        isNew = false;
    }
    void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("OnTriggerEnter2D");
        if(isNew)
            Destroy(this.gameObject);
    }

}
