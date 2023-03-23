using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    #region member fields
    public Tile parent;
    public Tile connectedTile;
    public Character occupyingCharacter;

    public float costFromOrigin = 0;
    public float costToDestination = 0;
    public int terrainCost = 0;
    public float TotalCost { get { return costFromOrigin + costToDestination + terrainCost; } }
    
    [SerializeField]
    GameObject highlight;

    [SerializeField]
    TMP_Text costText;

    public bool Occupied { get; set; } = false;
    public bool InFrontier { get; set; } = false;
    public bool CanBeReached { get { return !Occupied && InFrontier; } }
    #endregion

    /// <summary>
    /// Changes color of the tile by activating child-objects of different colors
    /// </summary>
    /// <param name="col"></param>
    public void Highlight(bool state)
    {
        highlight.SetActive(state);
    }

    public void DebugCostText()
    {
        costText.text = TotalCost.ToString("F1");
    }

    public void ClearText()
    {
        costText.text = "";
    }
}
