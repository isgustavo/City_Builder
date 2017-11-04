using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : MonoBehaviour {

    [SerializeField]
    private float contructionTime;
    [SerializeField]
    private GameObject loadingObject;

    public bool isNew;

    public void Start()
    {
        Debug.Log("Start");
        isNew = true;
        StartCoroutine(FixBuildTime());
        StartCoroutine(ContructionBuildTime());
    }

    IEnumerator FixBuildTime ()
    {
        yield return new WaitForSeconds(.1f);
        isNew = false;
    }

    IEnumerator ContructionBuildTime ()
    {
        yield return new WaitForSeconds(contructionTime);
        loadingObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("OnTriggerEnter2D");
        if(isNew)
            Destroy(this.gameObject);
    }

}
