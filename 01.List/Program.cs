﻿namespace _01.List
{
    internal class Program
    {
        /*******************************************************
         * 리스트 (List)
         * 
         * 런타임 중 크기를 확장할 수 있는 배열기반의 자료구조
         * 배열요소의 갯수를 특정할 수 없는 경우 사용이 용이
         *******************************************************/

        // <리스트 구현>
        // 리스트는 배열기반의 자료구조이며, 배열은 크기를 변경할 수 없는 자료구조
        // 리스트는 동작 중 크기를 확장하기 위해 포함한 데이터보다 더욱 큰 배열을 사용
        //
        // 크기 = 3, 용량 = 8       크기 = 4, 용량 = 8       크기 = 5, 용량 = 8
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐
        // │1 │2 │3 │ │  │  │  │  │            │1│2│3│4│ │ │ │ │        │1│2│3│4│5│ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘


        // <리스트 삽입>
        // 중간에 데이터를 추가하기 위해 이후 데이터들을 뒤로 밀어내고 삽입 진행
        //      ↓                        ↓                        ↓
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐
        // │1 │2 │3│4│  │  │  │  │       =>   │1 │2 ││3 │4 │ │ │ │   =>   │1│2│A│3│4│ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘


        // <리스트 삭제>
        // 중간에 데이터를 삭제한 뒤 빈자리를 채우기 위해 이후 데이터들을 앞으로 당김
        //      ↓                        ↓
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐        ┌─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│A│3│4│ │ │ │   =>   │1│2│ │3│4│ │ │ │   =>   │1│2│3│4│ │ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘        └─┴─┴─┴─┴─┴─┴─┴─┘


        // <리스트 용량>
        // 용량을 가득 채운 상황에서 데이터를 추가하는 경우
        // 더 큰 용량의 배열을 새로 생성한 뒤 데이터를 복사하여 새로운 배열을 사용
        //
        // 1. 리스트가 가득찬 상황에서 새로운 데이터 추가 시도
        // 크기 = 8, 용량 = 8
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│3│4│5│6│7│8│ ← A 추가
        // └─┴─┴─┴─┴─┴─┴─┴─┘
        //
        // 2. 새로운 더 큰 배열 생성
        // 크기 = 8, 용량 = 8          크기 = 0, 용량 = 16
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐           ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│3│4│5│6│7│8│ ← A 추가  │ │ │ │ │ │ │ │ │ │ │ │ │ │ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┘           └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
        //
        // 3. 새로운 배열에 기존의 데이터 복사
        // 크기 = 8, 용량 = 8          크기 = 8, 용량 = 16
        // ┌─┬─┬─┬─┬─┬─┬─┬─┐           ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│3│4│5│6│7│8│ ← A 추가  │1│2│3│4│5│6│7│8│ │ │ │ │ │ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┘           └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
        //
        // 4. 기본 배열 대신 새로운 배열을 사용
        // 크기 = 8, 용량 = 16
        // ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│3│4│5│6│7│8│ │ │ │ │ │ │ │ │ ← A 추가
        // └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
        //
        // 5. 빈공간에 데이터 추가
        // 크기 = 9, 용량 = 16
        // ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
        // │1│2│3│4│5│6│7│8│A│ │ │ │ │ │ │ │
        // └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘


        // <리스트 시간복잡도>
        // 접근    탐색    삽입    삭제
        // O(1)    O(n)    O(n)    O(n)

        static void Main(string[] args)
        {
            // 1000마리의 몬스터
            // 공격범위 안에 있는 몬스터를 공격해야 할때
            // 그 범위를 특정하기 어렵다.

              int[] array = new int[100];  // 100이상일 때 배열 크기 부족
           // int[] array = new int[1000]; // 수가 적을 때 용량 낭비

            //배열은 고정적인 크기에 용이하나 유동적인 크기에 제한이 있다.
            //이 때 List를 사용하여 유동적인 크기를 가진 배열을 구현할 수 있다.

            //List<자료형> 변수 = new List<자료형>();
            List<string> list = new List<string>();

            // 추가
            list.Add("0번째 데이터");// Add 순서대로 배열크기 및 데이터추가
            list.Add("1번째 데이터");//Insert보다 선호
            list.Add("2번째 데이터");//0(1)
            list.Add("3번째 데이터");
            list.Add("4번째 데이터");
            list.Insert(1, "중간 데이터 1에 추가");// Insert 중간에 몇번째를 지정하여 데이터 추가, 기존 데이터는 뒤로 한칸씩 밀린다.
            list.Insert(3, "중간 데이터 3에 추가");//0(n)


            // 삭제
            list.Remove("2번째 데이터");// Remove 해당 데이터를 삭제한다.
            list.Remove("3번째 데이터");// 지우고 나면 빈칸만큼 뒤에서 데이터를 앞당겨 준다.
            bool success = list.Remove("4번째 데이터"); //true
            list.RemoveAt(2);// RemoveAt 몇번째를 지정하여 데이터 삭제

            list.Remove("6번째 데이터");// 없는 데이터를 삭제하려고 시도하면 무시된다. 
            bool fail = list.Remove("4번째 데이터");// false

            list.RemoveAt(list.Count - 1); //add와 같이 가장 뒷 배열을 삭제하면 앞당기는과정을 생략하기 때문에 효율 좋음


            // 접근
            list[0] = "수정된 0번 데이터";
            string text = list[2];

            //ex)
            for (int i = 0; i < list.Count; i++) //list.Count 배열 갯수
            {

            }


            //탐색
            int index = list.IndexOf("4번째 데이터");//해당 데이터가 몇번째에 있는지 탐색


            // 크기의 제한이 필요할 때는 확정적인 크기를 가지는 배열이 더 유리하다.

            List<int> list1 = new List<int>();
            //list1.Capacity = 100000;   //사용할 배열의 크기(Capacity)가 클 것이 확정적이라면 미리 크기를
                                       //설정하는게 용량을 변경하는 복사 과정에서 생기는 쓰레기를 줄일 수 있다.
        
            for(int i = 0;i < 100; i++)
            {
                list1.Add(i);
                Console.WriteLine($"{i+3} {list1.Capacity}");
            }
            //자동으로 생성되는 용량의 크기는 4부터 2배씩 증가한다.
        }
    }
}
//
