using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : MonoBehaviour {

    private const int SHOW_COINS_NUMBER = 7;

    [SerializeField]
    private float buildTime;
    [SerializeField]
    private float coinsTime;
    [SerializeField]
    private GameObject loadingObject;
    [SerializeField]
    private GameObject coinObjectPrefab;

    private bool isNew;
    private bool isBuilded;
    private float currentCoinsTime;

    public void Start()
    {
        isNew = true;
        isBuilded = false;
        StartCoroutine(FixBuildTime());
        StartCoroutine(ContructionBuildTime());
    }

    public void Update()
    {
        if(isBuilded)
        {
            currentCoinsTime += Time.deltaTime;
            if(currentCoinsTime > coinsTime)
            {
                StartCoroutine(ShowCoins());
                currentCoinsTime = 0;
            }
        }
    }

    IEnumerator FixBuildTime ()
    {
        yield return new WaitForSeconds(.1f);
        isNew = false;
    }

    IEnumerator ContructionBuildTime ()
    {
        yield return new WaitForSeconds(buildTime);
        loadingObject.SetActive(false);
        isBuilded = true;
    }

    IEnumerator ShowCoins ()
    {
		for (int i = 0; i < SHOW_COINS_NUMBER; i++)
		{
            GameObject coin = Instantiate(coinObjectPrefab, transform.position, Quaternion.identity);
			coin.transform.SetParent(this.transform);
            yield return new WaitForSeconds(.3f);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if(isNew)
            Destroy(this.gameObject);
    }

}
