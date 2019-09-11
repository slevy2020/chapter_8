using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour {
void OnGUI() {
    int posX = 10;
    int posY = 10;
    int width = 100;
    int height = 30;
    int buffer = 10;

    List<string> itemList = Managers.Inventory.GetItemList();
    if (itemList.Count == 0) {
      GUI.Box(new Rect(posX, posY, width, height), "No Items");
    }
    foreach (string item in itemList) {
      int count = Managers.Inventory.GetItemCount(item);
      Texture2D image = Resources.Load<Texture2D>("Icons/"+item);
      GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
      posX += width+buffer;
    }

    string equipped = Managers.Inventory.equippedItem;
    if (equipped != null) {
      posX = Screen.width - (width+buffer);
      Texture2D image = Resources.Load("Icons/"+equipped) as Texture2D;
      GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
    }

    posX = 10;
    posY += height+buffer;

    // Debug.Log("Before loop");
    //for (int j = 0; j < itemList.length; j++)
    // || Input.GetKeyDown((itemList.IndexOf(item) + 1).ToString())
    foreach (string item in itemList) {
      // Debug.Log("In Loop");
      // string keyPress = (itemList.IndexOf(item) + 1).ToString();
      // Debug.Log("Key Press to check: " + keyPress);

      if (GUI.Button(new Rect(posX, posY, width, height), "Equip "+item)) {
        // Debug.Log("Equipping.....");
        Managers.Inventory.EquipItem(item);
      }

      if (item == "health") {
        if (GUI.Button(new Rect(posX, posY + height+buffer, width, height), "Use Health")) {
          Managers.Inventory.ConsumeItem("health");
          Managers.Player.ChangeHealth(25);
        }
      }

      posX += width+buffer;
    }
  }

  void Update() {
    List<string> itemList_ = Managers.Inventory.GetItemList();
    foreach (string item_ in itemList_) {
      if (Input.GetKeyDown((itemList_.IndexOf(item_) + 1).ToString())) {
        Managers.Inventory.EquipItem(item_);
      }

      if (Managers.Inventory.GetEquipped() == "health") {
        if (Input.GetKeyDown("u")) {
          Managers.Inventory.ConsumeItem("health");
          Managers.Player.ChangeHealth(25);
          Managers.Inventory.EquipItem("health");
        }
      }
    }
  }
}
