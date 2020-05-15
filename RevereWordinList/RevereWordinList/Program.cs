using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevereWordinList
{
    /// <summary>
    /// Class to define the structure of Node class
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="next"></param>
        public Node(char data, Node next)
        {
            Data = data;
            Next = next;
        }
        /// <summary>
        /// Hold the data as character
        /// </summary>
        public char Data;

        /// <summary>
        /// Pointer to next Node
        /// </summary>
        public Node Next;
    }
    class RerveseWord
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //string s = "practice makes a man perfect";
            string s = "practice makes a man perfect";
            //Convert given string to a linked list with character as data in each node
            var header = CreateLinkedList(s);
            Console.WriteLine("Before :");
            PrintList(header);
            header = Reverse(header);
            Console.WriteLine("\nAfter :");
            PrintList(header);
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Reverse the given linked list con
        /// </summary>
        /// <param name="header">Header of the linked list</param>
        /// <returns></returns>
        private static Node Reverse(Node header)
        {
            if (header == null)
                return header;
            Node cur = header, next, pre = null, wordFirstLetter = header , prevSpace =null;
            header = null;
            while (cur != null)
            {
                if (cur.Data == ' ')
                {
                    if(header==null)
                        header = pre;
                    //header = header == null ? pre : header;
                    if (prevSpace != null)
                        prevSpace.Next = pre;
                    prevSpace = cur;
                    wordFirstLetter.Next = cur;
                    wordFirstLetter = cur.Next;
                    cur = cur.Next;
                }
                next = cur.Next;
                cur.Next = pre;
                pre = cur;
                cur = next;
             }
            if (prevSpace != null)
                prevSpace.Next = pre;
            else
                header = pre; // if there is only one word in the entire sentance.
            wordFirstLetter.Next = null;
            
            return RevereLinkedList(header);
        }

        private static Node RevereLinkedList(Node header)
        {
            Node cur = header, next, pre = null;
            while (cur != null)
            {
                next = cur.Next;
                cur.Next = pre;
                pre = cur;
                cur = next;
            }
            return pre;
        }

        /// <summary>
        /// Method to create a linked list from a given string. It converts each character to a node data.
        /// Converts 'practice makes a man perfect' to 'p->r->a->c->t->i->c->e-> ->m->a->k->e->s-> ->a-> ->m->a->n-> ->p->e->r->f->e->c->t->null'
        /// </summary>
        /// <param name="s"></param>
        /// <returns>header of the linked list</returns>
        private static Node CreateLinkedList(string s)
        {
            Node header = null;
            Node temp = null;
            //Generates characters from the given string
            foreach (char c in s)
            {
                Node node = new Node(c, null);

                if (header == null)
                {
                    header = node;
                    temp = header;
                }
                else
                {
                    temp.Next = node;
                    temp = temp.Next;
                }
            }
            return header;
        }

        /// <summary>
        /// Method to print linked list
        /// </summary>
        /// <param name="Header">Header of the linked list</param>
        private static void PrintList(Node Header)
        {
            Node temp = Header;
            //Navigate till the end and print the data in each node
            while (temp != null)
            {
                Console.Write(temp.Data);
                temp = temp.Next;
            }
        }
    }
}







