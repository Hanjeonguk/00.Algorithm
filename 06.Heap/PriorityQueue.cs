using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{                       
    //우선순위큐 클래스<일반화>
    //우선순위는 비교가 필요하기 때문에 where TPriority : IComparable<TPriority>
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private struct Node //배열로 구현하기 위해 구조체로 Node 생성
        {
            public TElement element;   //데이터
            public TPriority priority; //우선순위

            public Node(TElement element, TPriority priority)//생성자
            {
                this.element = element;
                this.priority = priority;
            }
        }

        private List<Node> nodes; 

        public PriorityQueue()       //생성자
        {
            nodes = new List<Node>(); //리스트 생성
        }

        public int Count { get { return nodes.Count; } }

        public void Enqueue(TElement element, TPriority priority)//입력함수 구현
        {
            Node newNode = new Node(element, priority);
            nodes.Add(newNode);  //리스트에 새 노드 입력
            int index = nodes.Count - 1; //인덱스 위치는 리스트의 갯수-1
            
            while (index > 0) //인덱스0(최상단)에 도착하기 전까지 반복
            {
                int parentIndex = (index - 1) / 2; //부모 인덱스 위치
                Node parentNode = nodes[parentIndex]; //부모 값을  parentNode에 입력

                if (newNode.priority.CompareTo(parentNode.priority) < 0)
                    //새 노드의 우선순위와 부모의 우선순위를 비교했을 때 더 작다면
                {
                    //새노드가 상단으로 올라가야 한다.
                    nodes[index] = parentNode; //부모노드를 새 노드 위치에 입력
                    nodes[parentIndex] = newNode;//새 노드를 부모 노드 위치에 입력
                    index = parentIndex; //인덱스 상승
                }   
                else //부모가 더 우선순위가 높을 때
                {
                    break;//반복 중지
                }
            }
           // nodes[index] = newNode;
        }

        public TElement Peek()
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            return nodes[0].element;
        }

        public TElement Dequeue() //출력함수 구현
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            Node rootNode = nodes[0];//root 노드를 변수에 보관

            //제거 작업
            Node lastNode = nodes[nodes.Count - 1];//마지막노드 변수에 보관
            nodes[0] = lastNode; //마지막 노드 root노드 위치에 입력
            nodes.RemoveAt(nodes.Count - 1);//마지막노드 제거

            int index = 0;//인덱스 위치 최상단
            while (index < nodes.Count)// 인덱스가 마지막 위치까지 반복
            {
                int leftIndex = 2 * index + 1; //왼쪽 자식노드 인덱스 위치
                int rightIndex = 2 * index + 2; //오른쪽 자식노드 인덱스 위치

                if (rightIndex < nodes.Count)//자식이 둘 다 있는 경우
                {
                    int lessIndex;
                    if (nodes[leftIndex].priority.CompareTo(nodes[rightIndex].priority) < 0)
                        //왼쪽 인덱스 우선순위와 오른쪽 인덱스 우선순위를 비교해서 더 작다면
                    {   
                        lessIndex = leftIndex;//왼쪽 인덱스를 작은인덱스 변수에 입력
                    }
                    else//반대일 경우
                    {
                        lessIndex = rightIndex;//오른쪽 인덱스를 작은인덱스 변수에 입력
                    }
                    Node lessNode = nodes[lessIndex]; //작은 인덱스 노드 값을 작은노드변수에 입력 
                    if ((nodes[index].priority.CompareTo(nodes[lessIndex].priority) > 0))
                        //현재 인덱스 우선순위와 작은 인덱스 우선순위를 비교해서 더 크다면
                    {
                        nodes[lessIndex] = lastNode;//마지막노드에서 가져온 값을 작은 인덱스 위치에 입력
                        nodes[index] = lessNode; //작은 인덱스 노드 값을 최상단 인덱스 위치로 입력
                        index = lessIndex; //작은인덱스 위치를 현재 인덱스로 변경
                    }
                    else //현재 인덱스가 자식인덱스보다 작을 때
                    {
                        break; //반복 종료
                    }
                }
                else if (leftIndex < nodes.Count)//자식이 하나만 있는 경우
                {
                    Node leftNode = nodes[leftIndex];//왼쪽 자식노드의 값을 변수에 입력
                    if (nodes[index].priority.CompareTo(nodes[leftIndex].priority) > 0)
                        //현재노드의 우선순위와 왼쪽자식노드의 우선순위를 비교했을 때 크다면
                    {
                        nodes[leftIndex] = lastNode; //마지막 인덱스에서 가져온 값을 왼쪽인덱스에 입력
                        nodes[index] = leftNode;//왼쪽노드를 현재 인덱스에 입력
                        index = leftIndex;//왼쪽자식인덱스를 현재인덱스로 변경
                    }
                }
                else //자식이 없는 경우
                {
                    break;//반복 종료
                }
            }
            return rootNode.element;//보관한 root노드 출력
        }
    }
}
   