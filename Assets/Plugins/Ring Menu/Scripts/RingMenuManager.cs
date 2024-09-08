using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tellory.UI.RingMenu;
using UnityEngine.Events;

public class RingMenuManager : MonoBehaviour
{
    // Variables
    public static RingMenuManager Instance {get; private set; }
    public static bool IsActive { get; private set; }
    public static event System.Action OnActivated;
    public static event System.Action OnDeactivated;

    [Header("Settings")]

    [Tooltip("The minimum distance of the input position from the center of the screen to active the selection behaviour")]
    [SerializeField] private float m_minimumDistance = 100;

    [Header("Pointer")]
    [SerializeField] protected float m_pointerDistance = 64;
    [SerializeField] protected RectTransform m_pointerTransform;

    [Header("References")]
    [SerializeField] protected List<Item> m_items;
    [SerializeField] protected GameObject m_pivot;
    [SerializeField] protected RingLayoutGroup m_ringLayoutGroup;

    // Properties
    public RingLayoutGroup RingLayoutGroup => m_ringLayoutGroup;

    public float MinimumDistance => m_minimumDistance;

    public float InputDistance { get; set; }
    public float InputAngle { get; set; }

    protected RingMenuItem CurrentItem { get; private set; }

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        m_pivot.SetActive(false);

        IsActive = false;

        // If the instance is already set and it's not this one, destroy the duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        m_ringLayoutGroup.ReplaceItems(m_items);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        if (!IsActive) return;

        HandlePointerPosition();
        HandleInputAngle();
    }

    /// <summary>
    /// Handle the input angle and controls the behaviour of all the items.
    /// </summary>
    private void HandleInputAngle()
    {
        var item = m_ringLayoutGroup.GetItemOnAngle(InputAngle);
        if (item != CurrentItem)
        {
            if (CurrentItem) CurrentItem.OnExit();
            if (item) item.OnEnter();

            CurrentItem = item;
        }
    }

    /// <summary>
    /// Try to confirm the selection of the item in the correct angle.
    /// </summary>
    public void TryConfirm()
    {
        if (!CurrentItem) return;
        CurrentItem.OnConfirm();
    }

    /// <summary>
    /// Refresh the pointer position using the input distance / angle.
    /// </summary>
    private void HandlePointerPosition()
    {
        float distance = Mathf.InverseLerp(0, m_minimumDistance, InputDistance) * m_pointerDistance;
        float radian = InputAngle * Mathf.Deg2Rad;

        Vector2 position;
        position.x = Mathf.Cos(radian) * distance;
        position.y = Mathf.Sin(radian) * distance;

        m_pointerTransform.anchoredPosition = position;
    }

    /// <summary>
    /// Add an item to the ring menu.
    /// </summary>
    public virtual void AddItem(Item item)
    {
        m_items.Add(item);
        m_ringLayoutGroup.ReplaceItems(m_items);
    }

    /// <summary>
    /// Remove an item to the ring menu.
    /// </summary>
    public virtual void RemoveItem(Item item)
    {
        m_items.Remove(item);
        m_ringLayoutGroup.ReplaceItems(m_items);
    }

    /// <summary>
    /// Used to replace all the items in the current active layout group.
    /// </summary>
    public virtual void ReplaceItems(List<Item> items)
    {
        m_items.Clear();
        foreach (Item item in items) m_items.Add(item);
        m_ringLayoutGroup.ReplaceItems(m_items);
    }

    /// <summary>
    /// Refresh the layout group using the items list.
    /// </summary>
    public virtual void RefreshLayout() => m_ringLayoutGroup.RefreshLayout();

    /// <summary>
    /// Remove all the items in the ring menu.
    /// </summary>
    public virtual void ResetMenu()
    {
        if (m_items == null) return;
        m_items.Clear();
    }

    /// <summary>
    /// Open the ring menu without animation.
    /// Can be overridden to put an animation on it.
    /// </summary>
    public virtual void TryOpen()
    {
        if (IsActive) return;

        m_pivot.SetActive(true);
        IsActive = true;

        OnActivated?.Invoke();

        InputDistance = 0;
    }

    /// <summary>
    /// Close the ring menu without animation.
    /// Can be overridden to put an animation on it.
    /// </summary>
    public virtual void TryClose()
    {
        if (!IsActive) return;

        m_pivot.SetActive(false);
        IsActive = false;

        OnDeactivated?.Invoke();
    }
}

namespace Tellory.UI.RingMenu
{
    [CreateAssetMenu(fileName = "New UI Item", menuName = "ARCOS Elevator/UI Item")]
    [System.Serializable]
    public class Item : ScriptableObject
    {
        // Events
        public event PressAction OnClick;

        // Variables
        [SerializeField] private string m_name;
        [SerializeField] private Sprite m_icon;
        //[SerializeField] private UnityEvent m_event;

        // Properties
        public string Name => m_name;
        public Sprite Icon => m_icon;

        // Constructor
        public Item(string name, Sprite icon, PressAction call)
        {
            m_name = name;
            m_icon = icon;

            OnClick = call;
            //m_event = null;
        }

        // Methods
        public void ExecuteAction()
        {
            OnClick?.Invoke();
            //m_event?.Invoke();
        }

        // Delegates
        public delegate void PressAction();
    }
}