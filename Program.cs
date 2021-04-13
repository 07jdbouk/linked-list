using System;

namespace linked_list
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            list.testAddItem();
            // list.testDisplay();
            // list.testDeleteItem();
        }
    }

    class LinkedList
    {
        // Create large array (128 values) and a size variable and start pointer
        int[,] array = new int[128,2];
        int start = 0;

        // Next available space is start of array
        int nextAvailable = 0;

        public void testDisplay()
        {
            array[0,0] = 7;
            array[1,0] = 5;
            array[2,0] = 9;
            array[3,0] = 4;
            array[4,0] = 10;
            array[5,0] = 8;
            array[6,0] = 6;
            array[7,0] = 3;
            
            array[0,1] = 5;
            array[1,1] = 6;
            array[2,1] = 4;
            array[3,1] = 1;
            array[4,1] = -1;
            array[5,1] = 2;
            array[6,1] = 0;
            array[7,1] = 3;

            start = 7;

            display();
        }

        public void testAddItem()
        {
            addItem(1);
            addItem(5);
            addItem(7);
            addItem(3);
            addItem(6);
            addItem(4);
            addItem(8);
            addItem(2);

            display();
            //throw new NotImplementedException();
        }

        public void testDeleteItem()
        {
            addItem(6);
            addItem(3);
            addItem(7);
            addItem(2);
            addItem(5);

            delete(6);
            delete(2);

            display();
        }

        public void display()
        {
            // pointer acts as 'count' / 'i'
            int pointer = start;

            while (pointer != -1)
            {
                // Output data at pointer ('count') and 'increments' pointer to the pointer of this data
                Console.WriteLine(array[pointer,0]);
                pointer = array[pointer,1];
            }
        }

        public void addItem(int item)
        {
            // Must check 2 consecutive data values. If the first is lower than item and the second is larger, insert item there.
            // If the item with pointer = -1 is still smaller, append item
            // If the first value is larger, insert item at start pointer
            //
            // To insert item, previous pointer points to new value and item's pointer takes previous pointer
            // If there are less than two items (i.e. 0 or 1), special 
            
            array[nextAvailable,0] = item;

            if (nextAvailable < 2)
            {
                if (nextAvailable == 0)
                    array[0,1]= -1;
                else if (array[0,0] > item)
                    array[1,1] = 0;
                else
                {
                    array[1,1] = -1;
                    array[0,1] = 1;
                }
            }
            else
            {
                if (item < array[0,0])
                    array[nextAvailable,1] = 0;
                else 
                {
                    int prevPointer = start;
                    int nextPointer = array[prevPointer,1];

                    while (prevPointer != -1)
                    {
                        if (nextPointer == -1 && array[prevPointer,0] < item) {
                            array[nextAvailable,1] = -1;
                            array[prevPointer,1] = nextAvailable;
                        }
                        else if (array[prevPointer,0] < item && array[nextPointer,0] > item)
                        {
                            array[nextAvailable,1] = array[prevPointer,1];
                            array[prevPointer,1] = nextAvailable;
                        }
                        
                        if (nextPointer > -1)
                        {
                            prevPointer = nextPointer;
                            nextPointer = array[prevPointer,1];
                        }
                        else
                            break;
                    }

                    if (prevPointer == -1)
                    {
                        array[nextAvailable,1] = prevPointer;
                        array[prevPointer,1] = nextAvailable;
                    }
                }
            }

            nextAvailable++;
        }

        public int delete(int item)
        {
            int pointer = start;
            int nextPointer = array[start,1];

            while (pointer != -1)
            {
                if (array[nextPointer,0] == item)
                {
                    array[pointer,1] = array[nextPointer,1];
                    array[nextPointer,0] = 0;
                    break;
                }
                else {
                    pointer = array[pointer,1];
                    nextPointer = array[nextPointer,1];
                }
            }

            return -1;
        }
    }
}
