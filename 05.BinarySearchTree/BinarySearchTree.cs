using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class BinarySearchTree<T> where T : IComparable<T>
    //이진탐색트리 클래스 생성, IComparable 비교 가능한 자료형만 사용하도록 
    {
        private Node<T> root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public bool Add(T item)
        {
            Node<T> newNode = new Node<T>(item, null, null, null);

            if (root == null) //root가 null이라면
            {
                root = newNode; //root에 입력한다.
                return true;  
            }

            Node<T> current = root;// 현재 비교하고 있는 노드
            while (current != null) //빈자리가 아닐 때까지 반복
            {
                if (item.CompareTo(current.item) < 0)//item이 비교값보다 작을 때
                {
                    //왼쪽으로 가는 경우
                    if (current.left != null)
                    {
                        //왼쪽 자식이 있는 경우
                        //왼쪽으로 가서 계속 하강작업을 반복
                        current = current.left;
                    }
                    else
                    {
                        //왼쪽 자식이 없는 경우
                        //이 자리가 추가될 자리
                        current.left = newNode;
                        newNode.parent = current;
                        break;
                    }
                }
                else if (item.CompareTo(current.item) > 0)//item이 비교값보다 클 때
                {
                    //오른쪽으로 가는 경우
                    if (current.right != null)
                    {  
                        //오른쪽 자식이 있는 경우
                        //오른쪽으로 가서 계속 하강작업을 반복
                        current = current.right;
                    }
                    else
                    {
                        //오른쪽 자식이 없는 경우
                        //이 자리가 추가될 자ㄹ;
                        current.right = newNode;
                        newNode.parent = current;
                        break;
                    }
                }
                else // if (item.CompareTo(current.item) == 0)
                {
                    //똑같은 값을 찾았을 경우
                    //중복 무시
                    return false;
                }
            }
            return true;
        }

        public bool Remove(T item)
        {
            Node<T> findNode = FindNode(item);
            if (findNode == null)
            {
                return false;
            }
            else
            {
                EraseNode(findNode);
                return true;
            }
        }

        public bool Contains(T item)
        {
            Node<T> findNode = FindNode(item);
            //return findNode != null ? true : false;
            if(findNode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Clear()
        {
            root = null;
        }

        private Node<T> FindNode(T item)
        {
            if (root == null)
                return null;

            Node<T> current = root;
            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    //왼쪽으로 가는 경우
                    current = current.left;
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    //오른쪽으로 가는 경우
                    current = current.right;
                }
                else // if (item.CompareTo(current.item) == 0)
                {
                    //똑같은 것을 발견한 경우
                    return current;//찾았으니 반환
                }
            }
            return null; //모든 반복에도 못찾으면 null반환
        }

        private void EraseNode(Node<T> node)
        {
            if (node.HasNoChild)
            {
                //자식이 없는 경우
                if (node.IsLeftChild)//왼쪽 자식인 경우
                    node.parent.left = null;//부모의 왼쪽 자식 null
                else if (node.IsRightChild)//오른쪽 자식인 경우
                    node.parent.right = null;//부모의 오른쪽 자식 null
                else//(node.IsRootNode)
                    root = null; 
            }
            else if (node.HasLeftChild || node.HasRightChild)
            {
                //자식이 1개인 경우
                Node<T> parent = node.parent;
                Node<T> child = node.HasLeftChild ? node.left : node.right;
                
                // 부모와 자식을 연결해주고 삭제
                if (node.IsLeftChild)
                {
                    parent.left = child;
                    child.parent = parent;
                }
                else if (node.IsRightChild)
                {
                    parent.right = child;
                    child.parent = parent;
                }
                else // if (node.IsRootNode)
                {
                    root = child;
                    child.parent = null;
                }
            }
            else // if (node.HasBothChild)
            {
                //자식이 2개인 경우
                Node<T> nextNode = node.right;//
                while (nextNode.left != null) //왼쪽 자식이 없을때까지 반복
                {
                    nextNode = nextNode.left; 
                }
                node.item = nextNode.item; //값을 가져오고
                EraseNode(nextNode); //삭제
            }
        }

        private class Node<T> //Node 클래스 생성
        {
            public T item;
            public Node<T> parent;
            public Node<T> left;
            public Node<T> right;

            public Node(T item, Node<T> parent, Node<T> left, Node<T> right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            public bool IsRootNode { get { return parent == null; } } //부모가 null일때
            public bool IsLeftChild { get { return parent != null && parent.left == this; } } //부모가 있으면서 왼쪽 자식이 나 일때 
            public bool IsRightChild { get { return parent != null && parent.right == this; } }//부모가 있으면서 오른쪽 자식이 나일때

            public bool HasNoChild { get { return left == null && right == null; } } //왼쪽 오른쪽 자식이 null인 경우
            public bool HasLeftChild { get { return left != null && right == null; } } //왼쪽 자식만 있는 경우
            public bool HasRightChild { get { return left == null && right != null; } } //오른쪽 자식만 있는 경우
            public bool HasBothChild { get { return left != null && right != null; } } //왼쪽 오른족 자식이 null이 아닌경우
        }
    }
}