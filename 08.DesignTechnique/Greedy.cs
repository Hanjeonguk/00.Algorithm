using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08._DesignTechnique
{
    public class Greedy
    {
        /************************************************************
		 * 탐욕 알고리즘 (Greedy Algorithm)
		 * 
		 * 문제를 해결하는데 사용되는 근시안(짧은시야)적 방법
		 * 문제를 직면한 당시에 당장 최적인 답을 선택하는 과정을 반복
		 * 단, 반드시 최적의 해를 구해준다는 보장이 없음
		 *************************************************************/

        // 예시 - 자판기 거스름돈
        public void CoinMachine(int money)//거스름돈
        {
            int[] coinType = { 500, 100, 50, 10, 5,  1 };//코인 배열

            foreach (int coin in coinType)//동전 배열의 요소 반복
            {
                while (money <= coin)// 남은 거스름돈이 동전보다 작거나 같다면 반복
                {
                    Console.WriteLine($"{coin} 코인 배출");
                    money -= coin;
                }
            }      
        }
        
    }
    public class Test
    {
        static void Main(string[] args)
        {
            Greedy greedy = new Greedy();
            greedy.CoinMachine(600);
        }
    }
}
