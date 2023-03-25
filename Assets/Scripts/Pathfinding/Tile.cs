using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    public bool Occupied { get; set; } = false;
    [SerializeField]
    TMP_Text costText;
    #endregion

    Dictionary<int, Color> costMap = new Dictionary<int, Color>()
    {
        {0, new Color(0.42f, 0.38f, 0.38f) }, //Gray
        {1, new Color(0.45f, 0.23f, 0.15f) },//Red
        {2, new Color(0.3f, 0.1f, 0f) } //Dark red
    };

    /// <summary>
    /// Changes color of the tile by activating child-objects of different colors
    /// </summary>
    /// <param name="col"></param>
    public void Highlight()
    {
        SetColor(Color.white);
    }

    public void ClearHighlight()
    {
        SetColor(costMap[terrainCost]);
    }

    /// <summary>
    /// This is called when right clicking a tile to increase its cost
    /// </summary>
    /// <param name="value"></param>
    public void ModifyCost()
    {
        terrainCost++;
        if (terrainCost > costMap.Count-1)
            terrainCost = 0;

        if (costMap.TryGetValue(terrainCost, out Color col))
        {
            SetColor(col);
        }
        else
        {
            Debug.Log("Invalid terraincost or mapping" + terrainCost);
        }
    }

    private void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
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
