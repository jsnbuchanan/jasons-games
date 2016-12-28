using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	private enum States {cell, sheets, sheets_to_mirror, small_lock, small_lock_to_mirror, mirror, cell_holding_mirror, freedom};
	
	private States myState;
		
	public Text text;
	
	// Use this for initialization
	void Start () {
		myState = States.cell;
	}
	
	// Update is called once per frame
	void Update () {
		print (myState);
		if (myState == States.cell) {displayCell();}
		else if (myState == States.sheets) {viewSheets();}
		else if (myState == States.sheets_to_mirror) {holdMirrorToSheets();}
		else if (myState == States.small_lock) {viewLock();}
		else if (myState == States.small_lock_to_mirror) {holdMirrorToLock();}
		else if (myState == States.mirror) {viewMirror();}
		else if (myState == States.cell_holding_mirror) {displayCellWhileHoldingMirror();}
		else if (myState == States.freedom) {displayFreedom();}
	}
	
	void displayCell() {
		text.text = "You find yourself in a dank dark prison cell in the middle " +
			"of a large underground cave. The cell is approximately twelve " +
				"feet square with bars on every wall. There is a locked door " +
				"to the east. On one wall of the cell is an unlit torch. Another " +
				"corner of the cell contains a pile of debris. What would you like " +
				"to do?\n\n" +
				"Press T to View Torch, D to Examine Debris and L to Examine the Lock";
		if (Input.GetKeyDown(KeyCode.S)) {myState = States.sheets;}
		else if (Input.GetKeyDown(KeyCode.M)) {myState = States.mirror;}
		else if (Input.GetKeyDown(KeyCode.L)) {myState = States.small_lock;}
	}
	
	void displayCellWhileHoldingMirror() {
	
	}
	
	void displayFreedom() {
	
	}
	
	void viewSheets() { 
	
	}
	
	void viewLock() {
	
	}
	
	void viewMirror() {
	
	}
	
	void holdMirrorToSheets() {
	
	}
	
	void holdMirrorToLock() {
	
	}
}
