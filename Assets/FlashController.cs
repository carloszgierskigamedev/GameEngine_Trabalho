using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flash()
    {
        StartCoroutine(FadeInCoroutine(1.5f, Color.red, 0f));
    }

    IEnumerator FadeInCoroutine(float rate, Color color, float delay)
    {
        if(delay > 0) yield return new WaitForSeconds(delay);
        
        image.enabled = true;
        float alpha = 0.7f;

        while(alpha > 0f)
        {
            color.a = alpha;
            image.color = color;
            alpha -= rate * Time.deltaTime;

            yield return null;
        }
        
        image.enabled = false;
    }
}
