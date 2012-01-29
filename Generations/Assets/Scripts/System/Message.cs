using UnityEngine;
using System.Collections;

public abstract class Message
{
	public virtual string name { get { return GetType().Name; } }
	
	public bool requireReceiver { get; set; }
	public GameObject sender { get; private set; }
	
	private static string format = "On{0}";
	
	public Message()
	{
		requireReceiver = false;
	}
	
	public void Send(GameObject receipient)
	{
		Send(null, receipient);
	}
	
	public void Send(GameObject sender, GameObject receipient)
	{
		Assert.Test(receipient);
		this.sender = sender;
		receipient.SendMessage(string.Format(format, name), this, requireReceiver ? SendMessageOptions.RequireReceiver : SendMessageOptions.DontRequireReceiver);
	}
	
	public void Broadcast(GameObject receipient)
	{
		Broadcast(null, receipient);
	}
	
	public void Broadcast(GameObject sender, GameObject receipient)
	{
		Assert.Test(receipient);
		this.sender = sender;
		receipient.BroadcastMessage(string.Format(format, name), this, requireReceiver ? SendMessageOptions.RequireReceiver : SendMessageOptions.DontRequireReceiver);
	}
	
	public void BroadcastUpwards(GameObject receipient)
	{
		BroadcastUpwards(null, receipient);
	}
	
	public void BroadcastUpwards(GameObject sender, GameObject receipient)
	{
		Assert.Test(receipient);
		this.sender = sender;
		receipient.SendMessageUpwards(string.Format(format, name), this, requireReceiver ? SendMessageOptions.RequireReceiver : SendMessageOptions.DontRequireReceiver);
	}
}
