namespace System.Collections.Generic
{
    public class LList<T>
    {
        public class Node
        {
            public T Item { get; }
            public Node Next { get; set; }
            public Node(T item)
            { 
                this.Item = item;
            }
        }
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public int Count { get; private set; }
        
        // Operations
        public void AddLast(T item) 
        { 
            Node node = new Node(item);
            
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
            Node node = new Node(item);
                
            if (Head == null)
            { 
                Head = node;
                Tail = node;
            }
            if (Count == 1)
            { 
                Node temp = Head; 
                node.Next = temp; 
                Head = node;
                Tail = temp;
            }
            if (Count > 1)
            { 
                Node temp = Head; 
                node.Next = temp; 
                Head = node;
            }
            Count++;
        }

        public void Insert(T item, int index)
        { 
            Node newNode = new Node(item);
            
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
            Node node = Head;
            while (node.Next != null)
            {
                if (counter == index) 
                { 
                    Node temp = node.Next; 
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
            Node node = Head;
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
            Node node = Head; 
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

            Node node = Head;
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
            Node node = Head;
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
    }
}
