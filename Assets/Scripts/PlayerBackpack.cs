using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions.Must;

public enum tools { ladder, oxygenCylinder, drill, extinguisher}

public class PlayerBackpack : MonoBehaviour
{
    #region Tool-related Variables

    [Header("工具相关")]

    [Tooltip("记录每种道具的剩余数量")] [SerializeField] private int[] toolsRemaining;
    public int[] ToolsRemaining
    {
        get { return this.toolsRemaining; }
        set { this.toolsRemaining = value; }
    }

    [Tooltip("存放每种道具的UI")][SerializeField] private Sprite[] toolsUISprites;

    [Tooltip("游戏内道具UI")][SerializeField] private GameObject rescueSessionToolUI;
    [Tooltip("道具槽里显示当前所选道具的Image组件")][SerializeField] private Image currentToolImage;
    [Tooltip("当前所选道具剩余数量")][SerializeField] private TMPro.TextMeshProUGUI currentToolNumberText;

    [SerializeField] private tools currentTool = tools.ladder; // 当前选择的道具
    public tools CurrentTool
    {
        get { return this.currentTool; }
        set { currentTool = value; }
    }

    [SerializeField] private KeyCode switchTool, useTool, withdrawTool;     // 操作按键

    [SerializeField] private Transform toolShadows;
    public Transform ToolShadows
    {
        get { return this.toolShadows; }
        set { this.toolShadows = value; }
    }

    #endregion

    #region Interaction-related Variables

    [Header("交互相关")]

    [SerializeField] private List<GameObject> interactables;
    public List<GameObject> Interactables
    {
        get { return this.interactables; }
        set { this.interactables = value; }
    }

    [Tooltip("当前场景内的潜在交互对象")] [SerializeField] private GameObject toInteractWith;
    public GameObject ToInteractWith
    {
        get { return this.toInteractWith; }
        set { this.toInteractWith = value; }
    }

    public static PlayerBackpack _instance;

    [SerializeField] private GameObject yuanAnimPref;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _instance = this;

        // 初始化背包
        toolsRemaining = new int[Enum.GetNames(typeof(tools)).Length];

