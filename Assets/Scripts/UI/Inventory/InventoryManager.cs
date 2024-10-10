using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    private bool visibleInventoryPanel;

    private void Start()
    {
        inventoryPanel.SetActive(visibleInventoryPanel);
    }

    public void OnToggle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            visibleInventoryPanel = !visibleInventoryPanel;
            inventoryPanel.SetActive(visibleInventoryPanel);
        }
    }
}
