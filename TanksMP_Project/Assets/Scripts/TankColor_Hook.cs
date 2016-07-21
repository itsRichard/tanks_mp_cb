using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using UnityEngine.Networking;

public class TankColor_Hook: LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		TankColor tank = gamePlayer.GetComponent<TankColor>();

      	tank.color = lobby.playerColor;
    }
}
