using System;
using UnityEngine;

public class BUllet : MonoBehaviour
{
	private Transform target;
	public float speed = 70f;
	public GameObject impactEffect;

	public void Seek(Transform _target)
	{
		target = _target;
	}

	private void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		var dir = target.position - transform.position;
		var distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}
		
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	private void HitTarget()
	{
		var effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		Destroy(target.gameObject);
		Destroy(gameObject);
	}
}
