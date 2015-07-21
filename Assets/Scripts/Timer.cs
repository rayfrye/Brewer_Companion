using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	[SerializeField]
	float brewTime;

	[SerializeField]
	float realTime;

	public float timeSpeed;

	void Update()
	{
		brewTime += Time.deltaTime * timeSpeed;
		realTime += Time.deltaTime;
	}
}
