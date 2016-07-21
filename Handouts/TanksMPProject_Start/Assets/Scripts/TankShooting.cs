using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour
{
	[SerializeField] float power = 800f;		//How hard to shoot the cannonballs
	[SerializeField] GameObject shellPrefab;	//Prefab that represents the shell
	[SerializeField] Transform gunBarrel;		//Where should the shells shoot from

	//The reset method let's use run slow code, like "Find", in the editor where the performance
	//impact won't affect the players at runtime
	void Reset ()
	{
		//Get the location of the cannon
		gunBarrel = transform.FindChild("GunBarrel");
	}


	void Update ()
	{
		//If we click the mouse, touch a screen, or hit the spacebar...
		if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
		{
			//...we instantiate a shell at the gun barrel with the correct rotation
			GameObject instance = Instantiate (shellPrefab, gunBarrel.position, gunBarrel.rotation) as GameObject; 
			//Locate the rigidbody component of the shell and add some forward force to it
			instance.GetComponent<Rigidbody> ().AddForce (gunBarrel.forward * power);
		}
	}
}
