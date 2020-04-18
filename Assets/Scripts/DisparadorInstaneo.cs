using UnityEngine;
using System.Collections;

public class DisparadorInstaneo : MonoBehaviour 
{
	public float Daño;
	private float DistanciaDisparo;
	public LayerMask Layers;
	public float TiempoEntreDisparos;
	public float MargenErrorDisparo;
	public GameObject Personaje;
	[HideInInspector] public bool Disparando;
	public AudioSource AudioDisparo;

	public void Awake()
	{
		MovimientoNPC_MapMesh MovNPC_MapMesh = GetComponentInParent<MovimientoNPC_MapMesh> ();

		DistanciaDisparo = MovNPC_MapMesh.DistanciaDisparo ;
	}
	public void Update()
	{
		if ((Vector3.Distance(transform.position, Personaje.transform.position) <= DistanciaDisparo + (DistanciaDisparo / 2)) && (!Disparando))
		{
			Disparando = true;
			Invoke ("Disparar", TiempoEntreDisparos);
		}
	}

	void Disparar()
	{
		RaycastHit HitRaycast;

		Vector3 PosicionAleatoria = Vector3.zero;

		PosicionAleatoria.x = Random.Range (-MargenErrorDisparo, MargenErrorDisparo);
		PosicionAleatoria.y = Random.Range (-MargenErrorDisparo, MargenErrorDisparo) + 1f;
		PosicionAleatoria.z = Random.Range (-MargenErrorDisparo, MargenErrorDisparo);

		Vector3 Objetivo = 	transform.position + transform.forward * DistanciaDisparo;

		transform.LookAt (Personaje.transform.position + PosicionAleatoria);

		bool Impacto = Physics.Raycast(transform.position, transform.forward, out HitRaycast, DistanciaDisparo, Layers);
		AudioDisparo.Play();
		Debug.DrawLine(transform.position, Objetivo, Color.red, 1);

		if(Impacto)
		{
			Vida VerificacionVida = HitRaycast.collider.GetComponentInParent<Vida>();
			ZonaDaño Atenuador = HitRaycast.collider.GetComponent<ZonaDaño>();
			
			if (VerificacionVida != null && Atenuador != null)
			{	
				VerificacionVida.VidaActual = VerificacionVida.VidaActual - (Daño * Atenuador.AtenuacionDaño);
				// Efecto Disparo
			}
		}

		Disparando = false;
	}
}