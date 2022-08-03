using FishNet;
using FishNet.Transporting.Tugboat;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionView : BaseView
{
	[SerializeField] private Button hostButton;
	[SerializeField] private Button joinButton;
	//[SerializeField] private TMP_InputField usernameText;
	[SerializeField] private TMP_InputField ipInput;
	[SerializeField] private TMP_InputField portInput;
	public override void Init()
	{
		base.Init();	
		hostButton.onClick.AddListener(() =>
		{
			//SaveUsername();
			SetIP();
			SetPort();
			InstanceFinder.ServerManager.StartConnection();
			InstanceFinder.ClientManager.StartConnection();
		});

		joinButton.onClick.AddListener(() =>
		{
			//SaveUsername();
			SetIP();
			SetPort();
			InstanceFinder.ClientManager.StartConnection();
		});
	}
	private void SetIP()
	{
		if (String.IsNullOrEmpty(ipInput.text))
		{
			ipInput.text = "localhost";
		}
		InstanceFinder.NetworkManager.GetComponent<Tugboat>().SetClientAddress(ipInput.text);
	}

	private void SetPort()
	{
		InstanceFinder.NetworkManager.GetComponent<Tugboat>().SetPort(Convert.ToUInt16(portInput.text));
	}
}
