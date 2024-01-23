using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _10._Searching
{
    internal class Searching
    {

        // <순차 탐색>
        // 자료구조에서 순차적으로 찾고자 하는 데이터를 탐색
        // 시간복잡도 - O(n)
        public static int SequentialSearch<T>(in IList<T> list, in T item)//in 읽기전용
        {
            for (int i = 0; i < list.Count; i++)//리스트갯수만큼 반복
            {
                if (list[i].Equals(item))//list i번째 값과 item이 일치했을때
                    return i; //인덱스 위치 반환
            }
            return -1; //없으면 -1
        }


        // <이진 탐색>
        // 정렬이 되어있는 자료구조에서 2분할을 통해 데이터를 탐색
        // 시간복잡도 - O(logn)

        public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable<T>  //비교가 가능해야 함
        {
            int low = 0;//시작
            int high = list.Count - 1;//마지막
            while (low <= high)//low가 high 보다 작거나 같을 때 반복
            {
                int middle = (low + high) / 2;//가운데
                int compare = list[middle].CompareTo(item);//가운데 값과 아이템을 비교

                if (compare < 0)//item이 더 클때
                    low = middle + 1;
                else if (compare > 0)//item이 더 작을때
                    high = middle - 1;
                else//같은 경우
                    return middle; //결과값
            }

            return -1;//없을 때
        }


        // <깊이 우선 탐색 (Depth-First Search)>
        // 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤,
        // 분기의 탐색을 마쳤을 때 다음 분기를 탐색
        // 스택을 통해 구현
        public static void DFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];//visited배열 크기 graph첫 배열의 길이
            parents = new int[graph.GetLength(0)];//parents배열 크기 graph첫 배열의 길이

            for (int i = 0; i < graph.GetLength(0); i++)//배열길이만큼 반복
            {
                visited[i] = false;//기본값false
                parents[i] = -1;//기본값 -1
            }

            SearchNode(graph, start, visited, parents);
        }

        private static void SearchNode(in bool[,] graph, int start, bool[] visited, int[] parents)
        {
            visited[start] = true;//start에 있는 값 true
            for (int i = 0; i < graph.GetLength(0); i++)//배열길이만큼 반복
            {
                if (graph[start, i] &&      // 연결되어 있는 정점이며,
                    !visited[i])            // 방문한적 없는 정점
                {
                    parents[i] = start;
                    SearchNode(graph, i, visited, parents);
                }
            }
        }


        // <너비 우선 탐색 (Breadth-First Search)>
        // 그래프의 분기를 만났을 때 모든 분기들을 탐색한 뒤,
        // 다음 깊이의 분기들을 탐색
        // 큐를 통해 탐색
        public static void BFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }

            visited[start] = true;
            Queue<int> bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(start);

            while (bfsQueue.Count > 0)
            {
                int next = bfsQueue.Dequeue();

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[next, i] &&       // 연결되어 있는 정점이며,
                        !visited[i])            // 방문한적 없는 정점
                    {
                        visited[i] = true;
                        parents[i] = next;
                        bfsQueue.Enqueue(i);
                    }
                }
            }
        }
    }
}