        // 初始化道具数量为0
        for (int i = 0; i < toolsRemaining.Length; i++)
        {
            toolsRemaining[i] = 0;
        }
    }

    void Start()
    {
        toInteractWith = null;

        // Disable tool shadows at the start
        for (int i = 0; i < toolShadows.childCount; i++)
        {
            toolShadows.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 工具相关
        if (Input.GetKeyDown(switchTool))   // 使用了工具
        {
            Debug.Log("Key pressed down to switch tool.");

            // Deavtivate current tool shadow
            toolShadows.GetChild((int)currentTool).gameObject.SetActive(false);

            SwitchTool();

            // UI
            if (!IsEmptyBackpack())
            {
                currentToolImage.sprite = toolsUISprites[(int)currentTool];
                // currentToolImage.SetNativeSize();
                
                toolShadows.GetChild((int)currentTool).gameObject.SetActive(true);
            }
            else
            {
                currentToolImage.sprite = null;
            }

            UpdateToolRemainingText();

            // Tool Shadow
            /*if (currentTool == tools.ladder)
            {
                ladderShadow.SetActive(true);
            }
            else
            {
                ladderShadow.SetActive(false);
            }*/
            

            // Audio
            // 
        }

        // 交互相关
        GameObject tempInteractable = GetInteractable();
        
        if (tempInteractable == null)
        {
            if (toInteractWith != null)
            {   // 不存在新的交互物且上一帧的交互物不为null，取消上一帧交互物的高亮显示
                if (toInteractWith.GetComponent<Fire>() != null)
                {
                    toInteractWith.GetComponent<Fire>().CancelHighLight();
                }
                if (toInteractWith.GetComponent<Wall>())
                {
                    toInteractWith.GetComponent<Wall>().CancelHighLight();
                }
                if (toInteractWith.GetComponent<InjuredPerson>())
                {
                    toInteractWith.GetComponent<InjuredPerson>().CancelHighLight();
                }
                if (toInteractWith.GetComponent<LadderIsPlaced>())
                {
                    toInteractWith.GetComponent<LadderIsPlaced>().CancelHighLight();
                }
            }

            // 更新交互物
            toInteractWith = null;
        }
        else
        {   // 存在潜在交互物
            if (toInteractWith != tempInteractable)
            {   // 潜在交互物和上一帧交互物不同
                if (toInteractWith != null)
                {   // 上一帧交互物不为NULL，取消上一帧交互物的高亮显示
                    if (toInteractWith.GetComponent<Fire>() != null)
                    {
                        toInteractWith.GetComponent<Fire>().CancelHighLight();
                    }
                    if (toInteractWith.GetComponent<Wall>())
                    {
                        toInteractWith.GetComponent<Wall>().CancelHighLight();
                    }
                    if (toInteractWith.GetComponent<InjuredPerson>())
                    {
                        toInteractWith.GetComponent<InjuredPerson>().CancelHighLight();
                    }
                    if (toInteractWith.GetComponent<LadderIsPlaced>())
                    {
                        toInteractWith.GetComponent<LadderIsPlaced>().CancelHighLight();
                    }
                }

                // 更新交互物
                toInteractWith = tempInteractable;

            /*if (toInteractWith != null)
            {*/
                // 描边method或者激活描边图层 更新（如有
                if (toInteractWith.GetComponent<Fire>())
                {
                    toInteractWith.GetComponent<Fire>().HighLight();
                }
                if (toInteractWith.GetComponent<Wall>())
                {
                    toInteractWith.GetComponent<Wall>().HighLight();
                }
                if (toInteractWith.GetComponent<InjuredPerson>())
                {
                    toInteractWith.GetComponent<InjuredPerson>().HighLight();
                }
                if (toInteractWith.GetComponent<LadderIsPlaced>())
                {
                    toInteractWith.GetComponent<LadderIsPlaced>().HighLight();
                }
            /*}*/
            }
        }
    } // End of Update()

    private void LateUpdate()
    {
        // 把交互检测放在lateupdate是因为需要提前确定当前帧的潜在交互物
        if (Input.GetKeyDown(useTool))
        {
            Interact();
        }
    }

    #endregion

    #region Methods

    private void SwitchTool()   // 切换道具
    {
        if (IsEmptyBackpack())  // 当前背包为空，不显示道具 -> 空槽
        {
            return;
        }

        NextTool();

        while (ToolsRemaining[(int)currentTool] == 0)
        {
            NextTool();
        }

        // return toolsUISprites[(int)currentTool];
    }

    private void NextTool()
    {
        currentTool = (tools)(((int)(currentTool + 1)) % toolsRemaining.Length);
    }

    public void ToolUIInitialization()  // Called when the player presses the "comfirm" button to end the tool selection phase
    {
        rescueSessionToolUI.SetActive(true);
        
        /*currentToolImage.sprite = toolsUISprites[(int)currentTool];

        if (toolsRemaining[(int)currentTool] == 0)  // 如果背包编辑阶段结束后玩家背包的默认道具数量为0
        {
            Sprite temp = SwitchTool();

            currentToolImage.sprite = temp;
            if (temp != null)
            {
                currentToolImage.SetNativeSize();
            }
            UpdateToolRemainingText();

            return;
        }*/

        while (ToolsRemaining[(int)currentTool] == 0)
        {
            NextTool();
        }

        // Activate corresponding tool shadow
        toolShadows.GetChild((int)currentTool).gameObject.SetActive(true);

        currentToolImage.sprite = toolsUISprites[(int)currentTool];     // UI image
        currentToolImage.SetNativeSize();                               // Size of UI image
        UpdateToolRemainingText();                                      // Tool number
    }

    private GameObject GetInteractable()    // 返回当前场景中符合条件的距离玩家最近的可交互物
    {
        if (interactables.Count == 0) { return null; }  // 没有可交互物，返回null
        
        GameObject temp = null;

        // 将可互动物在List内按照离玩家的距离从小到大排序
        interactables.Sort(new DistanceComparer(transform.position));

        // 如果物品是梯子，则无论当前选择的是什么道具，都可以进行交互 -> 收回梯子；
        // 否则判断物品类型是否匹配当前选择的道具，如果匹配则返回该物品；
        // 如果两项都不满足，则i++，进入下一次判断
        for (int i = 0; i < interactables.Count; i++)
        {
            if ((currentTool != tools.ladder && interactables[i].tag == "Ladder") || 
                (currentTool == tools.oxygenCylinder && interactables[i].tag == "Person") ||
                (currentTool == tools.drill && interactables[i].tag == "Wall") ||
                (currentTool == tools.extinguisher && interactables[i].tag == "Fire"))
            {
                temp = interactables[i];
                break;
            }
        }

        return temp;
    }

    private void UpdateInteractable(GameObject _newInteractable)
    {
        if (_newInteractable == toInteractWith) { return; }


    }

    private void Interact()
    {
        if (IsEmptyBackpack()) { return; } // 背包为空则return

        if (currentTool == tools.ladder && toolShadows.GetChild((int)tools.ladder).gameObject.activeSelf)   // 使用梯子
        {
            UseCurrentTool();
            // 放置梯子
            toolShadows.GetChild((int)tools.ladder).GetComponent<Ladder>().Use();

            return;
        }

        if (interactables.Count == 0) { return; }   // 场景中不存在可交互物，返回

        if (currentTool == tools.oxygenCylinder)    // 使用氧气瓶
        {
            if (toInteractWith.GetComponent<LadderIsPlaced>())
            {
                RecycleLadder();
            }
            else
            {
                UseCurrentTool();
                // 伤者被救起
                // toInteractWith.GetComponent<InjuredPerson>().BeHelp();
                // play genshin
                GameObject yuanAnim = Instantiate(yuanAnimPref, Vector3.zero, Quaternion.identity);
                yuanAnim.GetComponent<Yuan>().InjuredPersonScript = toInteractWith.GetComponent<InjuredPerson>();
            }
        }
        else if (currentTool == tools.extinguisher) // 使用灭火器
        { 
            if (toInteractWith.GetComponent<LadderIsPlaced>())
            {
                RecycleLadder();
            }
            else
            {
                UseCurrentTool();
                // 火被扑灭
                toInteractWith.GetComponent<Fire>().PutOutFire();
            }
        }
        else if (currentTool == tools.drill)    // 使用电钻
        { 
            if (toInteractWith.GetComponent<LadderIsPlaced>())
            {
                RecycleLadder();
            }
            else
            {
                // 墙被钻通
                toInteractWith.GetComponent<Wall>().WallAni();
            }
        }
    } // End of Interact()

    private void UpdateToolRemainingText()
    {
        Debug.Log("Current tool number updated.");
        currentToolNumberText.text = toolsRemaining[(int)currentTool].ToString();
    }

    private bool HasCurrentTool()
    {
        return toolsRemaining[(int)currentTool] > 0;
    }

    private bool IsEmptyBackpack()
    {
        foreach (int i in toolsRemaining)
        {
            if (i > 0) { return false; }
        }

        return true;
    }

    private void UseCurrentTool()
    {
        // 更新当前道具剩余数量
        toolsRemaining[(int)currentTool]--;

        if (!HasCurrentTool())  // 如果当前道具用尽
        {
            Debug.Log("Current tool used up, perform auto tool switching");
            // 停用当前工具虚影
            toolShadows.GetChild((int)currentTool).gameObject.SetActive(false);
            // 切换工具
            SwitchTool();
            // 更新当前工具图标UI
            currentToolImage.sprite = toolsUISprites[(int)currentTool];
            // 启用当前工具虚影
            toolShadows.GetChild((int)currentTool).gameObject.SetActive(true);
        }

        // 更新当前工具剩余数量UI
        UpdateToolRemainingText();
    }

    private void RecycleLadder()
    {
        // 回收梯子
        toInteractWith.GetComponent<LadderIsPlaced>().Recycle();
        // 更新梯子剩余数量
        toolsRemaining[(int)tools.ladder]++;

        // 如果回收梯子前背包为空
        if (currentToolImage.sprite == null)
        {
            // 更新当前工具图标UI
            currentToolImage.sprite = toolsUISprites[(int)tools.ladder];
            // 更新当前工具
            currentTool = tools.ladder;
        }

        // 更新当前工具剩余数量UI
        UpdateToolRemainingText();
    }

    #endregion
}
