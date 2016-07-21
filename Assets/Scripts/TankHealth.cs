using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TankHealth : NetworkBehaviour
{
	public int maxHealth = 3;	//Player's max health

	Text informationText;		//Text element which will give information to the player
	int health;					//Player's current health

	void Start()
	{
		health = maxHealth;		//Set the player's health
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

			//Since the match is now over, the server will bring the players back to the lobby after
			//3 seconds
			Invoke ("BackToLobby", 3f);

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
		//Find the "Game Over" text object in the scene
		informationText = GameObject.FindObjectOfType<Text> ();

		//If a tank died and is the localPlayer, that means they lost (since they are the one that died)
		//Otherwise, they didn't die and thus won (since this is a two player game)
		if (isLocalPlayer) 
			informationText.text = "Game Over";
		else
			informationText.text = "You Won!";
	}

	void BackToLobby()
	{
		//Go back to the lobby
		FindObjectOfType<NetworkLobbyManager> ().ServerReturnToLobby ();
	}
}
