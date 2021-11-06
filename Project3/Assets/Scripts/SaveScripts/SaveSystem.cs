using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    XmlDocument xInventory = new XmlDocument();
    XmlDocument xSettings = new XmlDocument();

    public string iFileName = "";
    public string sFileName = "";

    public string xDirectory;

    public GameObject Player;

    List<string> nodeNames = new List<string>();

    //Variables to Save and Load
    public List<ItemSlot> inventoryList;
    public float masterVolume;
    public float backgroundMusicVolume;
    public float SFXVolume;
    public float resolutionHeight;
    public float resolutionWidth;
    public bool isWindowed;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        xInventory.LoadXml(File.ReadAllText(xDirectory + @"\" + iFileName));
        xSettings.LoadXml(File.ReadAllText(xDirectory + @"\" + sFileName));
    }

    public void SaveInventory(List<ItemSlot> list)
    {
        print("received message write inventory xml");
        if (list != null)
        {
            inventoryList = list;
            int count = 0;
            XmlNode root = xInventory.FirstChild;
            foreach (ItemSlot item in inventoryList)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    switch (node.Name)
                    {
                        //case "Inventory Space " + count:
                            //node.InnerText = item.item.ToString();
                    }
                    count++;
                }
            }
        }
        xInventory.Save(xDirectory + @"\" + "data" + iFileName);

    }

    #region Save/Load Settings
    public void SaveSettings()
    {
        print("received message write settings xml");

    }

    public void LoadSettings()
    {

    }

    #endregion

    #region NewGame
    void LoadNewGame()
    {
        LoadSettings();
    }
    #endregion

    #region ContinueGame
    void LoadSavedGame()
    {
        LoadSettings();
    }
    #endregion
}
