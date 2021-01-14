﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Card : MonoBehaviour {

	/*
	* 1-9 sunt normale
	* 10 este skip
	* 11 este reverse
	* 12 este trage 2
	* 13 este wild
	* 14 este wild draw 4
	*/

	int number;
	string color;
	GameObject cardObj;

	public Card (int numb, string color, GameObject obj) { //defines the object
		number = numb;
		this.color = color;
		cardObj = obj;
	}
	public GameObject loadCard(int x, int y, Transform parent) { //when ran, it tells where to load the card on the screen
		GameObject temp = loadCard (parent);
		temp.transform.localPosition = new Vector2 (x, y+540);
		return temp;
	}
	public GameObject loadCard(Transform parent) { //does all the setup for loading. Used if card doesn't need a specific position		
		GameObject temp = Instantiate (cardObj);
		temp.name = color + number;
		if (number < 10) {
			foreach (Transform childs in temp.transform) {
				if (childs.name.Equals ("Cover"))
					break;
				childs.GetComponent<Text> ().text = number.ToString ();
			}
			temp.transform.GetChild (1).GetComponent<Text> ().color = returnColor (color);
		}
		else if (number == 10 || number == 11 || number==12) {
			temp.transform.GetChild (1).GetComponent<RawImage> ().color = returnColor (color);
		}
		else if (number == 13) {
			temp.transform.GetChild (0).GetComponent<Text> ().text = ""; 
			temp.transform.GetChild (2).GetComponent<Text> ().text = "";
		}

		temp.GetComponent<RawImage> ().texture = Resources.Load (color + "Card") as Texture2D;
		temp.transform.SetParent (parent);
		temp.transform.localScale = new Vector3 (1, 1, 1);
		return temp;
	}
	Color returnColor(string what) { //returns a color based on the color string
		switch (what) {
		case "Green":
			return new Color32 (0x55, 0xaa, 0x55,255);
		case "Blue":
			return new Color32 (0x55, 0x55, 0xfd,255);
		case "Red":
			return new Color32 (0xff, 0x55, 0x55,255);
		case "Yellow":
			return new Color32 (0xff, 0xaa, 0x00,255);
		}
		return new Color (0, 0, 0);
	}
	public int getNumb() { 
		return number;
	}
	public string getColor() { 
		return color;
	}
	public bool Equals(Card other) { // cartile sunt considerate egale (compatibile) daca au acelasi numar sau aceiasi culoare
		return other.getNumb () == number || other.getColor ().Equals (color);
	}
	public void changeColor(string newColor) { // folosit pentru a schimba culoarea wild card-urilor
		color = newColor;
	}
}
