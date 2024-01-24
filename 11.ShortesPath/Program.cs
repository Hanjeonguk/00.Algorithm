namespace _11._ShortestPath
{
    internal class Program
    {
        const int INF = 99999;

        static void Main(string[] args)
        {
            int[,] graph = new int[8, 8] //사용할 그래프 생성
            {
                { INF, INF, INF, 5, 4, INF, INF, INF },
                { INF, INF, 6, INF, INF, INF, INF, INF },
                { 6, INF, INF, INF, INF, 8, INF, INF},
                { INF, INF, INF,INF, INF, 6, INF, INF },
                { INF, INF, 6, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, 4, 6},
                { INF, INF, 9, INF, INF, 1, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF },
                
            };

            Dijkstra.ShortestPath(in graph, 0,out bool[] visited, out int[] distance, out int[] parents);//함수에 값 대입

            Console.WriteLine("<Dijkstra>");

            PrintDijkstra(visited, distance, parents); 
        }

        private static void PrintDijkstra(bool[] visited, int[] distance, int[] parents)//값 출력 함수 생성
        {
            Console.WriteLine($"{"Vertex",8}{"Visited",10}{"Distance",10}{"Parents",10}");

            for (int i = 0; i < distance.Length; i++)
            {
                Console.Write($"{i,8}");

                Console.Write($"{visited[i],10}");

                if (distance[i] == INF)
                {
                    Console.Write($"{"INF",10}");
                }
                else
                {
                    Console.Write($"{distance[i],10}");
                }

                Console.WriteLine($"{parents[i],10}");
            }
        }
    }
}