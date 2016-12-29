using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TextController : MonoBehaviour {

	private enum States {
		cell, sheets, sheets_to_mirror, small_lock, small_lock_to_mirror, mirror, cell_holding_mirror, corridor,
	    stairs_0, stairs_1, stairs_2, courtyard, floor, corridor_1, corridor_2, corridor_3,closet_door, in_closet
	
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
			{States.stairs_0, 				stairs_0},
			{States.stairs_1, 				stairs_1},
			{States.stairs_2, 				stairs_2},
			{States.courtyard, 				courtyard},
			{States.floor,					floor},
			{States.corridor_1, 			corridor_1},
			{States.corridor_2, 			corridor_2},
			{States.corridor_3, 			corridor_3},
			{States.closet_door,			closet_door},
			{States.in_closet, 				in_closet}
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
		text.text = "You're out of your cell, but not out of trouble." +
					"You are in the corridor, there's a closet and some stairs leading to " +
					"the courtyard. There's also various detritus on the floor.\n\n" +
					"C to view the Closet, F to inspect the Floor, and S to climb the stairs";
		
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.S, States.stairs_0},
			{KeyCode.F, States.floor},
			{KeyCode.C, States.closet_door}
		});  
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
	
	private void in_closet() {
		text.text = "Inside the closet you see a cleaner's uniform that looks about your size! " +
					"Seems like your day is looking-up.\n\n" +
					"Press D to Dress up, or R to Return to the corridor";
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.R, States.corridor_2},
			{KeyCode.D, States.corridor_3}
		});
	}
	
	private void closet_door() {
		text.text = "You are looking at a closet door, unfortunately it's locked. " +
					"Maybe you could find something around to help enourage it open?\n\n" +
					"Press R to Return to the corridor";
		returnToCorridor();
	}
	
	private void corridor_3() {
		text.text = "You're standing back in the corridor, now convincingly dressed as a cleaner. " +
					"You strongly consider the run for freedom.\n\n" +
					"Press S to take the Stairs, or U to Undress";
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.S, States.courtyard},
			{KeyCode.U, States.in_closet}
		});
	}
	
	private void corridor_2() {
		text.text = "Back in the corridor, having declined to dress-up as a cleaner.\n\n" +
					"Press C to revisit the Closet, and S to climb the stairs";
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.C, States.in_closet},
			{KeyCode.S, States.stairs_2}
		});
	}
	
	private void corridor_1() {
		text.text = "Still in the corridor. Floor still dirty. Hairclip in hand. " +
					"Now what? You wonder if that lock on the closet would succumb to " +
					"to some lock-picking?\n\n" +
					"P to Pick the lock, and S to climb the stairs";
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.P, States.in_closet},
			{KeyCode.S, States.stairs_1}
		});
	}
	
	private void floor () {
		text.text = "Rummagaing around on the dirty floor, you find a hairclip.\n\n" +
					"Press R to Return to the standing, or H to take the Hairclip." ;
		handleChoice(new Dictionary<KeyCode, States>(){
			{KeyCode.R, States.corridor},
			{KeyCode.H, States.corridor_1}
		});
	}	
	
	private void courtyard () {
		text.text = "You walk through the courtyard dressed as a cleaner. " +
					"The guard tips his hat at you as you waltz past, claiming " +
					"your freedom. You heart races as you walk into the sunset.\n\n" +
					"Press P to Play again." ;
		handleReturnToCell(KeyCode.P);
	}	
	
	private void stairs_0 () {
		text.text = "You start walking up the stairs towards the outside light. " +
						"You realise it's not break time, and you'll be caught immediately. " +
							"You slither back down the stairs and reconsider.\n\n" +
							"Press R to Return to the corridor." ;
		returnToCorridor();
	}
	
	private void stairs_1 () {
		text.text = "Unfortunately weilding a puny hairclip hasn't given you the " +
						"confidence to walk out into a courtyard surrounded by armed guards!\n\n" +
							"Press R to Retreat down the stairs" ;
		handleChoice(getCommand(KeyCode.R, States.corridor_1));
	}
	
	private void stairs_2() {
		text.text = "You feel smug for picking the closet door open, and are still armed with " +
						"a hairclip (now badly bent). Even these achievements together don't give " +
							"you the courage to climb up the staris to your death!\n\n" +
							"Press R to Return to the corridor";
		handleChoice(getCommand(KeyCode.R, States.corridor_2));
	}
	#endregion
	
	#region Utility Code
	
	private void returnToCorridor() {
		handleChoice(getCommand(KeyCode.R, States.corridor));
	}
	
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
