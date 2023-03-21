using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    #region member fields
    public Tile parent;
    public Tile connectedTile;
    public Character occupyingCharacter;

    public int costFromOrigin = 0;
    public int costToDestination = 0;
    public int terrainCost = 0;
    public int TotalCost { get { return costFromOrigin + costToDestination + terrainCost; } }
    
    [SerializeField]
    GameObject highlight;

    [SerializeField]
    bool debug;
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
    public void Highlight()
    {
        highlight.SetActive(true);
    }

    public void DebugCostText()
    {
        if(debug)
            costText.text = TotalCost.ToString();
    }

    public void ClearText()
    {
        costText.text = "";
    }

    /// <summary>
    /// Deactivates all children, removing all color
    /// </summary>
    public void ClearColor()
    {

        highlight.SetActive(false);

    }
}
