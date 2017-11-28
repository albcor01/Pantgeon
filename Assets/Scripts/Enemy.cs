using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Parent of the path transforms
    public Transform Path;
    //Current target of the movement
    Transform currentTarget;
    //Current index of the path target
    int targetIndx=0;

    //Velocity in units per second of this enemy
    public float velocity;
    //Rotate velocity
    public float rotVelocity;

    //Life of this enemy

    [Range(0, 100)]
    public int MemReward;


	// Use this for initialization
	void Start () {
        currentTarget = Path.GetChild(0);
        StartCoroutine(Movement());
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    
    //Movement of this unit
    IEnumerator Movement()
    {
        
        while (targetIndx<Path.childCount && currentTarget != null)
        {
//            Debug.Log("Starting movement");
            while (transform.position != currentTarget.position)
            {
                //We store the direction we have
                
                //We will draw a ray to debug our current path
                //Debug.DrawLine(transform.position, currentTarget.position, Color.magenta, Time.deltaTime);

                //Now we move
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, velocity * Time.deltaTime);

                //And smoothly rotate to look toward the trarget
                //Quaternion targetRotation = Quat.LookAt(transform,currentTarget);
                //transform.rotation =Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime* rotVelocity);

                yield return new WaitForSeconds(Time.deltaTime);
            }
            if (++targetIndx < Path.childCount)
            {
                //targetIndx++;
                currentTarget = Path.GetChild(targetIndx);
            }
            else
            {
                targetIndx = 0;
                currentTarget = Path.GetChild(0);
            }
        }

    }
    float lookTo(Transform target)
    {
        Vector3 dirV = currentTarget.position - transform.position;
        float dir = Mathf.Atan2(dirV.y, dirV.x);
        dir *= Mathf.Rad2Deg;
        return dir;
    }//This will return the angles from this object to the target
}
