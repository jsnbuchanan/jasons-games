using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TextController : MonoBehaviour {

	private enum States {
		cell, sheets, sheets_to_mirror, small_lock, small_lock_to_mirror, mirror, cell_holding_mirror, corridor,
		freedom,
		};
	
	private States myState;
		
	public Text text;
	
	// Use this for initialization
	void Start () {
		myState = States.cell;
	}
	
	// Update is called once per frame
	void Update () {
		print (myState);
		handleState (new Dictionary<States, Action>() {
			{States.cell, 					displayCell},
			{States.sheets, 				viewSheets},
			{States.sheets_to_mirror, 		holdMirrorToSheets},
			{States.small_lock, 			viewLock},
			{States.small_lock_to_mirror, 	holdMirrorToLock},
			{States.mirror, 				viewMirror},
			{States.cell_holding_mirror, 	displayCellWhileHoldingMirror},
			{States.corridor, 				displayCorridor},
		});
	}
	
	#region State descriptions
	
	private void displayCell() {
		text.text = "You are in a prison cell, and you want to escape. There are " +
					"some dirty sheets on the bed, a mirror on the wall, and the door " +
					"is locked from the outside.\n\n" +
					"Press S to view Sheets, M to view Mirror and L to view Lock" ;
		
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.S, States.sheets},
			{KeyCode.M, States.mirror},
			{KeyCode.L, States.small_lock}
		});
	}
	
	private void displayCellWhileHoldingMirror() {
		text.text = "You are still in your cell, and you STILL want to escape! There are " +
					"some dirty sheets on the bed, a mark where the mirror was, " +
					"and that pesky door is still there, and firmly locked!\n\n" +
					"Press S to view Sheets, or L to view Lock";
					
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.S, States.sheets_to_mirror},
			{KeyCode.L, States.small_lock_to_mirror}
		});					
	}
	
	private void displayCorridor() {
		text.text = "You are FREE!\n\n" +
					"Press P to Play again";

		handleReturnToCell(KeyCode.P);
	}
	
	private void viewSheets() { 
		text.text = "You can't believe you sleep in these things. Surely it's " +
					"time somebody changed them. The pleasures of prison life " +
					"I guess!\n\n" +
					"Press R to Return to roaming your cell" ;
		
		handleReturnToCell();
	}
	
	private void viewLock() {
		text.text = "This is one of those button locks. You have no idea what the " +
					"combination is. You wish you could somehow see where the dirty " +
					"fingerprints were, maybe that would help.\n\n" +
					"Press R to Return to roaming your cell" ;
		
		handleReturnToCell();					
	}
	
	private void viewMirror() {
		text.text = "The dirty old mirror on the wall seems loose.\n\n" +
					"Press T to Take the mirror, or R to Return to cell" ;
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.T, States.cell_holding_mirror},
			{KeyCode.R, States.cell}
		});
	}
	
	//todo: bug we never get to here
	private void holdMirrorToSheets() {
		text.text = "Holding a mirror in your hand doesn't make the sheets look " +
					"any better.\n\n" +
					"Press R to Return to roaming your cell" ;
		
		handleChoice(getCommand(KeyCode.R, States.cell_holding_mirror));
	}
	
	private void holdMirrorToLock() {
		text.text = "You carefully put the mirror through the bars, and turn it round " +
					"so you can see the lock. You can just make out fingerprints around " +
					"the buttons. You press the dirty buttons, and hear a click.\n\n" +
					"Press O to Open, or R to Return to your cell" ;
		
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.O, States.corridor}, 
			{KeyCode.R, States.cell_holding_mirror}
		});
	}
	
	#endregion
	
	#region Utility Code
	private void handleReturnToCell() {
		handleReturnToCell(KeyCode.R);
	}
	
	private void handleReturnToCell(KeyCode key) {
		handleChoice(getCommand(key, States.cell));
	}
	
	private KeyValuePair<KeyCode, States> getCommand(KeyCode key, States state) { 
		return new KeyValuePair<KeyCode, States>(key, state);
	}
	
	private void handleChoice(IDictionary<KeyCode, States> commands) {
		foreach (var command in commands) {
			if (handleChoice(command)) {
				break;
			}
		}
	}
		
	private void handleState(IDictionary<States, Action> states) {
		foreach (KeyValuePair<States, Action> state in states) {
			if (myState == state.Key) {
				Action action = state.Value;
				action();
				break;
			}
		}
	}
	
	private bool handleChoice(KeyValuePair<KeyCode, States> command) {
		bool commandExecuted = Input.GetKeyDown(command.Key);
		if (commandExecuted) {
			myState = command.Value;
		}
		return commandExecuted;
	}
	#endregion
}
