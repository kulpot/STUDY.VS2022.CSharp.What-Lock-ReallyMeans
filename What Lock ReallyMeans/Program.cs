using System;

//ref link:https://www.youtube.com/watch?v=RILGBo8V2rA&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=21
// ctrl+m+o -- collapse all line
// ctrl+k+d -- align all
// lock statement - needed in instance member like Enqueue and Dequeue

/*
class MainClass
{
    static Queue<int> numbers = new Queue<int>(); // Queue Structure -- requires knowledge in data structures
    static Random rand = new Random(987); // output total 41
    const int NumThreads = 3;
    static int[] sums = new int[NumThreads]; // for total added array
    static void ProduceNumbers()    // Producing Method
    {
        for (int i = 0; i < 10; i++)
        {
            int numToEnqueue = rand.Next(10);
            //numbers.Enqueue(rand.Next(10));
            Console.WriteLine("Producing thread adding " + numToEnqueue + " to the queue.");
            lock(numbers)
                numbers.Enqueue(numToEnqueue);
            Thread.Sleep(rand.Next(1000));
        }
    }
    static void SumNumbers(object threadNumber)   // Consuming Method
    {   //---------poorman's method of synchronization technique---------- needs improvements
        DateTime startTime = DateTime.Now;
        int mySum = 0;
        while ((DateTime.Now - startTime).Seconds < 11)
        {
            int numToSum = -1;
            lock (numbers) // lock statement - enable queue properly
            {
                if (numbers.Count != 0)
                {
                    numToSum = numbers.Dequeue();
                }
            }
            if (numToSum != -1)
            {
                mySum += numToSum;
                Console.WriteLine("Consuming thread #"
                    + threadNumber + " adding "
                    + numToSum + " to its total sum making "
                    + numToSum + " for the thread total.");
            }
        }
        sums[(int)threadNumber] = mySum;
    }
    static void Main()
    {
        var producingThread = new Thread(ProduceNumbers);
        producingThread.Start();
        Thread[] threads = new Thread[NumThreads];
        for (int i = 0; i < NumThreads; i++)
        {
            threads[i] = new Thread(SumNumbers);
            threads[i].Start(i);
        }
        for (int i = 0; i < NumThreads; i++)
            threads[i].Join();
        int totalSum = 0;
        for (int i = 0; i < NumThreads; i++)
            totalSum += sums[i];
        Console.WriteLine("Done adding. Total is " + totalSum);
    }
}*/

//      ----Lock Sample------------
// locking -- CLR resource locking block 

class BathroomStall
{
    public void BeUsed(int userNumber)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Being used by " + userNumber);
            Thread.Sleep(500);
        }
    }
}

class MainClass
{
    static BathroomStall stall = new BathroomStall();
    static void Main()
    {
        for (int i = 0; i < 3; i++)
            new Thread(RegularUsers).Start();
        new Thread(TheWeirdGuy).Start();
    }
    static void RegularUsers()
    {
        lock (stall)
            stall.BeUsed(Thread.CurrentThread.ManagedThreadId);
    }
    static void TheWeirdGuy()
    {
        Monitor.Enter(stall);
        stall.BeUsed(99);
        Monitor.Exit(stall);
    }
}