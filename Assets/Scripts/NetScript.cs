using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetScript : MonoBehaviour
{
	NetworkManager manager;
	NetworkDiscovery networkDiscovery;

	bool broadcasting, iAmComp;
	string myName = "It's me";

    void Awake()
	{
		iAmComp = !(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);   
	}

	void Start()
	{
		manager = GetComponent<NetworkManager>();
		networkDiscovery = GetComponent<NetworkDiscovery>();
		networkDiscovery.Initialize();

		if (iAmComp)
		{
			networkDiscovery.broadcastData = myName;
			networkDiscovery.StartAsServer();
			manager.StartHost();
		}
		else
		{
			networkDiscovery.StartAsClient();
		}	
	}
		
	void Update()
	{
		if (!iAmComp && networkDiscovery.enabled)
		{
			foreach (KeyValuePair<string, NetworkBroadcastResult> pare in networkDiscovery.broadcastsReceived)
			{
				if (System.Text.Encoding.Unicode.GetString(pare.Value.broadcastData) == myName)
				{
					string sIP = pare.Key.Replace("f", "").Replace(":", "");
					manager.networkAddress = sIP;
					networkDiscovery.enabled = false;
					manager.StartClient();
					return;
				}
			}
		}
	}

}
