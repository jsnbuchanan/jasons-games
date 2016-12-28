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
		text.text = "You are in a prison cell, and you want to escape. There are " +
					"some dirty sheets on the bed, a mirror on the wall, and the door " +
					"is locked from the outside.\n\n" +
					"Press S to view Sheets, M to view Mirror and L to view Lock" ;
		if (Input.GetKeyDown(KeyCode.S)) {myState = States.sheets;}
		else if (Input.GetKeyDown(KeyCode.M)) {myState = States.mirror;}
		else if (Input.GetKeyDown(KeyCode.L)) {myState = States.small_lock;}
	}
	
	void displayCellWhileHoldingMirror() {
		text.text = "You are still in your cell, and you STILL want to escape! There are " +
					"some dirty sheets on the bed, a mark where the mirror was, " +
					"and that pesky door is still there, and firmly locked!\n\n" +
					"Press S to view Sheets, or L to view Lock" ;
		if (Input.GetKeyDown(KeyCode.S)) {myState = States.sheets_1;}
		else if (Input.GetKeyDown(KeyCode.L)) {myState = States.lock_1;}
	}
	
	void displayFreedom() {
		text.text = "You are FREE!\n\n" +
					"Press P to Play again";
		if (Input.GetKeyDown(KeyCode.P)) {myState = States.cell;}
	}
	
	void viewSheets() { 
		text.text = "You can't believe you sleep in these things. Surely it's " +
					"time somebody changed them. The pleasures of prison life " +
					"I guess!\n\n" +
					"Press R to Return to roaming your cell" ;
		if (Input.GetKeyDown(KeyCode.R)) {myState = States.cell;}
	}
	
	void viewLock() {
		text.text = "This is one of those button locks. You have no idea what the " +
					"combination is. You wish you could somehow see where the dirty " +
					"fingerprints were, maybe that would help.\n\n" +
					"Press R to Return to roaming your cell" ;
		if (Input.GetKeyDown(KeyCode.R)) {myState = States.cell;}
	}
	
	void viewMirror() {
		text.text = "The dirty old mirror on the wall seems loose.\n\n" +
					"Press T to Take the mirror, or R to Return to cell" ;
		if (Input.GetKeyDown(KeyCode.T)) {myState = States.cell_holding_mirror;}
		else if (Input.GetKeyDown(KeyCode.R)) {myState = States.cell;}
	}
	
	void holdMirrorToSheets() {
		text.text = "Holding a mirror in your hand doesn't make the sheets look " +
					"any better.\n\n" +
					"Press R to Return to roaming your cell" ;
		if (Input.GetKeyDown(KeyCode.R)) {myState = States.cell_holding_mirror;}
	}
	
	void holdMirrorToLock() {
		text.text = "You carefully put the mirror through the bars, and turn it round " +
					"so you can see the lock. You can just make out fingerprints around " +
					"the buttons. You press the dirty buttons, and hear a click.\n\n" +
					"Press O to Open, or R to Return to your cell" ;
		if (Input.GetKeyDown(KeyCode.O)) {myState = States.freedom;}
		else if (Input.GetKeyDown(KeyCode.R)) {myState = States.cell_mirror;}
	}
}
