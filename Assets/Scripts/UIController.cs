using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
  [SerializeField] private Text healthLabel;
  [SerializeField] private InventoryPopup popup;
  [SerializeField] private Text levelEnding;

  void Awake() {
    Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
    Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
  }
  void OnDestroy () {
    Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
    Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
  }

  void Start() {
    OnHealthUpdated();

    levelEnding.gameObject.SetActive(false);
    popup.gameObject.SetActive(false);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.M)) {
      bool isShowing = popup.gameObject.activeSelf;
      popup.gameObject.SetActive(!isShowing);
      popup.Refresh();
    }
  }

  private void OnHealthUpdated() {
    string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
    healthLabel.text = message;
  }

  private void OnLevelComplete() {
    StartCoroutine(CompleteLevel());
  }
  private IEnumerator CompleteLevel() {
    levelEnding.gameObject.SetActive(true);
    levelEnding.text = "Level Complete!";

    yield return new WaitForSeconds(2);

    Managers.Mission.GoToNext();
  }
}
