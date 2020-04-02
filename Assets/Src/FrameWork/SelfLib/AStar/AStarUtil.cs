using System;
using System.Collections.Generic;

namespace AStar
{
    /*
     *    A* 计算节点函数
     *    FinalN = BeginN+EndN
     *     总权值 = 从开始点的权值 + 结束点到当前点的权值
     * 
     */
    public static class AStarUtil
    {
        public static int MaxX = 100;
        public static int MaxY = 100;
        
        public static int[,] MapArr = new int[100,100];
        
        public static int CalcEndN(Node current, Node dest)
        {
            return Math.Abs(current.X - dest.X) + Math.Abs(current.Y - dest.Y);
        }

        public static List<Node> AStarFind(Node start, Node goal)
        {
            int[,] MapArr = new int[100, 100];
            Node[,] allNode = new Node[100, 100];
            //初始化节点
            for (int i = 0; i < MaxX; i++)
            {
                for (int j = 0; j < MaxY; j++)
                {
                    allNode[i, j] = new Node(i, j, MapArr[i, j] == 0);
                }
            }

            
            var openList =GetRoundNodes(start, allNode);
            var closeList = new List<Node>();
            var fScore = CalcEndN(start, goal);

            while (openList.Count != 0)
            {
                var current = GetMinF(openList);
                if (current.PosEqual(goal))
                {
                    return IteratorParent(current);
                }

                openList.Remove(current);
                closeList.Add(current);

//                var neighbor = GetRoundNodes(current);
//                for (int i = 0; i < neighbor.Count; i++)
//                {
//                    if (closeList.Contains())
//                    {
//                        
//                    }
//                }
            }
            
            return null;
        }

        private static List<Node> IteratorParent(Node current)
        {
            List<Node> list = new List<Node>();

            var temp = current;
            while (temp.Parent != null)
            {
                list.Add(temp.Parent);
                temp = temp.Parent;
            }

            return list;
        }

        /// 找到openList中Fn最小值
        public static Node GetMinF(List<Node> openList)
        {
            Node target = openList[0];
            for (var index = 1; index < openList.Count; index++)
            {
                var t = openList[index];
                if (t.Fn < target.Fn)
                {
                    target = t;
                }
            }

            return target;
        }

        public static List<Node> GetRoundNodes(Node node, Node[,] allNode)
        {
            List<Node> round = new List<Node>();
            
//            //up
//            
//            if (node.Y + 1 < MaxY)
//            {
//                var t = allNode[node.X, node.Y + 1];
//                if (t.Walkable)
//                {
//                    t.Gn = 10;
//                    t.Hn = CalcEndN(t,)
//                }
//                round.Add(allNode[]);
//            }
//            //down
//            if (node.Y - 1 >= 0 && MapArr[node.X, node.Y - 1] == 0)
//            {
//                round.Add(new AStarNode(node.X, node.Y - 1));
//            }
//            //left
//            if (node.X - 1 >= 0 && MapArr[node.X - 1, node.Y] == 0)
//            {
//                round.Add(new AStarNode(node.X - 1, node.Y));
//            }
//            //right
//            if (node.X + 1 < MaxX && MapArr[node.X +1, node.Y] == 0)
//            {
//                round.Add(new AStarNode(node.X + 1, node.Y));
//            }

            return round;
        }
    }
}