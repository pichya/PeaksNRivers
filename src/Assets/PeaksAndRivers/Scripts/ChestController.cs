using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject ChestTop;
	public GameObject ChestTreasure;
	public float ChestTopAngle;
	
	public string ChestState;
	
    void Start()
    {
        CloseChest();
    }

	public void CloseChest(){
		ChestTopAngle = 0;
		ChestTop.transform.localEulerAngles = new Vector3(0f,0f,0f);
		ChestTreasure.SetActive(false);
		ChestState = "Closed";
	}
	
	public void OpenChest(){
		if (ChestState == "Closed"){
			ChestState = "AnimateOpen";
			ChestTreasure.SetActive(true);
		}
	}
	
    void FixedUpdate()
    {
        if (ChestState == "AnimateOpen"){
			ChestTopAngle -= Time.deltaTime * 75;
			ChestTop.transform.localEulerAngles = new Vector3(ChestTopAngle,0f,0f);
			if (ChestTopAngle <=-90f){
				ChestTopAngle = -90f;
				ChestTop.transform.localEulerAngles = new Vector3(ChestTopAngle,0f,0f);
				ChestState = "Opened";
			}
		}
    }
}
