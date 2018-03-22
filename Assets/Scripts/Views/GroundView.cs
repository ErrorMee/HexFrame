using UnityEngine;
using System;

public class GroundView : ViewBase
{ 
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform cellList;

    private bool firstCreateNode = true;

    private void Start()
    {
        HexGrid grid = HexGridModel.Instance.CreateEmptyGrid(9, 13);
        grid.TraversalNodes(CreateNode);
    }

    private void CreateNode(HexNode node)
    {
        GameObject cell;
        if (firstCreateNode)
        {
            firstCreateNode = false;
            cell = prefab;
        } else
        {
            cell = Instantiate<GameObject>(prefab);
        }

        cell.transform.SetParent(cellList, false);
        
        cell.transform.localPosition = node.GetViewCoord();

        //cell.isStatic = true;
    }
}