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
    #endregion

    /// <summary>
    /// Changes color of the tile by activating child-objects of different colors
    /// </summary>
    /// <param name="col"></param>
    public void Highlight(bool state)
    {
        highlight.SetActive(state);
    }

    public void ModifyCost()
    {
        switch (terrainCost)
        {
            case 0:
                GetComponent<MeshRenderer>().material.color = new Color(0.45f, 0.23f, 0.15f);
                terrainCost = 1;
                break;
            case 1:
                GetComponent<MeshRenderer>().material.color = Color.red;
                terrainCost = 2;
                break;
            case 2:
                GetComponent<MeshRenderer>().material.color = new Color(0.42f, 0.38f, 0.38f);
                terrainCost = 0;
                break;
            default:
                break;
        }
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
