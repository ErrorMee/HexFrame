﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class GroundView : ViewBase
{ 
    [SerializeField]
    private Image prefab;
    [SerializeField]
    private Transform cellList;

    private int otherness = 3;

    private bool firstCreateNode = true;

    private void Start()
    {
        HexGrid grid = HexGridModel.Instance.CreateEmptyGrid(11, 13);
        grid.TraversalNodes(CreateNode);
    }

    private void CreateNode(HexNode node)
    {
        Image cell;
        if (firstCreateNode)
        {
            firstCreateNode = false;
            cell = prefab;
        } else
        {
            cell = Instantiate<Image>(prefab);
        }

        cell.transform.SetParent(cellList, false);
        
        cell.transform.localPosition = node.GetViewCoord();

        int v = (int)(node.ArrayCoord.y) % otherness;//0 1 2

        if ((int)(node.ArrayCoord.x) % 2 != 0)
        {
            v--;
            if (v < 0)
            {
                v = otherness - 1;
            }
        }

        cell.color = new Color(cell.color.r, cell.color.g, cell.color.b, cell.color.a + 0.02f * v);

        //cell.isStatic = true;
    }
}