using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeBar : MonoBehaviour {
    float maxlife;
    float currLife;
    public void SetValues(float maxLife)
    {
        maxlife = maxLife;
        currLife = maxLife; 
//        print("MaxLife = " + maxlife);
    }
    public void UpdateBar(float newLife)
    {

        StartCoroutine(Bar(currLife,newLife));
        StartCoroutine(SubBar(currLife,newLife));
    }
    IEnumerator Bar(float From, float To)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / .1f)
        {
            Image bar = GetComponent<Image>();
            bar.fillAmount = Mathf.Lerp(From / maxlife, To / maxlife, t);
            yield return null;
        }
        currLife = To;
    }
    IEnumerator SubBar(float From, float To)
    {
        yield return new WaitForSeconds(0.2f);
        for (float t = 0; t < 1; t+=Time.deltaTime/.75f)
        {
            Image bar = transform.parent.GetComponent<Image>();
            bar.fillAmount = Mathf.Lerp(From/maxlife, To/maxlife, t);
            yield return null;
        }
        currLife = To;
    }
}
