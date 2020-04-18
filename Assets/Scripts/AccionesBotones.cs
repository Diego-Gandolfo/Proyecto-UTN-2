using UnityEngine;
using System.Collections;

public class AccionesBotones : MonoBehaviour 
{
	public string EscenaJuego;
//	public string EscenaOpciones;

	public void AccionBotonJugar()
	{
		Application.LoadLevel (EscenaJuego);
	}
	/*
	public void AccionBotonOpciones()
	{
		Application.LoadLevel (EscenaOpciones);
	}
	*/
	public void AccionBotonSalir()
	{
		Application.Quit ();
	}
}
