using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class TankHealth_DM : NetworkBehaviour
{
	public int maxHealth = 3;	//Player's max health

	Text informationText;		//Text element which will give information to the player
	int health;					//Player's current health
										
	void Start()
	{
		//Set the current health
		health = maxHealth;

		//If this is the server, tell the DeathMatchManager that this tank spawned
		if (isServer) 
			DeathMatchManager.AddTank (this);
	}

	public void TakeDamage(int amount)
	{
		//Damage will only be calculated on the server. This prevents a hacked client from
		//cheating. Also, if this Tank is already dead, no need to run this code anymore.
		if (!isServer || health <= 0)
			return;

		health -= amount;

		//If the player is out of health...
		if (health <= 0) 
		{
			health = 0;

			//...Call a method on all instances of this object on all clients (This is called an RPC)
			RpcDied();

			//Tell the DeathmatchManager that this tank died
			if (DeathMatchManager.RemoveTankAndCheckWinner (this)) 
			{
				TankHealth_DM tank = DeathMatchManager.GetWinner ();
				tank.RpcWon ();

				//Since the match is now over, the server will bring the players back to the lobby after
				//3 seconds
				Invoke ("BackToLobby", 3f);
			}

			//Exit the method. This is usefull in case we have "hurt" effects below this. We may not want the "hurt" effects
			//to player when the tank has been destroyed. This leaves the method and prevents those effects from running
			return;
		}

		//If you have any "hurt" effects when the player takes damage, you would run them here
	}

	//Since TakeDamage was run on the server, if the code for a tank being destroyed was run there, it would
	//only be visible on the server machine. Since we want the same tank destroyed on all clients so all players
	//see the same tank destroyed, we need to use an RPC. Note that RPCs begin with Rpc 
	[ClientRpc]
	void RpcDied()
	{
		//Darken the tank to show it is dead
		GetComponent<TankColor> ().HideTank ();

		//If a tank died and is the localPlayer, that means they lost (since they are the one that died)
		if (isLocalPlayer) 
		{
			//Find the "Game Over" text object in the scene
			informationText = GameObject.FindObjectOfType<Text> ();
			informationText.text = "Game Over";

			//Disable tank functions
			GetComponent<TankController_Net> ().enabled = false;
			GetComponent<TankShooting_Net> ().enabled = false;

		}
	}

	[ClientRpc]
	public void RpcWon()
	{
		//If a tank won and is the localPlayer, that means they won (since they aren't the one that died)
		if (isLocalPlayer) 
		{
			//Find the "Game Over" text object in the scene
			informationText = GameObject.FindObjectOfType<Text> ();
			informationText.text = "You Won!";
		}
	}

	void BackToLobby()
	{
		//Go back to the lobby
		FindObjectOfType<NetworkLobbyManager> ().ServerReturnToLobby ();
	}
}
