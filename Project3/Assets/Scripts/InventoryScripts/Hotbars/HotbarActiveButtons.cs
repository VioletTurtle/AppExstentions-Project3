using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarActiveButtons : MonoBehaviour
{
    [SerializeField] private ToggleActiveWithKeyPress TP;
    [SerializeField] private GameObject[] hotbarButtons;
    [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

    [SerializeField] private Hotbar hotbar;

    // Update is called once per frame
    void Update()
    {
        if (TP.ObjectToToggle.activeSelf == true)
        {
            foreach(GameObject obj in hotbarButtons)
            {
                obj.SetActive(false);
            }
            hotbar.isInventory = true;
        }
        else
        {
            foreach (GameObject obj in hotbarButtons)
            {
                obj.SetActive(true);
            }
            
            hotbar.isInventory = false;
            onMouseEndHoverItem.Raise();
        }
    }
}
