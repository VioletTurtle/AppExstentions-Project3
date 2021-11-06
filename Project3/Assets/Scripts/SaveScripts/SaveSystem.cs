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
    public ItemDatabase database;


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
        xInventory.LoadXml(File.ReadAllText(xDirectory + @"\" + iFileName));
        xSettings.LoadXml(File.ReadAllText(xDirectory + @"\" + sFileName));
    }

    public void SaveInventory(List<ItemSlot> list)
    {
        inventoryList = list;
        print("received message write inventory xml");
        xInventory.RemoveAll();
        XmlNode iRoot = xInventory.CreateNode(XmlNodeType.Element, "iData", "");
        string[] nodes = {"ID", "quantity"};

        if (inventoryList != null)
        {
            for (int e = 0; e < inventoryList.Count; e++)
            {
                if (inventoryList[e].item != null)
                {
                    XmlNode iBase = xInventory.CreateNode(XmlNodeType.Element, "inventory", "");

                    for (int n = 0; n < nodes.Length; n++)
                    {
                        XmlNode newNode = xInventory.CreateNode(XmlNodeType.Element, nodes[n], "");

                        iBase.AppendChild(newNode);
                    }

                    foreach (XmlNode node in iBase.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "ID":
                                node.InnerText = inventoryList[e].ID.ToString();
                                break;
                            case "quantity":
                                node.InnerText = inventoryList[e].quantity.ToString();
                                break;
                        }
                        iRoot.AppendChild(iBase);
                    }
                    xInventory.AppendChild(iRoot);
                }
            }

            xInventory.Save(xDirectory + @"\" + "data" + iFileName);
        }

    }

    public List<ItemSlot> LoadInventory(List<ItemSlot> list)
    {
        print("received message load Inventory xml");
        int id = 0;
        int quantity = 0;
        inventoryList = list;

        ItemSlot[] items = new ItemSlot[inventoryList.Count];

        for (int e = 0; e < inventoryList.Count; e++)
        {
            if (inventoryList[e] != null)
            {
                if(inventoryList[e].item != null)
                {
                    inventoryList[e] = new ItemSlot();
                }
                
                XmlNode iData = xInventory.FirstChild;

                XmlNode item = iData.ChildNodes[e];

                if (item.Name == "inventory")
                {
                    foreach (XmlNode iNode in item.ChildNodes)
                    {
                        switch (iNode.Name)
                        {
                            case "ID":
                                id = Convert.ToInt32(iNode.InnerText.ToString());
                                break;
                            case "quantity":
                                quantity = Convert.ToInt32(iNode.InnerText.ToString());
                                break;
                        }
                        items[e] = new ItemSlot(database.GetItem[id], quantity, id);
                    }
                }
            }
            inventoryList[e] = items[e];
        }

        return inventoryList;
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
