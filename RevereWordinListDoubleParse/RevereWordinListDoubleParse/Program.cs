using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevereWordinListDoubleParse
{
    public class LinkedList
    {
        Node head; // head of list

        /* Linked list Node*/
        class Node
        {
            public char data;
            public Node next;
            public Node(char d)
            {
                data = d;
                next = null;
            }
        }

        Node reverse(Node head, int k)
        {
            if (head == null)
                return null;
            Node current = head;
            Node next = null;
            Node prev = null;
            
            /* Reverse first k nodes of linked list */
            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
                if (current != null && current.data == ' ')
                {
                    head = head.next = current;
                    next = current.next;
                    break;
                }

            }

            /* next is now a pointer to (k+1)th node
                Recursively call for the list starting from
               current. And make rest of the list as next of
               first node */
            if (next != null)
                head.next = reverse(next, k);

            // prev is now head of input list
            return prev;
        }

        /* Utility functions */

        /* Inserts a new Node at front of the list. */
        public void push(char new_data)
        {
            if (head == null)
                head = new Node(new_data);
            else
            {
                Node temp = head;
                while (temp.next != null)
                    temp = temp.next;

                Node new_node = new Node(new_data);
                temp.next = new_node;
            }

        }

        /* Function to print linked list */
        void printList()
        {
            Node temp = head;
            while (temp != null)
            {
                Console.Write(temp.data);
                temp = temp.next;
            }
            Console.WriteLine();
        }

        /* Driver code*/
        public static void Main(String[] args)
        {
            LinkedList llist = new LinkedList();

            /* Constructed Linked List is 1->2->3->4->5->6->
            7->8->8->9->null */
            string input = "This is a Program";
            foreach (var c in input)
                llist.push(c);


            Console.Write("Given Linked List : ");
            llist.printList();

            llist.head = llist.reverse(llist.head, 3);

            //Console.WriteLine("Reversed list");
            //llist.printList();

            llist.head = llist.reverse(llist.head);

            Console.Write("Reversed list : ");
            llist.printList();
        }

        Node reverse(Node head)
        {
            if (head == null)
                return null;
            Node current = head;
            Node next = null;
            Node prev = null;



            /* Reverse first k nodes of linked list */
            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;


            }
            return prev;
        }
    }
}
