using UnityEngine;
using System.Collections;

public class DisparadorPersonaje : MonoBehaviour
{
	public float Daño;
	public float DistanciaDisparo;
	public float FrecuenciaDisparo;
	public LayerMask Layers;
	public GameObject EfectoSangre;
	public AudioSource AudioDisparo;
	private bool Disparando;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			InvokeRepeating("Disparar", 0, FrecuenciaDisparo);
		}
		else if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			CancelInvoke("Disparar");
		}
	}

	public void Disparar()
	{
		RaycastHit HitRaycast;
		bool Impacto = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitRaycast, DistanciaDisparo, Layers);
		AudioDisparo.Play();
		
		Vector3 Objetivo = 	Camera.main.transform.position + Camera.main.transform.forward * DistanciaDisparo;
		Debug.DrawLine(Camera.main.transform.position, Objetivo, Color.red, 1);
		
		if(Impacto)
		{
			Vida VerificacionVida = HitRaycast.collider.GetComponentInParent<Vida>();
			ZonaDaño Atenuador = HitRaycast.collider.GetComponent<ZonaDaño>();
			
			if (VerificacionVida != null && Atenuador != null)
			{	
				VerificacionVida.VidaActual = VerificacionVida.VidaActual - (Daño * Atenuador.AtenuacionDaño);
				Instantiate(EfectoSangre, HitRaycast.transform.position, HitRaycast.transform.rotation);
			}
		}
	}
}