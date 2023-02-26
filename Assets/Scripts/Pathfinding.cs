using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public GameObject CtrlGrid;
    public int costStraight = 1, costDiag = 4;
    public float speedEnemy;

    [HideInInspector]
    public float dist;

    [HideInInspector]
    public Grid2D grid;
    
    Node2D enemyN, playerN;
    

    public void Start()
    {
        grid = CtrlGrid.GetComponent<Grid2D>();
    }

    public void Update()
    {
        //FindPlayer(enemy, player);
    }

    public void FindPlayer(Transform enemy, Transform player)
    {
        FindPath(enemy.transform.position, player.transform.position);
        dist = (enemy.transform.position - player.transform.position).magnitude;
        if (dist >= 0.4)
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, grid.pathFinded[0].truePosFromWorld, speedEnemy * Time.deltaTime);
    }

    //calculo do path ao contrario
    void RetracePath(Node2D start, Node2D end)
    {
        List<Node2D> pathFinded = new List<Node2D>();
        Node2D no = end;

        while (no != start)
        {
            pathFinded.Add(no);
            no = no.parent;
        }
        pathFinded.Reverse();

        grid.pathFinded = pathFinded;

    }

    //calculo da melhor distancia
    int Distance(Node2D nodeOne, Node2D nodeTwo)
    {
        int x = Mathf.Abs(nodeOne.GridX - nodeTwo.GridX);
        int y = Mathf.Abs(nodeOne.GridY - nodeTwo.GridY);

        if (x > y)
            return costDiag * y + costStraight * (x - y);

        return costDiag * x + costStraight * (y - x);
    }

    public void FindPath(Vector3 enemyPos, Vector3 playerPos)
    {
        //pegar os pontos do player e enemy para cada chamada da funcao
        enemyN = grid.pointWorld(enemyPos);
        playerN = grid.pointWorld(playerPos);

        List<Node2D> possibleNode = new List<Node2D>();
        HashSet<Node2D> noPossibleNode = new HashSet<Node2D>();
        possibleNode.Add(enemyN);
        
        //enquanto houver nodes
        while (possibleNode.Count > 0)
        {

            //calcular os custos
            Node2D node = possibleNode[0];
            for (int i = 1; i < possibleNode.Count; i++)
            {
                if (possibleNode[i].FCost <= node.FCost)
                {
                    if (possibleNode[i].hCost < node.hCost)
                        node = possibleNode[i];
                }
            }

            possibleNode.Remove(node);
            noPossibleNode.Add(node);

            //caso encontre o jogardor refazer o path ao contrario
            if (node == playerN)
            {
                RetracePath(enemyN, playerN);
                return;
            }

            //adicionando filhos possiveis
            foreach (Node2D nodeP in grid.nodeParents(node))
            {
                if (nodeP.collidable || noPossibleNode.Contains(nodeP))
                {
                    continue;
                }

                int newCostToNodeP = node.gCost + Distance(node, nodeP);
                if (newCostToNodeP < nodeP.gCost || !possibleNode.Contains(nodeP))
                {
                    nodeP.gCost = newCostToNodeP;
                    nodeP.hCost = Distance(nodeP, playerN);
                    nodeP.parent = node;

                    if (!possibleNode.Contains(nodeP))
                    {
                        possibleNode.Add(nodeP);
                    }
                }
            }
        }
    }
}