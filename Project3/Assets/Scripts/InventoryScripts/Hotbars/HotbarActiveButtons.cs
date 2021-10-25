using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarActiveButtons : MonoBehaviour
{
    [SerializeField] private ToggleActiveWithKeyPress TP;
    [SerializeField] private GameObject[] hotbarButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TP.ObjectToToggle.activeSelf == true)
        {
            foreach(GameObject obj in hotbarButtons)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in hotbarButtons)
            {
                obj.SetActive(true);
            }
        }
    }
}
