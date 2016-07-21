using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShellController_Net : NetworkBehaviour
{
	[SerializeField] float shellLifetime = 2f;	//How long does the shell live
	[SerializeField] bool canKill = false;		//Can the shell damage the players?
	[SerializeField] bool isDeathmatch = false;	//Is this a Deathmatch Game?

	bool isLive = true;							//Is the shell able to explode?
	float age;									//How long has the shell been alive
	ParticleSystem explosionParticles;			//The shell explosion effect
	MeshRenderer shellRenderer;					//The shell model renderer


	void Start ()
	{
		//Get references to the components we need
		explosionParticles = GetComponentInChildren<ParticleSystem> ();
		shellRenderer = GetComponent<MeshRenderer> ();
	}

	//Shells are updated by the server
	[ServerCallback]
	void Update () 
	{	
		//If the shell has been alive too long...
		age += Time.deltaTime;
		if( age > shellLifetime )
		{	
			//...Destroy it on the network
			NetworkServer.Destroy(gameObject);
		}
	}

	//When the shell hits something
	void OnCollisionEnter(Collision other)
	{
		//If the shell isn't live, leave. 
		if (!isLive)
			return;

		//The shell is going to explode and is no longer live
		isLive = false;

		//Hide the shell body
		shellRenderer.enabled = false;
		//Show the explosion particle effect
		explosionParticles.Play (true);

		//If this is not the server, leave. The above code doesn't need to be 
		//run only on the server since it only deals with the graphical explosion. Since
		//the code below handles actually harming other tanks, it should only be run on
		//the server
		if (!isServer)
			return;

		//If the shell isn't lethal or it didn't hit a player, leave
		if(!canKill || other.gameObject.tag != "Player")
			return;

		if (isDeathmatch) 
		{
			//Get a reference to the hit object's Tank Health script and tell it to take a point of damage
			TankHealth_DM health = other.gameObject.GetComponent<TankHealth_DM> ();
			if(health != null)
				health.TakeDamage (1);
		} 
		else 
		{
			//Get a reference to the hit object's Tank Health script and tell it to take a point of damage
			TankHealth health = other.gameObject.GetComponent<TankHealth> ();
			if(health != null)
				health.TakeDamage (1);
		}
	}
}
