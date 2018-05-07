using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball2 : MonoBehaviour
{


	private float kickForce = 0.1f;
	private GameObject collisionObj;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		
        collisionObj = collision.gameObject;
		if (collisionObj.layer == 8)
        {

            // https://answers.unity.com/questions/1121432/kick-a-ball-add-force.html
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
            // ignoring y-axis => ball doesn't fly around like crazy

            float colVelocity = collision.gameObject.GetComponent<car>().speedPerSec;

            colVelocity = colVelocity * 0.2f;

            print("hit " + colVelocity);
            GetComponent<Rigidbody>().AddForce(direction * kickForce * colVelocity, ForceMode.Impulse);

        }

        if (collisionObj.tag == "GoalPost")
        {
            if (collisionObj.name == "GoalPostPlaneA")
            {
                print("GOOOOOOOL (player B scored 1 point)");
            }
            else if (collisionObj.name == "GoalPostPlaneB")
            {
                print("GOOOOOOOL (player A scored 1 point)");
            }

        }
		
	}


}
