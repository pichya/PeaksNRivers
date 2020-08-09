using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameboardLogic : MonoBehaviour {

	public GameObject StartButton;
	public GameObject ResetButton;
	public GameObject PlayerObj;
	
	public GameObject TitleScreenObj;
	
	public ChestController chestControlObj;
	public DiceController diceControlObj;
	public MusicLoopController musicControlObj;
	public SoundFXController sfxControlObj;
	
	public string GameState;
	
	public Text textTotalMoves;
	public Text textTenthMoves;
	public Text textTwentiethMoves;
	public Text textLadderMoves;
	public Text textRiverMoves;
	
	private int playerGamePosition;
	private int playerCurrentPosition;
	//
	private int RandomMove;
	//
	private List<int> GameMoves;
	private List<int> LadderMoves;
	private List<int> RiverMoves;
	
	private int tenthMove;
	private int twentiethMove;
	
	private float waitTimer;

	void Start(){
		ResetButton.SetActive (false);
		ResetGameValues();
		GameState = "TitleScreen";
		musicControlObj.playTitleScreenLoop();
	}
	
	public void OnHideTitleScreen(){
		TitleScreenObj.SetActive(false);
		musicControlObj.playStartGameLoop();
	}
	
	public void OnStartGame(){
		PlayerObj.SetActive (true);
		StartButton.SetActive (false);
		musicControlObj.playMainLoop();
		sfxControlObj.PlaySoundFX(1);
		GameState = "RollDiceAnimation";
	}
	
	public void OnResetGame(){
		StartButton.SetActive (true);
		ResetButton.SetActive (false);
		ResetGameValues();
		UpdateTextDisplay();
		sfxControlObj.PlaySoundFX(1);
		musicControlObj.playStartGameLoop();
		GameState = "StartGameWait";
	}
	
	private void ResetGameValues(){
		PlayerObj.transform.localPosition = new Vector3(-3f, -2.5f, 0.5f);
		PlayerObj.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		PlayerObj.SetActive(true);
		//
		chestControlObj.CloseChest();
		//
		tenthMove = 0;
		twentiethMove = 0;
		//
		playerGamePosition = 0;
		playerCurrentPosition = 0;
		//
		GameMoves = new List<int>();
		LadderMoves = new List<int>();
		RiverMoves = new List<int>();
	}
	
	private int GetRandomMove(){
		int RandomNumber = (int)Random.Range(1, 7);
		return RandomNumber;
	}
	
	private void ShowRandomDiceFace(int DiceNumber){
		switch (DiceNumber){
			case 1:
			diceControlObj.DiceState = "Display1";
			break;
			case 2:
			diceControlObj.DiceState = "Display2";
			break;
			case 3:
			diceControlObj.DiceState = "Display3";
			break;
			case 4:
			diceControlObj.DiceState = "Display4";
			break;
			case 5:
			diceControlObj.DiceState = "Display5";
			break;
			case 6:
			diceControlObj.DiceState = "Display6";
			break;
		}
	}
	
	private void UpdateTextDisplay(){
		textTotalMoves.text = "Total # Of Moves - " + GameMoves.Count;
		textTenthMoves.text = "Square # on 10th Move - " + tenthMove;
		textTwentiethMoves.text = "Square # on 20th Move - " + twentiethMove;
		textLadderMoves.text = "Ladders Climbed - " + LadderMoves.Count;
		textRiverMoves.text = "Rivers Crossed - " + RiverMoves.Count;
	}
	
	public void HighlightSquare(int SquareNum){	
		string SquareName = "square001";
		if (SquareNum < 10) {
			SquareName = "square00" + SquareNum.ToString ();
		}else{
			if (SquareNum > 99) {
				SquareName = "square100";
			}else{
				SquareName = "square0" + SquareNum.ToString ();
			}
		}
		GameObject hilight = GameObject.Find (SquareName);
		hilight.GetComponent<SquareController> ().ShowHilight ();
	}

	public void ClearHighlightSquares (){
		for (int i = 1; i<101; i++){
			ClearHighlightSquare (i);
		}
	}

	public void ClearHighlightSquare(int SquareNum){	
		string SquareName = "square001";
		if (SquareNum < 10) {
			SquareName = "square00" + SquareNum.ToString ();
		}else{
			if (SquareNum > 99) {
				SquareName = "square100";
			}else{
				SquareName = "square0" + SquareNum.ToString ();
			}
		}
		GameObject hilight = GameObject.Find (SquareName);
		hilight.GetComponent<SquareController> ().ClearHilight ();
	}
	
	Vector3 GetNewPosition(int PosNum){
		string SquareName = "square001";
		if (PosNum < 10) {
			SquareName = "square00" + PosNum.ToString ();
		}else{
			if (PosNum > 99) {
				SquareName = "square100";
			}else{
				SquareName = "square0" + PosNum.ToString ();
			}
		}
		GameObject hilight = GameObject.Find (SquareName);
		return (hilight.transform.position);
	}
	
	void RotatePlayerMoveDirection(int PosNum){
		int[] Move90 = {1,2,3,4,5,6,7,8,9,21,22,23,24,25,26,27,28,29,4,1,42,43,44,45,46,47,48,49,61,62,63,64,65,66,67,68,69,81,82,83,84,85,86,87,88,89};
		int[] Move270 = {11,12,13,14,15,16,17,18,19,31,32,33,34,35,36,37,38,39,51,52,53,54,55,56,57,58,59,71,72,73,74,75,76,77,78,79,91,92,93,94,95,96,97,98,99};
		int[] Move0 = {10,20,30,40,50,60,70,80,90};		
		int index = System.Array.IndexOf( Move90, PosNum );
		if (index>-1){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,90f,0f);
		}
		index = System.Array.IndexOf( Move270, PosNum );
		if (index>-1){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,270f,0f);
		}
		index = System.Array.IndexOf( Move0, PosNum );
		if (index>-1){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
		}
	}
	
	void PlayFootstepSound(int PosNum){
		int[] WaterSqaures = {3, 8, 13, 18, 27, 34, 47, 53, 63, 68, 72, 78, 82, 89, 99};
		int index = System.Array.IndexOf( WaterSqaures, PosNum );
		if (index>-1){
			sfxControlObj.PlaySoundFX(6);
		}else{
			if (PosNum >80){
				sfxControlObj.PlaySoundFX(2);
			}
			if (PosNum <20){
				sfxControlObj.PlaySoundFX(3);
			}
			if (PosNum >19 && PosNum <50){
				sfxControlObj.PlaySoundFX(4);
			}
			if (PosNum >49 && PosNum <81){
				sfxControlObj.PlaySoundFX(5);
			}
		}
	}
	
	void FixedUpdate(){
		switch (GameState) {
		case "Init":
			break;
		case "TitleScreen":
			break;
		case "StartGame":
			break;
		case "RollDiceAnimation":
			waitTimer = 0;
			diceControlObj.DiceState = "StartSpin";
			GameState = "RollDiceAnimationWait";
			break;
		case "RollDiceAnimationWait":
			waitTimer += Time.deltaTime * 100f;
			if (waitTimer > 75) {
				GameState = "GetRandomMove";
			}
			break;
		case "GetRandomMove":
			RandomMove = GetRandomMove();
			ShowRandomDiceFace(RandomMove);
			GameMoves.Add(RandomMove);
			playerGamePosition += RandomMove;
			if (playerGamePosition>100){
				playerGamePosition = 100;
			}
			if (GameMoves.Count == 10){
				tenthMove = playerGamePosition;
			}
			if (GameMoves.Count == 20){
				twentiethMove = playerGamePosition;
			}
			HighlightSquare(playerGamePosition);
			UpdateTextDisplay();
			GameState = "ShowMoveWait";
			break;
		case "ShowMoveWait":
			waitTimer += Time.deltaTime * 100f;
			if (waitTimer > 30) {
				GameState = "PlayerAnimateMove";
			}
			GameState = "PlayerAnimateMove";
			break;
		case "PlayerAnimateMove":
			playerCurrentPosition++;
			Vector3 animatePos = GetNewPosition (playerCurrentPosition);
			RotatePlayerMoveDirection(playerCurrentPosition);
			PlayFootstepSound(playerCurrentPosition);
			PlayerObj.transform.position = new Vector3 (animatePos.x, animatePos.y, animatePos.z);
			if (playerCurrentPosition == playerGamePosition) {
				ClearHighlightSquares ();
				if (playerGamePosition == 100){
					// Win State
					PlayerObj.SetActive (false);
					ResetButton.SetActive (true);
					chestControlObj.OpenChest();
					musicControlObj.playWinLoop();
					GameState = "PlayerWin";
				}else{
					// Check For Rope Ladders or Rivers
					GameState = "PlayerMoveCheck";
				}
			} else {
				waitTimer = 0f;
				GameState = "PlayerAniamteWait";
			}
			break;
		case "PlayerAniamteWait":
			waitTimer += Time.deltaTime * 100f;
			if (waitTimer > 23) {
				GameState = "PlayerAnimateMove";
			}
			break;
		case "PlayerMoveCheck":
			int checkPos = CheckForChuteLadder (playerGamePosition);
			if (checkPos == 0) {
			} else {
				Vector3 newPos = GetNewPosition (checkPos);
				PlayerObj.transform.position = new Vector3 (newPos.x, newPos.y, newPos.z);
				playerGamePosition = checkPos;
				playerCurrentPosition = checkPos;
			}
			waitTimer = 0;
			GameState = "PlayerMoveCheckWait";
			break;
		case "PlayerMoveCheckWait":
			waitTimer += Time.deltaTime * 100f;
			if (waitTimer > 20) {
				GameState = "RollDiceAnimation";
			}
			break;
		case "PlayerWin":
			waitTimer += Time.deltaTime * 100f;
			if (waitTimer > 15) {
				GameState = "PlayerWinWait";
			}
			break;
		}
	}

	int CheckForChuteLadder(int SquarePos){
		bool updateFlag = false;
		// Rope Ladders
		if (SquarePos==6){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 15;
		}
		if (SquarePos==25){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 36;
		}
		if (SquarePos==32){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 49;
		}
		if (SquarePos==42){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 59;
		}
		if (SquarePos==56){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 65;
		}
		if (SquarePos==75){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 86;
		}
		if (SquarePos==85){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,0f,0f);
			LadderMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(7);
			UpdateTextDisplay();
			updateFlag = true;
			return 96;
		}
		// Rivers
		if (SquarePos==99){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 82;
		}
		if (SquarePos==89){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 72;
		}
		if (SquarePos==78){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 63;
		}
		if (SquarePos==68){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 53;
		}
		if (SquarePos==47){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 27;
		}
		if (SquarePos==34){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 27;
		}
		if (SquarePos==18){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 3;
		}
		if (SquarePos==13){
			PlayerObj.transform.localEulerAngles = new Vector3(0f,180f,0f);
			RiverMoves.Add(GameMoves.Count);
			sfxControlObj.PlaySoundFX(8);
			UpdateTextDisplay();
			updateFlag = true;
			return 8;
		}
		if (updateFlag != true) {
			return 0;
		}
		// Win State
		if (SquarePos==100){
			Debug.Log("WIN");
			chestControlObj.OpenChest();
			GameState = "PlayerWin";
			return 100;
		}
		//
		return 0;
	}
	
}
