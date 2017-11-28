using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour {
    //Porsiaca
    public bool Active=true;
    //El jugador que no puede pasar
    [Range(1,2)]
    public int PlayerToStop;
    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        if (PlayerToStop == 2)
            color = Color.green;
        color = new Color(color.r, color.g, color.b, 100);
        Gizmos.color = color;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Gizmos.DrawWireCube(child.position, child.GetComponent<BoxCollider2D>().size);
        }
    }
}
