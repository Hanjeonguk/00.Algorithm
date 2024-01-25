using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._PathFinding
{
    public class AStar
    {
        /************************************************************
         * A* 알고리즘
         * 
         * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
         * 경로 탐색의 우선순위를 두고 유망한 해부터 우선적으로 탐색
         ************************************************************/

        const int CostStraight = 10; //직선 가중치
        const int CostDiagonal = 14; //대각선 가중치

        static Point[] Direction =  //방향 
        {
            new Point(  0, +1 ),            // 상
            new Point(  0, -1 ),            // 하
            new Point( -1,  0 ),            // 좌
            new Point( +1,  0 ),            // 우
            new Point( -1, +1 ),            // 좌상
            new Point( -1, -1 ),            // 좌하
            new Point( +1, +1 ),            // 우상
            new Point( +1, -1 )             // 우하
        };

        public static bool PathFinding(in bool[,] tileMap, in Point start, in Point end, out List<Point> path)
        //경로 찾기 bool 함수(bool 2차원 배열 타일맵, 포인트 시작 위치, 포인트 끝 위치, 포인트 리스트 경로)
        {
            int ySize = tileMap.GetLength(0); //?
            int xSize = tileMap.GetLength(1); //?

            ASNode[,] nodes = new ASNode[ySize, xSize]; //노드 데이터 보관 ASNode 2차원 배열
            bool[,] visited = new bool[ySize, xSize]; //방문 확인 visited 2차원배열 
            PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();
            //f가 낮은 것부터 탐색하기 위한 우선순위큐<ASNode,int>  

            // 0. 시작 정점을 생성하여 추가
            ASNode startNode = new ASNode(start, new Point(), 0, Heuristic(start, end));
            //시작 정점 데이터 입력 ASNode(현재정점, 이전정점, g, h)
            
            nodes[startNode.pos.y, startNode.pos.x] = startNode;
            //시작 위치 노드에 대입
            nextPointPQ.Enqueue(startNode, startNode.f); 
            //우선순위큐에 f를 기준으로 값 대입

            while (nextPointPQ.Count > 0)//우선순위 큐 갯수만큼 반복
            {
                // 1. 다음으로 탐색할 정점 꺼내기 : f가 가장 낮은 정점
                ASNode nextNode = nextPointPQ.Dequeue();

                // 2. 탐색한 정점은 방문표시
                visited[nextNode.pos.y, nextNode.pos.x] = true;

                // 3. 다음으로 탐색할 정점이 도착지인 경우
                // 도착했다고 판단해서 경로 반환, 종료
                if (nextNode.pos.x == end.x && nextNode.pos.y == end.y)//다음 정점 x,y가 끝 위치와 같을 때
                {
                    path = new List<Point>(); //경로 반환용 path 리스트

                    Point point = end;
                    while ((point.x == start.x && point.y == start.y) == false)
                        //도착지부터 시작점에 돌아올 때까지
                    {
                        path.Add(point);//정점을 path에 추가
                        point = nodes[point.y, point.x].parent;//parent 추적
                    }
                    path.Add(start); //마지막에 시작지점 추가

                    path.Reverse();//path 값 반전

                    return true;//끝 위치를 찾은 경우니까 true
                }

                // 4. AStar 탐색을 진행 : 탐색한 정점 주변의 정점 점수 계산
                // 방향 탐색
                for (int i = 0; i < Direction.Length; i++)//방향배열 길이만큼 반복
                {
                    int x = nextNode.pos.x + Direction[i].x;//?
                    int y = nextNode.pos.y + Direction[i].y;//?

                    // 4-1. 탐색하면 안되는 경우
                    // 맵을 벗어났을 경우 : 0보다 작거나 배열보다 클때
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                        continue;
                    // 탐색할 수 없는 정점일 경우 : 벽을 만났을 때
                    else if (tileMap[y, x] == false)
                        continue;
                    // 이미 탐색한 정점일 경우 : visited == true
                    else if (visited[y, x])
                        continue;
                    // 대각선으로 이동이 불가능 지역인 경우
                    // 대각선 방향의 양 옆이 막혀있을 때 대각선이동 불가 &&
                    // 대각선 방향의 양 옆 중 하나라도 막혀 있을 때 대각선 이동불가 ||
                    else if (i >= 4 && tileMap[y, nextNode.pos.x] == false || tileMap[nextNode.pos.y, x] == false)
                        continue;

                    // 4-2. 탐색후 점수를 계산한 정점 만들기
                    int g = nextNode.g + ((nextNode.pos.x == x || nextNode.pos.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Point(x, y), end);//지금 위치부터 도착지까지 h
                    ASNode newNode = new ASNode(new Point(x, y), nextNode.pos, g, h); //새로운 정점

                    // 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 점수계산을 하지 않은 정점이거나
                        nodes[y, x].f > newNode.f)  // 새로운 정점의 가중치가 더 낮을 때 
                    {
                        nodes[y, x] = newNode;//새로운 정점의 값으로 갱신 
                        nextPointPQ.Enqueue(newNode, newNode.f);//새로운 정점 데이터 우선순위큐에 입력
                    }
                }
            }
            //모든 정점을 탐색해도 도착지가 없을 떄 null, false
            path = null;
            return false;
        }

        // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
        private static int Heuristic(Point start, Point end) //휴리스틱 계한 함수 생성
        {
            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수, 절댓값 Math.Abs
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수, 절댓값 Math.Abs

            // 맨해튼 거리 : 직선을 통해 이동하는 거리
            // return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리 (가로제곱+세로제곱)에 루트Math.Sqrt = 대각선 거리
            // return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);

            // 타일맵 거리 : 직선과 대각선을 통해 이동하는 거리
            int straightCount = Math.Abs(xSize - ySize); //대각선을 먼저쓰고 남은 갯수 = 직선 갯수
            int diagonalCount = Math.Max(xSize, ySize) - straightCount; //xSize, ySize 중 큰 것 - 직선 갯수 = 대각선 갯수
            return CostStraight * straightCount + CostDiagonal * diagonalCount;
            //직선가중치*직선갯수 + 대각선가중치*대각선갯수 = h

            //다익스트라
            //return 0;
        }

        public class ASNode// 정점이 가지는 데이터 ASNode 생성
        {
            public Point pos;       // 현재 정점 위치
            public Point parent;    // 이 정점을 탐색한 정점 위치

            public int g;           // 현재까지의 값, 즉 지금까지 경로 가중치(g가 높은 걸 우선탐색 경향, 남은거리가 짧기 때문)
            public int h;           // 휴리스틱, 앞으로 예상되는 값, 목표까지 추정 경로 가중치
            public int f;           // 총 예상거리, f(x) = g(x) + h(x);

            public ASNode(Point point, Point parent, int g, int h) //생성자
            {
                this.pos = point;
                this.parent = parent;
                this.g = g;
                this.h = h; 
                this.f = g + h;
            }
        }
    }

    public struct Point //x,y 값을 가지는 포인트 구조체
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}