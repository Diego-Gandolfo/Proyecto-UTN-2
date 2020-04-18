using UnityEngine;
using System.Collections;

public class MovimientoPersonaje : MonoBehaviour 
{
	public float Caminar;
	public float Correr;
	[Range (1, 10)]	public int SensibilidadMouse;
	private int SensibilidadMouseFinal;
	public float Gravedad;
	public float CantidadSaltos;
	public float FuerzaSalto;

	private float Traslacion;
	private float FuerzaGravedad;
	private float SaltosRestantes;
	private float EnElPiso;

	void Awake()
	{
		SensibilidadMouseFinal = SensibilidadMouse * SensibilidadMouse * 100;
		FuerzaGravedad = -Gravedad;
		SaltosRestantes = CantidadSaltos;
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.LeftShift))
		{
			Traslacion = Correr;
		}
		else 
		{
			Traslacion = Caminar;
		}

	}

	void FixedUpdate ()
	{
		transform.Translate (0, FuerzaGravedad * Time.deltaTime, 0);

		transform.Translate (0, 0, Traslacion * Time.deltaTime * Input.GetAxis ("Vertical"));
		transform.Translate (Traslacion * Time.deltaTime * Input.GetAxis ("Horizontal"), 0, 0);

		Camera.main.transform.Rotate (Mathf.Clamp (SensibilidadMouseFinal * Time.deltaTime * -Input.GetAxis ("Mouse Y"), -90, 90), 0, 0);
		transform.Rotate (0, SensibilidadMouseFinal * Time.deltaTime * Input.GetAxis ("Mouse X"), 0);

		if(EnElPiso < 1)
			FuerzaGravedad -= Gravedad * Time.deltaTime;

		if(EnElPiso >= 1)
		{
			if ((SaltosRestantes > 0) && (Input.GetKeyDown(KeyCode.Space)) && (CantidadSaltos <= 1))
		    {
				SaltosRestantes--;
				FuerzaGravedad = FuerzaSalto;
				//transform.Translate (0, FuerzaSalto * Time.deltaTime, 0);
			}
			else
			{
				FuerzaGravedad = 0;
				SaltosRestantes = CantidadSaltos * (Traslacion / Correr);
			}
		}

		if ((SaltosRestantes > 0) && (Input.GetKeyDown(KeyCode.Space)) && (CantidadSaltos > 1))
		{
			SaltosRestantes--;
			transform.Translate (0, FuerzaSalto * Time.deltaTime, 0);
		}
	}

	void OnCollisionEnter(Collision ObjCol)
	{
		if (ObjCol.gameObject.name == "Piso")
		{
			EnElPiso++;
		}
	}

	void OnCollisionExit(Collision ObjCol)
	{
		if (ObjCol.gameObject.name == "Piso")
		{
			EnElPiso--;
		}
	}
}
