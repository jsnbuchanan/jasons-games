using UnityEngine;
using System.Collections;

public class NumberWizard : MonoBehaviour {
	
	const int STARTING_MIN = 1;
	const int STARTING_MAX = 1000;
	
	int max;
	int min;
	int guess;
	
	// Use this for initialization
	void Start () {
		StartGame();
	}
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			min = guess;
			NextGuess();
			
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			max = guess;
			NextGuess();
			
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			print("I won!");
			StartGame();
		}
	}

	void StartGame() {
		max = STARTING_MAX;
		min = STARTING_MIN;
		guess = STARTING_MAX / 2;
		
		print("========================");
		print("Welcome to Number Wizard");
		print("Pick a number but don't tell me");
		
		print("The highest number you can pick is " + max);
		print("The lowest number you can pick is " + min);
		
		print("Is the number higher or lower than "+guess+"?");
		
		max = max+1;
	}
	
	void NextGuess() {
		guess = (max+min)/2;
		print("Higher or lower than "+guess);
		print("Up key = higher, down key = lower, return key = equal");
	}
}
