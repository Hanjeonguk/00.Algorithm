namespace _10._Searching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 순차탐색
            int[] array = { 1, 3, 5, 7, 9, 8, 6, 4, 2, 0 };
            Console.Write("순차탐색 배열 : ");
            foreach (int i in array)
            {
                Console.Write($"{i,2}");
            }
            Console.WriteLine();

            int indexOf1 = Array.IndexOf(array, 2);
            int indexOf2 = Searching.SequentialSearch(array, 2);
            Console.WriteLine($"C# IndexOf 결과 위치 : {indexOf1}");
            Console.WriteLine($"구현한 IndexOf 결과 위치 : {indexOf2}");
            Console.WriteLine();


            // 이진탐색
            Console.Write("이진탐색 배열 : ");
            foreach (int i in array)
            {
                Console.Write($"{i,2}");
            }
            Console.WriteLine();
            Console.WriteLine();

            int nonSortedBinaryIndex1 = Array.BinarySearch(array, 2);
            int nonSortedBinaryIndex2 = Searching.BinarySearch(array, 2);
            Console.WriteLine("정렬 전 결과");
            Console.WriteLine($"C# 이진탐색1 결과 위치 : {nonSortedBinaryIndex1}");
            Console.WriteLine($"구현한 이진탐색2 결과 위치 : {nonSortedBinaryIndex2}");
            Console.WriteLine();

            Array.Sort(array);  // 이진탐색의 경우 우선 정렬이 필요

            Console.Write("정렬 후 이진탐색 배열 : ");
            foreach (int i in array)
            {
                Console.Write($"{i,2}");
            }
            Console.WriteLine();
            Console.WriteLine();

            int sortedBinaryIndex1 = Array.BinarySearch(array, 2);
            int sortedBinaryIndex2 = Searching.BinarySearch(array, 2);
            Console.WriteLine("정렬 후 결과");
            Console.WriteLine($"C# 이진탐색1 결과 위치 : {sortedBinaryIndex1}");
            Console.WriteLine($"구현한 이진탐색2 결과 위치 : {sortedBinaryIndex2}");
            Console.WriteLine();

            //그래프 함수

            bool[,] graph = new bool[8, 8]
            {
                { false,  true, false, false, false, false, false, false },
                {  true, false,  true, false, false,  true, false, false },
                { false,  true, false, false,  true,  true, false, false },
                { false, false, false, false, false,  true, false, false },
                { false, false,  true, false, false, false,  true,  true },
                { false,  true,  true,  true, false, false, false, false },
                { false, false, false, false,  true, false, false, false },
                { false, false, false, false,  true, false, false, false },
            };


            // DFS 탐색
            Searching.DFS(in graph, 0, out bool[] dfsVisited, out int[] dfsPath);
            Console.WriteLine("<DFS>");
            PrintGraphSearch(dfsVisited, dfsPath);
            Console.WriteLine();


            // BFS 탐색
            Searching.BFS(in graph, 0, out bool[] bfsVisited, out int[] bfsPath);
            Console.WriteLine("<BFS>");
            PrintGraphSearch(bfsVisited, bfsPath);
            Console.WriteLine();
        }

        private static void PrintGraphSearch(bool[] visited, int[] path)
        {
            Console.WriteLine($"{"Vertex",8}{"Visit",8}{"Path",8}");

            for (int i = 0; i < visited.Length; i++)
            {
                Console.WriteLine($"{i,8}{visited[i],8}{path[i],8}");
            }
        }
    }
}