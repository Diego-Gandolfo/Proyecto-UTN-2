using UnityEngine;
using System.Collections;

public class ContarMuertes : MonoBehaviour
{
	public void Mori ()
	{
		GameManager.GM.VidasTotales--;
	}
}
