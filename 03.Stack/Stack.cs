using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    /******************************************************************
     * 어댑터 패턴 (Adapter)
     * 
     * 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환
     ******************************************************************/

    public class Stack<T> // Stack 클래스 생성, 자료형 일반화
    {
        private List<T> container; // 입력값을 담을  List container 생성 

        public Stack()  //Stack 생성자 함수
        {
            container = new List<T>(); //인스턴스 생성
        }
        public int Count { get { return container.Count; } }//Count 변수로 List의 갯수 container.Count 참조

        public void Push(T item) //Push함수로 item 입력 기능 구현
        {
            container.Add(item); //List container에 item 추가
       
        }

        public T Pop() //Pop함수로 item 출력 기능 구현
        {
            T item = container[container.Count - 1]; //List container에 마지막으로 입력된 값을 item에 입력
            container.RemoveAt(container.Count - 1); //container에 마지막으로 입력된 값을 제거
            return item; //item에 입력된 값 출력
        }

        public T Peek() //Peek함수로 다음으로 출력될 값 확인하는 기능 구현
        {
            return container[container.Count - 1]; //container에 마지막으로 입력된 값 출력
        }
    }
}
