using UnityEngine;
using System.Collections;

public class ComponentFinder
{
	public enum Direction { Upward, Downward }
	
	/// <summary>
	/// Find a component in the transform hiearchy upwards. 
	/// </summary>
	/// <param name="transform">
	/// The transform to start from.
	/// </param>
	/// <returns>
	/// The component if found, else null.
	/// </returns>
	public static T FindComponent<T>(Transform transform) where T : Component
	{
		return FindComponent<T>(transform, Direction.Upward);
	}
	
	/// <summary>
	/// Find a component in the transform hiearchy. 
	/// </summary>
	/// <param name="transform">
	/// The transform to start from.
	/// </param>
	/// <param name="direction">
	/// The direction in which the hiearchy will be parsed.
	/// </param>
	/// <returns>
	/// The component if found, else null.
	/// </returns>
	public static T FindComponent<T>(Transform transform, Direction direction) where T : Component
	{
		if (!transform)
			return null;
			
		T component = transform.GetComponent<T>() as T;
		if (!component)
		{			
			if (direction == ComponentFinder.Direction.Upward)
			{
				component = FindComponent<T>(transform.parent, direction);
			}
			else
			{
				for (int i = 0; i < transform.childCount && !component; ++i)
					component = FindComponent<T>(transform.GetChild(i), direction);
			}
		}
		
		return component;
	}
	
	/// <summary>
	/// Find a component in the transform hiearchy upwards. 
	/// </summary>
	/// <param name="type">
	/// The type of the component that we are looking for.
	/// </param>
	/// <param name="transform">
	/// The transform to start from.
	/// </param>
	/// <returns>
	/// The component if found, else null.
	/// </returns>
	public static Component FindComponent(System.Type type, Transform transform)
	{
		return FindComponent(type, transform, Direction.Upward);
	}
	
	/// <summary>
	/// Find a component in the transform hiearchy. 
	/// </summary>
	/// <param name="type">
	/// The type of the component that we are looking for.
	/// </param>
	/// <param name="transform">
	/// The transform to start from.
	/// </param>
	/// <param name="direction">
	/// The direction in which the hiearchy will be parsed.
	/// </param>
	/// <returns>
	/// The component if found, else null.
	/// </returns>
	public static Component FindComponent(System.Type type, Transform transform, Direction direction)
	{			
		if (!transform)
			return null;
			
		Component component = transform.GetComponent(type);
		if (!component)
		{			
			if (direction == ComponentFinder.Direction.Upward)
			{
				component = FindComponent(type, transform.parent, direction);
			}
			else
			{
				for (int i = 0; i < transform.childCount && !component; ++i)
					component = FindComponent(type, transform.GetChild(i), direction);
			}
		}
		
		return component;
	}
}
