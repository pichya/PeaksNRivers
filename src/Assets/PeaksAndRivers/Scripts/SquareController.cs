using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public GameObject hilightObj;
	
	// Start is called before the first frame update
    void Start()
    {
        ClearHilight();
    }

	public void ShowHilight(){
		hilightObj.SetActive(true);
	}
	
	public void ClearHilight(){
		hilightObj.SetActive(false);
	}
	
}
