using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastructure
{
    //List 생성 과정
    public class List<T>//List 클래스 생성, 자료형 일반화
    {
        private const int DefaultCapacity = 4;
        private T[] items;
        private int count;

        public List() 
        {
            items = new T[DefaultCapacity];
            count = 0;
        }

        public List(int capacity)
        {
            items =new T[capacity];
             count = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return count; } }

        public bool IsFull { get { return count == items.Length; } }//추가 방식

        public T this[int index] 
        { 
            get 
            {
                return items[index];
            }
            set 
            {
                items[index] = value;
            }
        }
        public void Add(T item)
        {
            if (count== items.Length)// 배열이 다 채워졌을때
            if (IsFull)
                Grow();
            items[count++] = item;

        }
        public void Insert(int index, T item)
        {
            //예외처리 필요 : 크기를 벗어나게 중간에 끼워넣는 것이 불가능
            if (index < 0 || index > count)
           //     Console.WriteLine("Error 불가능하다.");
            throw new ArgumentOutOfRangeException("index");//추가 방식

            if (IsFull)
                Grow();

            Array.Copy(items, index, items, index + 1, count - index);

            items[index] = item;
            count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveAt(int index)
        {
            //예외처리 필요 : 크기를 벗어나게 중간에 끼워넣는 것이 불가능
            if (index < 0 || index >= count)
              //  Console.WriteLine("Error 불가능하다.");
            throw new ArgumentOutOfRangeException("index");//추가 방식

            count--;
            Array.Copy(items, index + 1, items, index, count - index);
        }

        public int IndexOf(T item)
        {
            for(int i=0; i <  count; i++)
            {
                 if (item.Equals(items[i]))
                {
                    return i;                    
                }
            }
            return -1;
        }

        private void Grow()
        {
            //2. 새로운 더 큰 배열 생성
            T[] newItems = new T[items.Length*2];

            //3. 새로운 배열에 기존의 데이터 복사
            Array.Copy(items, newItems, items.Length);

            //4. 기본 배열 대신 새로운 배열을 사용
            items = newItems;

            //5. 빈 공간에 데이터 추가

        }
    }
}
//
