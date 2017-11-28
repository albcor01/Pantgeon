using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParpadeoEfecto : MonoBehaviour {
    // Update is called once per frame
    float ScaleX, ScaleY;
    RectTransform rectT;
	float Xstart;
    private void Start()
    {
         rectT= GetComponent<RectTransform>();
			Xstart = rectT.localScale.x;
    }
	float aumento = 0.10f;
	// Update is called once per frame
	void Update () {

		if (rectT.localScale.x > Xstart + 0.035f)
			aumento = -aumento;
		else if (rectT.localScale.x < Xstart)
			aumento = -aumento;


		transform.localScale += new Vector3 (aumento, aumento)* Time.deltaTime; 
		}
}
