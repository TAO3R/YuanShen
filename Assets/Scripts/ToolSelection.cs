using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolSelection : MonoBehaviour
{
    [Tooltip("玩家的背包脚本")] [SerializeField] PlayerBackpack backpackScript;

    [SerializeField] private int ladderMax, oxygenCylinderMax, drillMax, extinguisherMax;
    [SerializeField] private float ladderLoad, oxygenCylinderLoad, drillLoad, extinguisherLoad;

    [SerializeField] private int oxygenCylinderNeeded = 3;

    [SerializeField] private float currentLoad = 0f;
    [SerializeField] private float maxLoad = 25f;

    [SerializeField] private TMPro.TextMeshProUGUI loadText, ladderNumberText, oxygenCylinderNumberText, drillNumberText, extinguisherNumberText;

    [SerializeField] private GameObject backpack;

    #region MonoBehavior Callbacks

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < oxygenCylinderNeeded; i++)
        {
            AddOxygenCylinder();
        }
        
        UpdateLoadText();
        UpdateLadderNumberText();
        UpdateOxygenCylinderNumberText();
        UpdateExtinguisherNumberText();
        UpdateDrillNumberText();
    }

    #endregion

    private void UpdateLoadText()
    {
        loadText.text = currentLoad.ToString() + "Kg / " + maxLoad.ToString() + "Kg";
    }

    #region Ladder

    public void AddLadder()
    {
        if (currentLoad + ladderLoad <= maxLoad && backpackScript.ToolsRemaining[(int)tools.ladder] < ladderMax)    // 不超重且不超过道具数量上限
        {
            backpackScript.ToolsRemaining[(int)tools.ladder]++;
            currentLoad += ladderLoad;

            UpdateLadderNumberText();
            UpdateLoadText();

            Debug.Log("Ladder added");
        }
    }

    public void RemoveLadder()
    {
        if (backpackScript.ToolsRemaining[(int)tools.ladder] > 0)
        {
            backpackScript.ToolsRemaining[(int)tools.ladder]--;
            currentLoad -= ladderLoad;

            UpdateLadderNumberText();
            UpdateLoadText();

            Debug.Log("Ladder removed");
        }
    }

    private void UpdateLadderNumberText()
    {
        ladderNumberText.text = backpackScript.ToolsRemaining[(int)tools.ladder].ToString() + " / " + ladderMax.ToString();
    }

    #endregion

    #region Oxygen Cylinder

    private void AddOxygenCylinder()
    {
        if (currentLoad + oxygenCylinderLoad <= maxLoad && backpackScript.ToolsRemaining[(int)tools.oxygenCylinder] < oxygenCylinderMax)    // 不超重且不超过道具数量上限
        {
            backpackScript.ToolsRemaining[(int)tools.oxygenCylinder]++;
            currentLoad += oxygenCylinderLoad;

            UpdateOxygenCylinderNumberText();
            UpdateLoadText();

            Debug.Log("Oxygen cylinder added");
        }
    }

    private void RemoveOxygenCylinder()
    {
        if (backpackScript.ToolsRemaining[(int)tools.oxygenCylinder] > 0)
        {
            backpackScript.ToolsRemaining[(int)tools.oxygenCylinder]--;
            currentLoad -= oxygenCylinderLoad;

            UpdateOxygenCylinderNumberText();
            UpdateLoadText();

            Debug.Log("Oxygen cylinder removed");
        }
    }

    private void UpdateOxygenCylinderNumberText()
    {
        oxygenCylinderNumberText.text = backpackScript.ToolsRemaining[(int)tools.oxygenCylinder].ToString() + " / " + oxygenCylinderMax.ToString();
    }

    #endregion

    #region Drill

    public void AddDrill()
    {
        if (currentLoad + drillLoad <= maxLoad && backpackScript.ToolsRemaining[(int)tools.drill] < drillMax)    // 不超重且不超过道具数量上限
        {
            backpackScript.ToolsRemaining[(int)tools.drill]++;
            currentLoad += drillLoad;

            UpdateDrillNumberText();
            UpdateLoadText();

            Debug.Log("Drill added");
        }
    }

    public void RemoveDrill()
    {
        if (backpackScript.ToolsRemaining[(int)tools.drill] > 0)
        {
            backpackScript.ToolsRemaining[(int)tools.drill]--;
            currentLoad -= drillLoad;

            UpdateDrillNumberText();
            UpdateLoadText();

            Debug.Log("Drill removed");
        }
    }

    private void UpdateDrillNumberText()
    {
        drillNumberText.text = backpackScript.ToolsRemaining[(int)tools.drill].ToString() + " / " + drillMax.ToString();
    }

    #endregion

    #region Extinguisher

    public void AddExtinguisher()
    {
        if (currentLoad + extinguisherLoad <= maxLoad && backpackScript.ToolsRemaining[(int)tools.extinguisher] < extinguisherMax)    // 不超重且不超过道具数量上限
        {
            backpackScript.ToolsRemaining[(int)tools.extinguisher]++;
            currentLoad += extinguisherLoad;

            UpdateExtinguisherNumberText();
            UpdateLoadText();

            Debug.Log("Extinguisher added");
        }
    }

    public void RemoveExtinguisher()
    {
        if (backpackScript.ToolsRemaining[(int)tools.extinguisher] > 0)
        {
            backpackScript.ToolsRemaining[(int)tools.extinguisher]--;
            currentLoad -= extinguisherLoad;

            UpdateExtinguisherNumberText();
            UpdateLoadText();

            Debug.Log("Extinguisher removed");
        }
    }

    private void UpdateExtinguisherNumberText()
    {
        extinguisherNumberText.text = backpackScript.ToolsRemaining[(int)tools.extinguisher].ToString() + " / " + extinguisherMax.ToString();
    }

    #endregion

    public void ConfirmToolSelection()
    {
        backpack.SetActive(false);
        GameManager._instance.start = true;
        backpackScript.ToolUIInitialization();
    }
}
