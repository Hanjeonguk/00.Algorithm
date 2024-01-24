using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._ShortestPath
{
    internal class Dijkstra
    {
        /********************************************************************
         * 데이크스트라 알고리즘 (Dijkstra Algorithm)
         * 
         * 특정한 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구함
         * 방문하지 않은 노드 중에서 최단 거리가 가장 짧은 노드를 선택 후,
         * 해당 노드를 거쳐 다른 노드로 가는 비용 계산
         ********************************************************************/

        const int INF = 99999;
        //단절 INF 최댓값 상수설정, int.Maxvalue 사용 시 오버플로우 위험있음

        public static void ShortestPath(in int[,] graph, in int start,out bool[] visited, out int[] distance, out int[] parents)
        //최단거리 함수 생성
        {
            int size = graph.GetLength(0);//첫 배열길이를 가지는 size 변수
            visited = new bool[size];//첫 배열길이 대입
            distance = new int[size];//첫 배열길이 대입
            parents = new int[size];//첫 배열길이 대입

            for (int i = 0; i < size; i++)// 갯수만큼 반복
            {
                visited[i] = false; //초기값 대입
                distance[i] = INF; //초기값 대입
                parents[i] = -1;  //초기값 대입
            }
            distance[start] = 0; //시작지점 0;

            for (int i = 0; i < size; i++) // 갯수만큼 반복
            {
                // 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색
                int next = -1;//다음 정점 next 초기값 대입
                int minDistance = INF;//다음 정점까지 거리 minDistance 초기값 대입

                for (int j = 0; j < size; j++)//갯수만큼 반복
                {
                    if (!visited[j]&& 
                        distance[j] < minDistance) 
                        //방문하지 않았으면서(false) And 거리가 더 가까운 경우
                    {
                        next = j;//j가 다음 정점
                        minDistance = distance[j];//distance[j]가 최소거리
                    }
                }

                if (next < 0)// 반복이 끝났을 때, 해당되는 값이 없을 때
                    break; //멈춘다.

                // 2. 직접연결된 거리보다 다른 정점을 거쳐서 더 짧아진다면 갱신.
                for (int j = 0; j < size; j++)
                {
                    // distance[j] : 목적지까지 직접 연결된 거리
                    // distance[next] : 탐색중인 정점까지 거리
                    // graph[next, j] : 탐색중인 정점부터 목적지의 거리
                    if (distance[j]> distance[next]+ graph[next, j])
                        //직접거리가 탐색중인 정점을 거쳐서 목적지까지의 거리보다 먼 경우
                    {
                        distance[j] = distance[next] + graph[next, j];
                        //탐색정점을 거쳐서 간 거리를 직접거리로 대입

                        parents[j] = next;
                        //직전 정점 값 next;
                    }
                }
                visited[next] = true;//방문한 정점 true 표기
            }     
        }
    }
}