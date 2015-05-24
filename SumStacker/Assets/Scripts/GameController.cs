using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private int[] textNums;
	
	public Text rowText0;
	public Text rowText1;
	public Text rowText2;
	public Text rowText3;
	public Text rowText4;
	public Text rowText5;
	public Text rowText6;
	public Text rowText7;
	
	public Text scoreText;
	private int score;
	public int blockNum;
	private int prevBlockNum;
	



	// Use this for initialization
	void Start () {
		textNums = new int[8];
		for (int i = 0; i < 8; i++) {
			textNums[i] = Random.Range (8, 24);
		}
		score = 1;
		blockNum = 0;
		prevBlockNum = 0;

	}
	
	// Update is called once per frame
	void Update () {
		// SCORE STUFF BELOW
		if (blockNum > prevBlockNum) {
			prevBlockNum++;
			score++;
		}
		
		rowText0.text = "" + textNums[0];
		rowText1.text = "" + textNums[1];
		rowText2.text = "" + textNums[2];
		rowText3.text = "" + textNums[3];
		rowText4.text = "" + textNums[4];
		rowText5.text = "" + textNums[5];
		rowText6.text = "" + textNums[6];
		rowText7.text = "" + textNums[7];
		
		scoreText.text = "Score: " + score;

	}
}
