using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    public Vector3 gridWorldSize;
    public float nodeRadius, distancePerGrid;
    public Node2D[,] Grid;
    public Tilemap obstaclemap;
    public List<Node2D> pathFinded;
    Vector3 worldBottomLeft;

    float nodeDiameter;
    public int gridSizeX, gridSizeY;

    void Awake(){
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        newGrid();
    }

    

    void newGrid(){
        Grid = new Node2D[gridSizeX, gridSizeY];
        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                Grid[x, y] = new Node2D(false, worldPoint, x, y);

                if (obstaclemap.HasTile(obstaclemap.WorldToCell(Grid[x, y].truePosFromWorld)))
                    Grid[x, y].tilemapCollidable(true);
                else
                    Grid[x, y].tilemapCollidable(false);
            }
        }
    }

    public List<Node2D> nodeParents(Node2D a_NeighborNode){
        List<Node2D> NeighborList = new List<Node2D>();
        int vX;
        int vY;

        //Nodes da direita
        vX = a_NeighborNode.GridX + 1;
        vY = a_NeighborNode.GridY;
        if (vX >= 0 && vX < gridSizeX)
        {
            if (vY >= 0 && vY < gridSizeY)
            {
                NeighborList.Add(Grid[vX, vY]);
            }
        }
        //Nodes da esquerda
        vX = a_NeighborNode.GridX - 1;
        vY = a_NeighborNode.GridY;
        if (vX >= 0 && vX < gridSizeX)
        {
            if (vY >= 0 && vY < gridSizeY)
            {
                NeighborList.Add(Grid[vX, vY]);
            }
        }
        //Nodes de cima
        vX = a_NeighborNode.GridX;
        vY = a_NeighborNode.GridY + 1;
        if (vX >= 0 && vX < gridSizeX)
        {
            if (vY >= 0 && vY < gridSizeY)
            {
                NeighborList.Add(Grid[vX, vY]);
            }
        }
        //Nodes de baixo
        vX = a_NeighborNode.GridX;
        vY = a_NeighborNode.GridY - 1;
        if (vX >= 0 && vX < gridSizeX)
        {
            if (vY >= 0 && vY < gridSizeY)
            {
                NeighborList.Add(Grid[vX, vY]);
            }
        }

        return NeighborList;
    }
 
    public Node2D pointWorld(Vector3 a_vWorldPos)
    {
        float x = ((a_vWorldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float y = ((a_vWorldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);

        x = Mathf.Clamp01(x);
        y = Mathf.Clamp01(y);

        int xX = Mathf.RoundToInt((gridSizeX - 1) * x);
        int yY = Mathf.RoundToInt((gridSizeY - 1) * y);

        return Grid[xX, yY];
    }

    //Debug, desenhar representacao do grid e seu funcionamento
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (Grid != null)
        {
            foreach (Node2D n in Grid)
            {
                if (n.collidable)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.black;

                if (pathFinded != null && pathFinded.Contains(n))
                    Gizmos.color = Color.white;
                Gizmos.DrawCube(n.truePosFromWorld, Vector3.one * (nodeDiameter - distancePerGrid));

            }
        }
    }
}
