using UnityEngine;
using System.Collections;

public class DestructorAutomatico : MonoBehaviour 
{
	public float Tiempo;
	private float Acumulador;

	void Update()
	{
		Acumulador+=Time.deltaTime;

		if (Acumulador >= Tiempo)
		{
			Destroy(gameObject);
		}
	}
}
