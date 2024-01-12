using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datastructure
{
    public class LinkedList<T>
    {
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;

        public LinkedList() 
        {
            head = null;
            tail = null;
            count = 0;
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(value);
           
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            InsertNodeBefore(node, newNode);
            return newNode;
        }

        private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {

        }
    }   

    public class LinkedListNode<T>
    {
        private T value;

        public LinkedListNode<T> prev;
        public LinkedListNode<T> next;

        public LinkedListNode(T value)
        {
            this.value = value;
            this.prev = null;
            this.next = null;
        }
        public LinkedListNode(T value, LinkedListNode<T> prev, LinkedListNode<T> next)
        {
            this.value = value;
            this.prev = prev;
            this.next = next;
        }

        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedListNode<T> Next { get { return next; } }
          public T Value { get { return value; } set { this.value = value; } }
    }
}
