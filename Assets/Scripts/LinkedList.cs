using UnityEngine;

namespace System.Collections.Generic
{
    public interface ILinkedList<T>
    {
        int Count { get; }
        void AddLast(T item);
        void AddFirst(T item);
        void Insert(T item, int index);
        int IndexOf(T item);
        T ItemOf(int index);
        bool Contains(T item);
        void Remove(T item);
        void RemoveAt(int index);
        public void Write();

        public T this[int index] { get; set; }
    }
    
    public class LList<T> : ILinkedList<T>
    {
        public class ListNode
        {
            public T Item { get; }
            public ListNode Next { get; set; }
            public ListNode(T item)
            { 
                this.Item = item;
            }
        }
        public ListNode Head { get; private set; }
        public ListNode Tail { get; private set; }
        public int Count { get; private set; }
        
        // Operations
        public void AddLast(T item) 
        { 
            ListNode node = new ListNode(item);
            
            if (Head == null) 
            { 
                Head = node;
                Tail = node;
            }
            
            if (Count == 1)
            { 
                Tail = node; 
                Head.Next = Tail;
            }
            if (Count > 1)
            { 
                Tail.Next = node; 
                Tail = node;
            }
            Count++;
        }
            
        public void AddFirst(T item)
        {
            ListNode node = new ListNode(item);
                
            if (Head == null)
            { 
                Head = node;
                Tail = node;
            }
            if (Count == 1)
            { 
                ListNode temp = Head; 
                node.Next = temp; 
                Head = node;
                Tail = temp;
            }
            if (Count > 1)
            { 
                ListNode temp = Head; 
                node.Next = temp; 
                Head = node;
            }
            Count++;
        }

        public void Insert(T item, int index)
        { 
            ListNode newNode = new ListNode(item);
            
            if (index == 0)
            { 
                AddFirst(item); 
                return;
            }
                
            if (index >= Count)
            { 
                AddLast(item); 
                return;
            }

            int counter = 1;
            ListNode node = Head;
            while (node.Next != null)
            {
                if (counter == index) 
                { 
                    ListNode temp = node.Next; 
                    node.Next = newNode; 
                    newNode.Next = temp; 
                    Count++;
                    return;
                }
                node = node.Next;
                counter++;
            }
        }
            
        public int IndexOf(T item)
        {
            int counter = 0;
            ListNode node = Head;
            while (node != null)
            { 
                if (node.Item.Equals(item)) 
                { 
                    return counter;
                } 
                counter++; 
                node = node.Next;
            }
            return -1;
        }

        public T ItemOf(int index)
        {
            int counter = 0;
            ListNode node = Head; 
            while (node != null)
            { 
                if (counter == index) 
                { 
                    return node.Item; 
                }
                counter++; 
                node = node.Next;
            }
            return default;
        }
            
        public bool Contains(T item)
        { 
            return IndexOf(item) != -1;
        }
            
        public void Remove(T item)
        { 
            if (Head.Item.Equals(item))
            { 
                Head = Head.Next; 
                Count--; 
                return; 
            }

            ListNode node = Head;
            while (node.Next != null)
            { 
                if (node.Next.Item.Equals(item)) 
                { 
                    node.Next = node.Next.Next; 
                    Count--; 
                    return;
                } 
                node = node.Next;
            }
        }

        public void RemoveAt(int index)
        {
            if (index == 0)
            { 
                Head = Head.Next; 
                Count--; 
                return; 
            }

            int counter = 1;
            ListNode node = Head;
            while (node.Next != null)
            { 
                if (counter == index) 
                { 
                    node.Next = node.Next.Next; 
                    Count--; 
                    return; 
                } 
                node = node.Next; 
                counter++;
            }
        }
        
        // Indexer
        public T this[int index]
        {
            get => ItemOf(index);
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Invalid index: " + index);
                }
                Insert(value, index);
            }
        }

        // Methods
        public void Write()
        {
            Console.WriteLine("Linked List");
            Console.WriteLine("> Head: " + Head.Item);
            Console.WriteLine("> Tail: " + Tail.Item);
            Console.WriteLine("> Count: " + Count);
            Console.WriteLine("-----------");
            int counter = 0; 
            ListNode node = Head;
            while (node != null)
            {
                Console.WriteLine(counter + ": " + node.Item);
                counter++;
                node = node.Next;
            }
            Console.WriteLine("-----------");
        }
    }
}
