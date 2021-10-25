using UnityEngine;

public class ToggleActiveWithKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode = KeyCode.None;
    [SerializeField] private GameObject objectToToggle = null;

    public GameObject ObjectToToggle
    {
        get { return objectToToggle; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
