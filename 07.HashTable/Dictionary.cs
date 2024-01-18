using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{   //Dictionary 클래스 생성<key,value 자료형 일반화>
    //key 값을 비교할 필요가 없기 때문에 IComparable이 아닌
    //키 값이 같은지 다른지 비교하는 where TKey : IEquatable<TKey>
    public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 1000; //기본 크기 설정(소수로 설정 시 효율 증가)

        private struct Entry //구조체 Entry
        {
            public enum State { None, Using, Deleted } //Entry의 상태를 구별하는 열거형 

            public State state; //state값
            public TKey key; //key값
            public TValue value; //value값         
        }

        private Entry[] table; //key와 value, state를 가진 테이블 배열 생성
        private int usedCount; //사용중인 갯수

        public Dictionary()
        {
            table = new Entry[DefaultCapacity]; //테이블 기본크기 
            usedCount = 0; //갯수 0
        }

        public TValue this[TKey key] //this 인덱서, dictionary[4] = "F"; 구현
        {
            get //가져오기
            {
                if (Find(key, out int index)) //키값 찾았을 때
                {
                    return table[index].value; //값 확인
                }
                else //키값 못 찾았을 때
                {
                    throw new KeyNotFoundException(); //예외처리
                }
            }
            set //갱신하기
            {
                if (Find(key, out int index)) //키값 찾았을 때
                {
                    table[index].value = value; //value값 인덱스 위치에 입력
                }
                else //키값 못 찾았을 때
                {
                    Add(key, value); //비어있는 위치에 키,데이터 입력
                    //table[index].key = key;
                    //table[index].value = value;
                    table[index].state = Entry.State.Using; //인덱스위치를 사용중으로 상태변경
                    usedCount++; //갯수 증가
                }
            }
        }

        public void Add(TKey key, TValue value) //값 입력 함수 Add
        {
            if (Find(key, out int index))  //입력한 키값이 있다면 
            {
                throw new InvalidOperationException("Already exist key"); //예외처리
            }
            else // 입력한 키값이 없다면
            {
                if (usedCount > table.Length * 0.7f) // 테이블의 70%이상 사용중이라면
                {
                    ReHashing(); //테이블 크기 증가 
                }

                table[index].key = key; //지금 key값 현재인덱스key값에 입력
                table[index].value = value; // 지금 value값 현재인덱스 value값에 입력
                table[index].state = Entry.State.Using; //값을 추가했으니 사용중으로 상태변경
                usedCount++; //갯수 증가
            }
        }

        public void Clear()
        {
            table = new Entry[DefaultCapacity];
            usedCount = 0;
        }

        public bool ContainsKey(TKey key) //키값으로 데이터 확인하는 함수
        {
            if (Find(key, out int index)) //찾는 키값이 있을때
            {
                return true;
            }
            else //찾는 키값이 없을때
            {
                return false;
            }
        }

        public bool Remove(TKey key) //키값을 찾아 삭제하는 함수 구현
        {
            if (Find(key, out int index)) //찾는 키값이 있다면
            {
                table[index].state = Entry.State.Deleted; //삭제된 것으로 상태변경
                return true; //완료
            }
            else //찾는 키값이 없다면
            {
                return false; //실패
            }
        }

        private bool Find(TKey key, out int index) //key값이 있는지 확인하는 bool함수
        {
            if (key == null) //key값이 없으면
                throw new ArgumentNullException(); //예외처리

            index = Hash(key); //key값을 해싱하여 index입력

            for (int i = 0; i < table.Length; i++) //빈자리를 발견할 때까지 반복
            {
                if (table[index].state == Entry.State.None) //테이블 인덱스위치가 비어있는 경우
                {
                    return false; //찾지 못했으니 false
                }
                else if (table[index].state == Entry.State.Using) //테이블 인덱스위치가 사용중인 경우
                {
                    if (key.Equals(table[index].key)) //현재 키 값이 테이블에 있는 키 값과 같은지 확인
                    {
                        return true; //같으면 true
                    }
                    else // 같지 않다면
                    {
                        // 다음칸으로
                        index = Hash2(index);
                    }
                }
                else // if(table[index].state==Entry.state.Deleted) 테이블 인덱스 위치가 지워진 상태일 때
                {
                    //다음칸으로
                    index = Hash2(index);
                }
            }
            index = -1; //빈칸이 없을경우, 인덱스를 찾을 수 없다는 표현
            throw new InvalidOperationException();
        }

        private int Hash(TKey key) //key값으로 해싱하는 함수
        {
            
            return Math.Abs(key.GetHashCode() % table.Length); //key값을 받아서 테이블크기로 나눠서 나머지, 절댓값으로 고정, 나눗셈법 구현
        }

        private int Hash2(int index) //충돌했을 떄 선형탐사하는 함수
        {
            // 선형 탐사
            return (index + 1) % table.Length; //다음 인덱스에 나눗셈법하여 입력

            // 제곱 탐사
            // return (index + 1) * (index + 1) % table.Length;

            // 이중 해싱
            // return Math.Abs((index + 1).GetHashCode() % table.Length);
        }

        private void ReHashing() //데이터가 일정 부분 채워지면 크기 변경
        {
            Entry[] oldTable = table; //기존 테이블을 변수에 저장
            table = new Entry[table.Length * 2]; //기존테이블의 2배 크기로 인스턴스 생성
            usedCount = 0; //갯수 초기화

            for (int i = 0; i < oldTable.Length; i++) //기존테이블을 전부 확인
            {
                Entry entry = oldTable[i]; //인덱스 위치를 순서대로 넣고
                if (entry.state == Entry.State.Using) // 사용중인 위치가 있다면
                {
                    Add(entry.key, entry.value); // 입력해준다.
                }
            }
        }
    }
}
