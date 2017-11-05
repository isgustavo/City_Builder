using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildItem : MonoBehaviour {

    private const int SHOW_COINS_NUMBER = 3;
    private const float TIME_BETWEEN_COINS = .1f;

    [SerializeField]
    private SpriteRenderer buildImage;
	[SerializeField]
	private GameObject loadingObject;
	[SerializeField]
	private GameObject coinObjectPrefab;

    private int constructionTime;
	private int profitValue;
    private int profitTime;

    private bool isNew;
    private bool isBuilded;
    private float currentCoinsTime;

	public void SetValues(Sprite sprite, int constructionTime, int profitValue, int profitTime)
	{
        this.buildImage.sprite = sprite;
		this.constructionTime = constructionTime;
		this.profitValue = profitValue;
		this.profitTime = profitTime;

		isNew = true;
		isBuilded = false;
		StartCoroutine(FixBuildTime());
		StartCoroutine(ContructionBuildTime());
	}

	public void Update()
	{
		if (isBuilded)
		{
			currentCoinsTime += Time.deltaTime;
			if (currentCoinsTime > profitTime)
			{
				StartCoroutine(ShowCoins());
				currentCoinsTime = 0;
				GameManagerBehaviour.instancie.AddMoney(profitValue);
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
        yield return new WaitForSeconds(constructionTime);
        loadingObject.SetActive(false);
        isBuilded = true;
    }

    IEnumerator ShowCoins ()
    {
		for (int i = 0; i < SHOW_COINS_NUMBER; i++)
		{
            GameObject coin = Instantiate(coinObjectPrefab, transform.position, Quaternion.identity);
			coin.transform.SetParent(this.transform);
            yield return new WaitForSeconds(TIME_BETWEEN_COINS);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if(isNew)
            Destroy(this.gameObject);
    }

}
