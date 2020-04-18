using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovimientoNPC_MapMesh : MonoBehaviour 
{
	public float VelocidadCaminar;
	public float VelocidadCorrer;

	public bool Patrullar;
	public List<Transform> Camino;
	private int PostaActual;

	public GameObject Personaje;
	public float RadioVision;
	public LayerMask LayerPersonaje;

	public float DistanciaDisparo;

	void Update ()
	{
		Collider[] PersonajeCercano = Physics.OverlapSphere (transform.position, RadioVision, LayerPersonaje);
		NavMeshAgent nma = GetComponent<NavMeshAgent> ();

		if (Patrullar)
		{
			if (PersonajeCercano.Length > 0)
			{
				if (Vector3.Distance(transform.position, Personaje.transform.position) <= DistanciaDisparo)
				{
					nma.speed = 0;
					nma.SetDestination(transform.position);
					transform.LookAt(Personaje.transform.position);
				}
				else
				{
					nma.speed = VelocidadCorrer;
					nma.SetDestination(Personaje.transform.position);
				}
			}
			else
			{
				nma.speed = VelocidadCaminar;

				Transform Destino = Camino[PostaActual];
				nma.SetDestination(Destino.transform.position);
				
				if (Vector3.Distance(transform.position, Destino.position) <= 5f)
				{
					PostaActual++;
					
					if (PostaActual >= Camino.Count)
						PostaActual =0;
				}
			}
		}
		else
		{
			if (Vector3.Distance(transform.position, Personaje.transform.position) <= DistanciaDisparo)
			{
				nma.speed = 0;
				nma.SetDestination(transform.position);
				transform.LookAt(Personaje.transform.position);
			}
			else
			{
				nma.speed = VelocidadCorrer;
				nma.SetDestination(Personaje.transform.position);
			}
		}
	}
}
