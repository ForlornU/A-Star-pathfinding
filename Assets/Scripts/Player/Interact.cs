using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Interact : MonoBehaviour
{
    #region member fields
    [SerializeField]
    AudioClip click, pop;
    [SerializeField]
    LayerMask interactMask;

    //Debug purposes only
    [SerializeField]
    bool debug;
    Path Lastpath;

    Camera mainCam;
    Tile currentTile;
    Character selectedCharacter;
    Pathfinder pathfinder;
    #endregion

    private void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();

        if (pathfinder == null)
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
    }

    private void Update()
    {
        Clear();
        MouseUpdate();
    }

    private void MouseUpdate()
    {
        if (!Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f, interactMask))
            return;

        currentTile = hit.transform.GetComponent<Tile>();
        InspectTile();
    }

    private void InspectTile()
    {
        //Alter cost by right clicking
        if (Input.GetMouseButtonUp(1))
        {
            currentTile.ModifyCost();
            return;
        }

        if (currentTile.Occupied)
            InspectCharacter();
        else
            NavigateToTile();
    }

    private void InspectCharacter()
    {
        if (currentTile.occupyingCharacter.Moving)
            return;

        currentTile.Highlight();

        if (Input.GetMouseButtonDown(0))
            SelectCharacter();
    }

    private void Clear()
    {
        if (currentTile == null  || currentTile.Occupied == false)
            return;

        //currentTile.ModifyCost(currentTile.terrainCost-1);//Reverses to previous cost and color after being highlighted
        currentTile.ClearHighlight();
        currentTile = null;
    }

    private void SelectCharacter()
    {
        selectedCharacter = currentTile.occupyingCharacter;
        GetComponent<AudioSource>().PlayOneShot(pop);
    }

    private void NavigateToTile()
    {
        if (selectedCharacter == null || selectedCharacter.Moving == true)
            return;

        if (RetrievePath(out Path newPath))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<AudioSource>().PlayOneShot(click);
                selectedCharacter.StartMove(newPath);
                selectedCharacter = null;
            }
        }
    }

    bool RetrievePath(out Path path)
    {
        path = pathfinder.FindPath(selectedCharacter.characterTile, currentTile);
        
        if (path == null || path == Lastpath)
            return false;

        //Debug only
        if (debug)
        {
            ClearLastPath();
            DebugNewPath(path);
            Lastpath = path;
        }
        return true;
    }

    //Debug only
    void ClearLastPath()
    {
        if (Lastpath == null)
            return;

        foreach (Tile tile in Lastpath.tiles)
        {
            tile.ClearText();
        }
    }

    //Debug only
    void DebugNewPath(Path path)
    {
        foreach (Tile t in path.tiles)
        {
            t.DebugCostText();
        }
    }
}