namespace _09._Sorting  //break안했을 때? IList쓰는이유?
{
    public class Sorting 
    {
       
        public static void Swap(IList<int> list, int left, int right)// 두 값을 교체하는 Swap 함수 생성
        { //IList: 배열과 리스트 둘 다 사용하기 위함
            int temp = list[left]; 
            list[left] = list[right];
            list[right] = temp;
        }

        // <선택정렬> 
        // 데이터 중 가장 작은 값부터 하나씩 선택하여 정렬
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        // 안정정렬   -  O

        public static void SelectionSort(IList<int> list, int start, int end)
        {
            for (int i = start; i <= end; i++) //start부터 end까지 반복
            {
                int minIndex = i; //처음 인덱스를 minIndex 변수에 대입
                for (int j = i + 1; j <= end; j++) // j를 i다음칸으로 설정 후 end까지 반복
                {
                    if (list[minIndex] > list[j]) //현재 값이 다음값보다 크다면
                    {
                        minIndex = j; //minIndex를 다음 인덱스로 변경하고
                    }
                }
                Swap(list, i, minIndex); //두 값을 교체
            }
        }

        // <삽입정렬>
        // 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 삽입하여 정렬
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        // 안정정렬   -  O
        public static void InsertionSort(IList<int> list, int start, int end)
        {
            for (int i = start; i <= end; i++)//시작인덱스부터 마지막 인덱스까지 반복
            {
                for (int j = i; j >= 1; j--)//비교할 값i를 j에 대입, j가 1보다 작을 때까지 반복
                {
                    if (list[j - 1] < list[j])//앞에 값보다 자신이 크다면

                        break;  //stop 

                    Swap(list, j - 1, j); //앞에 값이 자신보다 크다면 교체
                }
            }
        }

        // <버블정렬> 
        // 서로 인접한 데이터를 비교하여 정렬
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        // 안정정렬   -  O
        public static void BubbleSort(IList<int> list, int start, int end) // 처음부터 2개씩 비교하며 큰 수를 뒤로 이동
        {
            for (int i = start; i <= end - 1; i++) //start부터 end-1까지 반복(j와 j+1을 비교하기 때문)
            {
                for (int j = start; j < end - i; j++) //start(앞에서부터 두 개씩 비교하면 큰수를 뒤로 보내는 형태이기 때문ㅇ)부터 end-i까지 반복
                {
                    if (list[j] > list[j + 1]) //현재 값이 다음 값보다 크다면
                    {
                        Swap(list, j, j + 1); //두 값을 교체
                    }
                }
            }
        }

        // <병합정렬>  //??
        // 데이터를 2분할하여 정렬 후 합병
        // 데이터 갯수만큼의 추가적인 메모리가 필요
        // 시간복잡도 -  O(nlogn)
        // 공간복잡도 -  O(n) //다른 정렬에 비해 공간활용도가 낮음
        // 안정정렬   -  O


        public static void MergeSort(IList<int> list, int start, int end)
        {
            if (start == end) 
                return;

            int mid = (start + end) / 2;
            MergeSort(list, start, mid); //?
            MergeSort(list, mid + 1, end); //정렬?
            Merge(list, start, mid, end);
        }

        private static void Merge(IList<int> list, int start, int mid, int end)
        {
            List<int> sortedList = new List<int>();//병합한 배열을 보관할 list
            int leftIndex = start;
            int rightIndex = mid + 1;

            // 분할 정렬된 List를 병합
            while (leftIndex <= mid && rightIndex <= end) //왼쪽 혹은 오른쪽 배열이 모두 소진되는 경우까지
            {
                if (list[leftIndex] < list[rightIndex]) //왼쪽배열값이 오른쪽배열값보다 작다면
                    sortedList.Add(list[leftIndex++]);//왼쪽 배열값을 list에 입력하고 1칸이동
                else
                    sortedList.Add(list[rightIndex++]);//오른쪽 배열값을 list에 입력하고 1칸이동
            }

            if (leftIndex > mid)    // 왼쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = rightIndex; i <= end; i++)//오른쪽에 남은 값 입력
                    sortedList.Add(list[i]);
            }
            else  // 오른쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = leftIndex; i <= mid; i++)//왼쪽에 남은 값 입력
                    sortedList.Add(list[i]);
            }

            // 정렬된 sortedList를 list로 재복사
            for (int i = 0; i < sortedList.Count; i++)
            {
                list[start + i] = sortedList[i];
            }
        }

        // <퀵정렬>
        // 하나의 피벗을 기준으로 작은값과 큰값을 2분할하여 정렬
        // 최악의 경우(피벗이 최소값 또는 최대값)인 경우 시간복잡도가 O(n²)
        // 시간복잡도 -  평균 : O(nlogn)   최악 : O(n²) but: 캐시 친화도가 높아 힙정렬보다 효율이 좋고 빠르다.
        // 공간복잡도 -  O(1)
        // 안정정렬   -  X
        public static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) //값이 같을 때,하나일 때 
                return;

            int pivot = start; //시작위치를 피벗으로
            int left = pivot + 1;//피벗다음위치
            int right = end;//마지막위치

            while (left <= right) // 엇갈릴때까지 반복
            {
                //left는 피벗보다 큰 값을 만날 때까지
                while (list[left] <= list[pivot] && left < right)
                    left++;
                //right는 피벗보다 작은 값을 만날 때까지
                while (list[right] > list[pivot] && left <= right)
                    right--;

                if (left < right)     // 엇갈리지 않았다면
                    Swap(list, left, right); //left와 right값 교체
                else                  // 엇갈렸다면
                    Swap(list, pivot, right);//피벗과 right값 교체
            }

            QuickSort(list, start, right - 1);//피벗기준으로 왼쪽끼리 퀵정렬
            QuickSort(list, right + 1, end);//피벗기준으로 오른쪽끼리 퀵정렬
        }

        // <힙정렬>
        // 힙을 이용하여 우선순위가 가장 높은 요소가 가장 마지막 요소와 교체된 후 제거되는 방법을 이용
        // 배열에서 연속적인 데이터를 사용하지 않기 때문에 캐시 메모리를 효율적으로 사용할 수 없어 상대적으로 느림
        // 시간복잡도 -  O(nlogn)
        // 공간복잡도 -  O(1)
        // 안정정렬   -  X
        public static void HeapSort(IList<int> list)
        {
            for (int i = list.Count / 2 - 1; i >= 0; i--)
            {
                Heapify(list, i, list.Count);
            }

            for (int i = list.Count - 1; i > 0; i--)
            {
                Swap(list, 0, i);
                Heapify(list, 0, i);
            }
        }
        
        //현업에서는 주로 Sort()보다 최적의 정렬을 생각해서 직접 구현하나요?
       
        private static void Heapify(IList<int> list, int index, int size)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int max = index;
            if (left < size && list[left] > list[max])
            {
                max = left;
            }
            if (right < size && list[right] > list[max])
            {
                max = right;
            }

            if (max != index)
            {
                Swap(list, index, max);
                Heapify(list, max, size);
            }
        }




    }
}
