using System.Collections.Generic;
using UnityEngine;

public class Pathfinding :MonoBehaviour
{
    public static Pathfinding pathfinding;

    public int[,] Grid { set; get; }
       
    int rows = 200;
    int cols = 200;

    class Node
    {
        public int x, y;
        public int gCost; // coût réel
        public int hCost; // heuristique
        public int fCost => gCost + hCost;
        public Node parent;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public void Awake()
    {
        if (pathfinding == null)
            pathfinding = this;
    }
    public void Start()
    {
        Grid = TileGenerator.tileGenerator.WorldIntMatrice;
    }

    public List<Vector3Int> Launch(Vector2Int startPosition, Vector2Int endPosition)
    {
        var path = FindPath(startPosition, endPosition);
        List<Vector3Int> liste = new();
        if (path == null)
        {
            Debug.Log("Aucun chemin possible");
            return liste;
        }

        foreach (var node in path)
           liste.Add(new Vector3Int(node.x, node.y, 0));
        

        return liste;
    }


    List<Node> FindPath(Vector2Int start, Vector2Int end)
        {
            Node[,] nodes = new Node[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    nodes[i, j] = new Node(i, j);

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node startNode = nodes[start.x, start.y];
            Node endNode = nodes[end.x, end.y];

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node current = openList[0];

                // Trouver le meilleur node
                foreach (var node in openList)
                    if (node.fCost < current.fCost)
                        current = node;

                openList.Remove(current);
                closedList.Add(current);

                if (current == endNode)
                    return RetracePath(startNode, endNode);

                foreach (var neighbor in GetNeighbors(nodes, current))
                {
                    if (closedList.Contains(neighbor))
                        continue;

                    int newCost = current.gCost + Grid[neighbor.x, neighbor.y];
                    int turnPenalty = 0;

                    if (current.parent != null)
                    {
                        int dx1 = current.x - current.parent.x;
                        int dy1 = current.y - current.parent.y;

                        int dx2 = neighbor.x - current.x;
                        int dy2 = neighbor.y - current.y;

                        if (dx1 != dx2 || dy1 != dy2)
                            turnPenalty = 1; // pénalité de virage
                    }

                    newCost = current.gCost + Grid[neighbor.x, neighbor.y] + 10 - turnPenalty;
                if (newCost < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newCost;
                        neighbor.hCost = Heuristic(neighbor, endNode);
                        neighbor.parent = current;

                        if (!openList.Contains(neighbor))
                            openList.Add(neighbor);
                    }
                }
            }

            return null;
        }

        List<Node> RetracePath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            Node current = end;

            while (current != start)
            {
                path.Add(current);
                current = current.parent;
            }

            path.Add(start);
            path.Reverse();

            return path;
        }

        List<Node> GetNeighbors(Node[,] nodes, Node node)
        {
            List<Node> neighbors = new List<Node>();

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int nx = node.x + dx[i];
                int ny = node.y + dy[i];

                if (nx >= 0 && ny >= 0 && nx < rows && ny < cols)
                    if (Grid[nx, ny] >= 0)
                        neighbors.Add(nodes[nx, ny]);
            }

            return neighbors;
        }

        int Heuristic(Node a, Node b)
        {
            // Distance Manhattan (parfait pour grille)
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
}
