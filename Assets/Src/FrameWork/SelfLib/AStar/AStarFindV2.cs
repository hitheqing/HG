using System;
using System.Collections.Generic;
using HG;

namespace AStar
{
    /// A*寻路类，只能横竖行走，四个方向
    public class AStarFindV2
    {
        private Node _start;
        private Node _target;
        private byte[,] _mapArr;
        private Node[,] _nodeArr;

        private readonly int XMax;
        private readonly int YMax;

        /// 待检查的节点列表
        private List<Node> openList = new List<Node>();
        
        /// 已经检查过了的节点列表
        private List<Node> closeList = new List<Node>();

        /// <summary>
        /// 构造寻路实例
        /// TODO：假设输入合法，点在地图上
        /// </summary>
        /// <param name="map"> 地图大小，0可以通过,1为障碍物不可通过 </param>
        /// <param name="xStart">起点X坐标</param>
        /// <param name="yStart">起点Y坐标</param>
        /// <param name="xTarget">目标点X坐标</param>
        /// <param name="yTarget">目标点Y坐标</param>
        public AStarFindV2(byte[,] map, int xStart, int yStart, int xTarget, int yTarget)
        {
            _mapArr = map;
            XMax = map.GetLength(0);
            YMax = map.GetLength(1);
            _nodeArr = new Node[XMax,YMax];
            for (int i = 0; i < XMax; i++)
            {
                for (int j = 0; j < YMax; j++)
                {
                    _nodeArr[i, j] = new Node(i, j, map[i, j] == 0);
                }
            }

            _start = _nodeArr[xStart, yStart];
            _target =  _nodeArr[xTarget, yTarget];
            
        }

        /// <summary>
        /// 处理该节点周围的节点
        /// 如果没加入openlist 则加入
        /// 
        /// 否则，针对每个节点temp，针对每个节点 计算temp.Gn和新的Gn(node.Gn + node->temp的Gn)
        /// 如果新的gn不是更小，那么无意义不作处理
        /// 如果新的gn更小，那么将temp.GN重新赋值，并且给parent赋值
        /// </summary>
        /// <param name="node"></param>
//        private void PrepareAroundNode(Node node)
//        {
//            int x, y;
//            //left
//            x = node.X - 1;
//            y = node.Y;
//            if (x >= 0)
//            {
//                var temp = _nodeArr[x, y];
//                if (temp.Walkable)
//                {
//                    temp.Parent = node;
//                    temp.Gn = node.Gn + 10;
//                    temp.Hn = CalcHn(temp);
//                }
//            }
//            //right
//            x = node.X + 1;
//            y = node.Y;
//            if (x < XMax)
//            {
//                var temp = _nodeArr[x, y];
//                if (temp.Walkable)
//                {
//                    temp.Parent = node;
//                    temp.Gn = node.Gn + 10;
//                    temp.Hn = CalcHn(temp);
//                }
//            }
//            //up
//            x = node.X;
//            y = node.Y+1;
//            if (y < YMax)
//            {
//                var temp = _nodeArr[x, y];
//                if (temp.Walkable)
//                {
//                    temp.Parent = node;
//                    temp.Gn = node.Gn + 10;
//                    temp.Hn = CalcHn(temp);
//                }
//            }
//            //down
//            x = node.X ;
//            y = node.Y - 1;
//            if (y >= 0)
//            {
//                var temp = _nodeArr[x, y];
//                if (temp.Walkable)
//                {
//                    temp.Parent = node;
//                    temp.Gn = node.Gn + 10;
//                    temp.Hn = CalcHn(temp);
//                }
//            }
//        }

        private List<Node> getAroundNodes(Node node)
        {
            List<Node> nodes = new List<Node>();
            int x, y;
            //left
            x = node.X - 1;
            y = node.Y;
            if (x >= 0)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            //right
            x = node.X + 1;
            y = node.Y;
            if (x < XMax)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            //up
            x = node.X;
            y = node.Y+1;
            if (y < YMax)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            //bottom
            x = node.X ;
            y = node.Y - 1;
            if (y >= 0)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            
            //left up
            x = node.X - 1;
            y = node.Y + 1;
            if (x >=0 && y < YMax)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            
            //left bottom
            x = node.X - 1;
            y = node.Y - 1;
            if (x >=0 && y >= 0)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            
            //right up
            x = node.X + 1;
            y = node.Y + 1;
            if (x < XMax && y < YMax)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            
            //right bottom
            x = node.X + 1;
            y = node.Y - 1;
            if (x < XMax && y >= 0)
            {
                var temp = _nodeArr[x, y];
                if (temp.Walkable)
                {
                    nodes.Add(temp);
                }
            }
            return nodes;
        }

        private int CalcHn(Node node)
        {
            return 10*( Math.Abs(node.X - _target.X) + Math.Abs(node.Y - _target.Y));
        }
        
        public  Node GetMinF()
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

        public List<Node> Result { get; private set; }

        public void Dsth()
        {
            Loger.Color("closelist count -->"+closeList.Count);
        }
        private List<Node> IteratorParent(Node current)
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

        private int GetHn(Node node)
        {
            return CalcHn(node);
        }
        

        public void Find()
        {
            _start.Gn = 0;
            _start.Hn = CalcHn(_start);
            openList.Add(_start);

            while (openList.Count != 0)
            {
                var current = GetMinF();
                
//                Loger.Color(current);
                if (current.PosEqual(_target))
                {
                    Result = IteratorParent(current);
                    return;
                }

                openList.Remove(current);
                closeList.Add(current);

                var around2 = getAroundNodes(current);
                foreach (var t in around2)
                {
                    if (closeList.Contains(t))
                    {
                        continue;
                    }

                    if (t.Gn == 0)
                    {
                        t.Gn = getGnBetween(_start, t);
                    }

                    var nG = current.Gn + getGnBetween(current,t);
                    if (!openList.Contains(t))
                    {
                        openList.Add(t);
                        
                    }
                    else if(nG >= t.Gn)
                    {
                        continue;
                    }
                    
                    t.Gn = nG;
                    t.Hn = CalcHn(t);
                    t.Parent = current;
                }
            }
        }

        private int getGnBetween(Node current, Node node)
        {
            var v =100* (current.X - node.X) *  (current.X - node.X)+100* (current.Y - node.Y) *  (current.Y - node.Y);
            var f =(int) (Math.Sqrt(v));
            return f;
        }
    }
}

//    -13   -1101
//原码    10001101
//补码    11110011
//反码    11110010