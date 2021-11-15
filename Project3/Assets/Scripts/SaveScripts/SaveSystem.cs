using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.Audio;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    XmlDocument xInventory = new XmlDocument();
    XmlDocument xSettings = new XmlDocument();

    public string iFileName = "";
    public string sFileName = "";

    public string xDirectory;
    public ItemDatabase database;
    bool isFilled;
    public AudioMixer audioMixer;
    public AudioSource music;


    //Variables to Save and Load
    List<ItemSlot> inventoryList;
    public bool isNew = false;
    public string level = "Level1";
    public int quality;
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;
    public int resolutionHeight;
    public int resolutionWidth;
    public bool isFullscreen;

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
        music = GetComponent<AudioSource>();
    }

    #region Save/Load Inventory
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
                
                XmlNode iBase = xInventory.CreateNode(XmlNodeType.Element, "inventory", "");

                for (int n = 0; n < nodes.Length; n++)
                {
                    XmlNode newNode = xInventory.CreateNode(XmlNodeType.Element, nodes[n], "");

                    iBase.AppendChild(newNode);
                }
                if (inventoryList[e].item != null)
                {
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
                }
                else
                {
                    foreach(XmlNode node in iBase.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "ID":
                                node.InnerText = Convert.ToString(-1);
                                break;
                            case "quantity":
                                node.InnerText = Convert.ToString(-1);
                                break;
                        }
                        iRoot.AppendChild(iBase);
                    }
                }
                xInventory.AppendChild(iRoot);
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
        xInventory.Load(xDirectory + @"\" + "data" + iFileName);

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
                                if (Convert.ToInt32(iNode.InnerText) != -1)
                                {
                                    id = Convert.ToInt32(iNode.InnerText);
                                    isFilled = true;
                                }
                                else
                                    isFilled = false;
                                break;
                            case "quantity":
                                if (Convert.ToInt32(iNode.InnerText) != -1)
                                    quantity = Convert.ToInt32(iNode.InnerText);
                                break;
                        }
                        if (isFilled == true)
                            items[e] = new ItemSlot(database.GetItem[id], quantity, id);
                        else
                            items[e] = new ItemSlot();
                    }
                }
            }
            inventoryList[e] = items[e];
        }

        return inventoryList;
    }
    #endregion

    #region Save/Load Settings
    public void SaveSettings()
    {
        print("received message write Settings xml");
        XmlNode root = xSettings.FirstChild;
        foreach (XmlNode node in root.ChildNodes)
        {
            switch (node.Name)
            {
                case "masterV":
                    node.InnerText = masterVolume.ToString();
                    break;
                case "sfxV":
                    node.InnerText = SFXVolume.ToString();
                    break;
                case "musicV":
                    node.InnerText = musicVolume.ToString();
                    break;
                case "resolutionH":
                    node.InnerText = resolutionHeight.ToString();
                    break;
                case "resolutionW":
                    node.InnerText = resolutionWidth.ToString();
                    break;
                case "quality":
                    node.InnerText = quality.ToString();
                    break;
                case "windowed":
                    node.InnerText = isFullscreen.ToString();
                    break;
                case "level":
                    node.InnerText = level;
                    break;
            }
        }
        xSettings.Save(xDirectory + @"\" + "data" + sFileName);
    }

    public void LoadSettings()
    {
        print("received message load Settings xml");
        xSettings.Load(xDirectory + @"\" + "data" + sFileName);

        XmlNode root = xSettings.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            switch (node.Name)
            {
                case "masterV":
                    masterVolume = Convert.ToSingle(node.InnerText);
                    audioMixer.SetFloat("MasterVolume", masterVolume);
                    break;
                case "sfxV":
                    SFXVolume = Convert.ToSingle(node.InnerText);
                    audioMixer.SetFloat("SFXVolume", SFXVolume);
                    break;
                case "musicV":
                    musicVolume=Convert.ToSingle(node.InnerText);
                    audioMixer.SetFloat("MusicVolume", musicVolume);
                    break;
                case "isFullscreen":
                    isFullscreen = Convert.ToBoolean(node.InnerText);
                    Screen.fullScreen = isFullscreen;
                    break;
                case "resolutionH":
                    resolutionHeight = Convert.ToInt32(node.InnerText);
                    break;
                case "resolutionW":
                    resolutionWidth = Convert.ToInt32(node.InnerText);
                    Screen.SetResolution(resolutionWidth, resolutionHeight, isFullscreen);
                    break;
                case "quality":
                    quality = Convert.ToInt32(node.InnerText);
                    QualitySettings.SetQualityLevel(quality);
                    break;
                case "level":
                    level = node.InnerText;
                    break;
            }
        }
    }

    #endregion

    #region NewGame
    public void LoadNewGame()
    {
        LoadSettings();
        print("New Game");
        level = "Level1";
        isNew = true;
    }
    #endregion

    #region ContinueGame
    public void LoadSavedGame()
    {
        LoadSettings();
        print("Saved Game");
        isNew = false;
    }
    #endregion

    public void ResetLevel()
    {
        isNew = true;
        LoadSettings();
    }
}
