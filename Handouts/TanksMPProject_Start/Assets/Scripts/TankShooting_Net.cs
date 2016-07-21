using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankShooting_Net : NetworkBehaviour
{
	[SerializeField] float shotVelocity = 18f;	//How fast to shoot the cannonballs
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
		//If this isn't the local player, leave. Note that we can't just remove this script
		//from non-local players like we did with the TankController script. The reason is 
		//that this script has a Command, CmdSpawnShell, that needs to exist on this object
		//even if it isn't the local player
		if (!isLocalPlayer)
			return;

		//If we click the mouse, touch a screen, or hit the spacebar...
		if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
		{
			//Run the server Command to spawn a shell
			CmdSpawnShell();
		}
	}		


	//This Command is called from the localPlayer and run on the server. Note that Commands must begin
	//with 'Cmd'
	[Command]
	void CmdSpawnShell ()
	{
		//We instantiate a shell at the gun barrel with the correct rotation
		GameObject instance = Instantiate (shellPrefab, gunBarrel.position, gunBarrel.rotation) as GameObject; 
		//Locate the rigidbody component of the shell and add some forward force to it
		instance.GetComponent<Rigidbody> ().velocity = gunBarrel.forward * shotVelocity;

		//Finally, let's instantiate this object on the network for all players to see
		NetworkServer.Spawn (instance);
	}
}