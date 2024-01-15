using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Queue<T> //클래스 Queue 생성, 자료형 일반화
    {
        private const int DefaultCapacity = 4; //상수로 기본크기 생성

        private T[] array; //array 생성
        private int head;  //head 생성
        private int tail;  //tail 생성
        private int Count; //Count 생성
        public Queue() // 생성자 함수
        {
            array = new T[DefaultCapacity]; //배열 기본크기 초기화
            head = 0; //head 0초기화
            tail = 0; //tail 0초기화
            Count = 0; //Count 0초기화
        }
        public void Clear() //Queue를 초기화시키는 함수
        {
            array = new T[DefaultCapacity];
            head = 0;
            tail = 0;
            Count = 0;
        }
        public void Enqueue(T item) //Enqueue함수로 아이템 입력 구현
        {
            if (IsFull()) //배열이 전부 차있을 떄
            {
                Grow(); //배열의 크기를 늘려준다.
            }
            array[tail] = item; //현재 tail 위치에 item 입력
            tail++; // tail을 다음 칸으로 이동
            if(tail == array.Length) //tail이 배열의 마지막 위치에 있을 때는                     
            {
                tail = 0;        //다음 칸이 아닌 처음 칸으로 이동
            }
            Count++; //Count 1증가
        }
        public T Dequeue() //Dequeue 함수로 아이템 출력 구현
        {
            if (Count == 0) // 가지고 있는 값이 없을 때 출력하려고 하면
                throw new InvalidOperationException(); //예외처리

            T item = array[head];//head 위치의 값을 item에 입력
            head++;//head를 다음칸으로 이동
            if(head == array.Length) //head가 배열의 마지막 위치라면
            {
                head = 0;            //처음 칸으로 이동
            }
            Count--;              //Count 1감소
            return item; //head에서 꺼낸 item 출력
        }
        public T Peek() //Peek 함수로 다음 출력 될 head값 확인
        {
            if (IsEmpty()) //배열이 비어있는 경우 예외처리
                throw new InvalidOperationException();

            return array[head]; //head 값 확인
        }
        private void Grow() //배열이 다 채워졌을 때를 대비한 배열 늘리기
        {
            T[] newArray = new T[array.Length * 2];//기존 배열의 2배크기 newArray 생성
            
            if (head < tail) // head가 tail보다 앞에 있을 때 
            {
                Array.Copy(array, head, newArray, 0, tail);//처음부터 끝까지 복사
            }
            else // tail이 head보다 앞에 있을 때 그대로 복사하는게 아닌
                 // head부터 끝까지 복사
                 // 처음부터 tail까지 복사
            {
                Array.Copy(array, head, newArray, 0, array.Length - head);
                Array.Copy(array, 0, newArray, array.Length - head, tail);
            }
            array = newArray; //array에 newArray 크기 대입
            tail = Count; //tail은 마지막위치
            head = 0; //head는 처음위치를 가리키도록
        }
        private bool IsFull() // 배열이 다 채워졌을 때
        {
            if (head > tail)
            {
                return head == tail + 1; //head가 tail 앞칸에 있을 때 가득 찬 것으로 판정
            }
            else
            {
                return head == 0 && tail == array.Length - 1; //head가 맨 앞일 때 tail이 마지막일 때도 가득 찬 것으로 판정
            }
        }
        private bool IsEmpty() // 배열이 비어있는 경우
        {
            return head == tail; //head와 tail이 같은 위치에 있을 때
                                 //비어있다고 판정
        }
    }
}

