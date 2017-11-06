using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {

    private Vector3 destinationPosition;
    private float lazyDestoryTime = .5f;
    private float lifetime = 3f;

    public void Start()
    {
        RectTransform destinationPanel = GameObject.FindGameObjectWithTag("MoneyPanel").transform as RectTransform;
        RectTransform canvas = GameObject.FindGameObjectWithTag("Canvas").transform as RectTransform;
        if(destinationPanel != null)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, destinationPanel.position, Camera.main, out destinationPosition);
		}
        StartCoroutine(DestroyCoin());
    }

    IEnumerator DestroyCoin ()
    {
        yield return new WaitForSeconds(lazyDestoryTime);
        float currentTime = 0;
        while (currentTime < lifetime)
        {
            transform.position = Vector3.Lerp(transform.position, destinationPosition, (currentTime / lifetime));
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

}
