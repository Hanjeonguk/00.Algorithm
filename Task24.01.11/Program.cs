// 사용자에게 숫자를 입력 받아서
// 0부터 숫자까지 가지는 리스트 만들기
// 0요소를 제거
// 리스트가 가지는 모든 요소들을 출력해주는 반복분 작성
namespace Task24._01._11_1
{
    public class Program
    {
        static void Main1(string[] args)
        {

            List<int> list = new List<int>();

            Console.Write("숫자를 입력하세요 : ");
            int input = int.Parse(Console.ReadLine());


            for (int i = 0; i <= input; i++)
            {

                list.Add(i);

            }
            list.Remove(0);

            for (int i = 0; i <= list.Count; i++)
            {
                Console.Write($"{list[i]}  ");
            }
        }
    }
}

// 사용자의 입력을 받아서 없는 데이터면 추가, 있던 데이터면 삭제하는 보관함
// ex) 1,6,7,8,3 들고 있던 상황이면
// 2입력하면 1,6,7,8,3,2
// 7 입력하면 1,6 8,3
namespace Task24._01._11__2
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(3);

            Console.Write("숫자를 입력하세요 : ");
            int input = int.Parse(Console.ReadLine());
            int num = list.Count;

            bool b = list.IndexOf(input) != -1;


            if (b == true)
            {
                for (int i = 0; i < num; i++)
                {
                    if (list[i] == input)
                        list.RemoveAt(i);    
                }
            }
            else
            {
                list.Add(input);
            }

            for (int j = 0; j < list.Count; j++)
            {
                Console.Write($"{list[j]} ");
            }
        }
    }
}
// 인벤토리 구현(아이템 수집, 아이템 버리기)

// 브랜치 Homework 만들어서 저장

