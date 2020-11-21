using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS
{
    class VexInfo
    {
        public int no;

        public string name;

        public string introduction;

        public VexInfo()
        {
            no = 0;
            name = null;
            introduction = null;
        }
    }

    class Graph
    {
        public VexInfo[] vexs;

        public int[,] arcs;

        public int vexsNum;

        public int arcsNum;

        public const int INFINITE = 32767;

        public Graph()
        {
            string[] vexsNames = new string[] { "大门", "体育馆", "主楼", "联通广场", "游泳馆", "音乐厅", "图书馆", "家属区", "田径场", "实验楼", "文博馆", "餐厅", "篮球场" };
            string[] vexsIntroductions = new string[] { "A区大门", "集比赛、运动、集会的综合性场馆", "行政办公", "由中国联通赞助", "游泳场所", "文艺演出、观影场所", "借阅图书和自习", "家属、职工住宅区", "田径运动场所", "各门学科实验场所", "参观黑大校史", "吃饭", "打篮球" };
            vexs = new VexInfo[vexsNames.Length];
            arcs = new int[vexsNames.Length, vexsNames.Length];
            for (int i = 0; i < vexs.Length; i++)
            {
                vexs[i] = new VexInfo();
                vexs[i].no = i;
                vexs[i].name = vexsNames[i];
                vexs[i].introduction = vexsIntroductions[i];
            }
            for (int i = 0; i < vexsNames.Length; i++)
            {
                for (int j = 0; j < vexsNames.Length; j++)
                {
                    arcs[i, j] = INFINITE;
                }
            }
            vexsNum = vexsNames.Length;
            arcsNum = 25;

            arcs[0, 1] = 2;
            arcs[0, 2] = 1;
            arcs[1, 2] = 2;
            arcs[1, 4] = 4;
            arcs[2, 4] = 2;
            arcs[2, 3] = 1;
            arcs[3, 4] = 2;
            arcs[4, 5] = 2;
            arcs[3, 5] = 2;
            arcs[3, 6] = 1;

            arcs[5, 6] = 1;
            arcs[5, 7] = 1;
            arcs[6, 7] = 2;
            arcs[6, 8] = 1;
            arcs[6, 9] = 2;
            arcs[7, 8] = 1;
            arcs[3, 9] = 3;
            arcs[3, 10] = 5;
            arcs[9, 10] = 3;
            arcs[9, 11] = 1;

            arcs[10, 11] = 2;
            arcs[11, 12] = 2;
            arcs[9, 12] = 2;
            arcs[8, 9] = 2;
            arcs[8, 12] = 5;
            for (int i = 0; i < vexsNum; i++)
            {
                for (int j = 0; j < vexsNum; j++)
                {
                    arcs[j, i] = arcs[i, j];
                }
            }
        }

        public int ConfirmVexLocation(string vexName)
        {
            for (int i = 0; i < vexsNum; i++)
            {
                if (vexName.Equals(vexs[i].name))
                {
                    return i;
                }
            }
            return -1;
        }

        public VexInfo CheckInfo(string name)
        {
            for (int i = 0; i < vexsNum; i++)
            {
                if (vexs[i].name.Equals(name))
                {
                    return vexs[i];
                }
            }
            return null;
        }

        public StringBuilder PathGateToOthers(string destinationVexName, out int[] vexLocation)
        {
            int[] currentShortestPathLength = new int[vexsNum];
            int[] currentShortestPathBeforeNodeLocation = new int[vexsNum];
            bool[] isShortestPathConfirm = new bool[vexsNum];
            for (int i = 0; i < vexsNum; i++)
            {
                isShortestPathConfirm[i] = false;
                currentShortestPathLength[i] = arcs[0, i];
                currentShortestPathBeforeNodeLocation[i] = 0;
            }
            isShortestPathConfirm[0] = true;
            currentShortestPathLength[0] = 0;
            for (int i = 1; i < vexsNum; i++)
            {
                int shortestSideLocation = -1;
                for (int j = 0; j < vexsNum; j++)
                {
                    if (!isShortestPathConfirm[j] && (shortestSideLocation == -1 || currentShortestPathLength[j] < currentShortestPathLength[shortestSideLocation]))
                    {
                        shortestSideLocation = j;
                    }
                }
                isShortestPathConfirm[shortestSideLocation] = true;

                for (int j = 0; j < vexsNum; j++)
                {
                    if (!isShortestPathConfirm[j] && (currentShortestPathLength[shortestSideLocation] + arcs[shortestSideLocation, j] < currentShortestPathLength[j]))
                    {
                        currentShortestPathLength[j] = currentShortestPathLength[shortestSideLocation] + arcs[shortestSideLocation, j];
                        currentShortestPathBeforeNodeLocation[j] = shortestSideLocation;
                    }
                }
            }
            int destinationVexLocation = ConfirmVexLocation(destinationVexName);
            int ShortestPathLength = currentShortestPathLength[destinationVexLocation];
            Stack<int> stack = new Stack<int>();
            while (currentShortestPathBeforeNodeLocation[destinationVexLocation] != destinationVexLocation)
            {
                stack.Push(destinationVexLocation);
                destinationVexLocation = currentShortestPathBeforeNodeLocation[destinationVexLocation];
            }
            stack.Push(destinationVexLocation);
            StringBuilder str = new StringBuilder();
            vexLocation = new int[stack.Count];
            int k = 0;
            str.Append("【" + vexs[0].name + "】到" + "【" + destinationVexName + "】的最短路径为:  \n");
            while (stack.Count() != 0)
            {
                str.Append(vexs[stack.Peek()].name);
                vexLocation[k++] = stack.Peek();
                if (stack.Count() != 1)
                {
                    str.Append("->");
                }
                stack.Pop();
            }
            str.Append("\n路径长度为:  " + ShortestPathLength);
            return str;
        }

        public StringBuilder PathOneToOthers(string beginVexName, string destinationVexName,out int[] vexLocation)
        {
            int[,] currentShortestPathLength = new int[vexsNum, vexsNum];
            int[,] currentShortestPathBeforeNodeLocation = new int[vexsNum, vexsNum];
            for (int i = 0; i < vexsNum; i++)
            {
                for (int j = 0; j < vexsNum; j++)
                {
                    currentShortestPathLength[i, j] = arcs[i, j];
                    currentShortestPathBeforeNodeLocation[i, j] = i;
                }
            }
            for (int k = 0; k < vexsNum; k++)
            {
                for (int i = 0; i < vexsNum; i++)
                {
                    for (int j = 0; j < vexsNum; j++)
                    {
                        if (currentShortestPathLength[i, k] + currentShortestPathLength[k, j] < currentShortestPathLength[i, j])
                        {
                            currentShortestPathLength[i, j] = currentShortestPathLength[i, k] + currentShortestPathLength[k, j];
                            currentShortestPathBeforeNodeLocation[i, j] = currentShortestPathBeforeNodeLocation[k, j];
                        }
                    }
                }
            }

            int beginVexLocation = ConfirmVexLocation(beginVexName);
            int destinationVexLocation = ConfirmVexLocation(destinationVexName);
            int ShortestPathLength = currentShortestPathLength[beginVexLocation, destinationVexLocation];
            StringBuilder str = new StringBuilder();
            Stack<int> stack=new Stack<int>();
            while (currentShortestPathBeforeNodeLocation[beginVexLocation, destinationVexLocation] != beginVexLocation)
            {
                stack.Push(destinationVexLocation);
                destinationVexLocation = currentShortestPathBeforeNodeLocation[beginVexLocation, destinationVexLocation];
            }
            stack.Push(destinationVexLocation);
            stack.Push(beginVexLocation);
            vexLocation = new int[stack.Count()];
            int v = 0;
            str.Append("【" + beginVexName + "】到" + "【" + destinationVexName + "】的最短路径为:  \n");
            while (stack.Count() != 0)
            {
                vexLocation[v++] = stack.Peek();
                str.Append(vexs[stack.Peek()].name);
                if (stack.Count() != 1)
                {
                    str.Append("->");
                }
                stack.Pop();
            }
            str.Append("\n路径长度为:  " + ShortestPathLength);
            return str;
        }
    }
}
