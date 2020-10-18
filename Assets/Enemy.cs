using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Range(2,10)]
	public float speed = 5f;
	private Transform target;
	private int wavePointIndex = 0;

	void Start ()
	{
		target = WayPoints.points[0];
	}

	private void Update()
	{
		var dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWayPoint();
		}
	}

	private void GetNextWayPoint()
	{
		if (wavePointIndex >= WayPoints.points.Length - 1)
		{
			DestroyImmediate(gameObject);
			return;
		}
		wavePointIndex++;
		target = WayPoints.points[wavePointIndex];
	}
}
