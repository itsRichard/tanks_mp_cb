using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;

namespace UnityStandardAssets.Network
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;


		//This accounts for the API change in Unity 5.4
		#if UNITY_5_4_OR_NEWER
		public void Populate(MatchInfoSnapshot match, LobbyManager lobbyManager, Color c)
		#else
		public void Populate(MatchDesc match, LobbyManager lobbyManager, Color c)
		#endif
		{
			serverInfoText.text = match.name;

            slotInfo.text = match.currentSize.ToString() + "/" + match.maxSize.ToString(); ;

            NetworkID networkID = match.networkId;

            joinButton.onClick.RemoveAllListeners();
            joinButton.onClick.AddListener(() => { JoinMatch(networkID, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        void JoinMatch(NetworkID networkID, LobbyManager lobbyManager)
        {
			//This accounts for the API change in Unity 5.4
			#if UNITY_5_4_OR_NEWER
				lobbyManager.matchMaker.JoinMatch (networkID, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
			#else
				lobbyManager.matchMaker.JoinMatch(networkID, "", lobbyManager.OnMatchJoined);
			#endif

			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
			lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();
        }
    }
}