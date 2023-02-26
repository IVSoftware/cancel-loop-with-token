One of many ways to achieve your outcome is to apply your cancellation token _directly_ to `await Task.Delay(100, ct)` inside the loop.

***
    static void Main(string[] args)
    {
        Console.Title = "Test Cancellation Token";
        CancellationTokenSource _cts = new CancellationTokenSource();

        writeAsync(_cts.Token)
            .GetAwaiter()
            .OnCompleted(()=>Console.WriteLine("CANCELLED"));

        async Task writeAsync(CancellationToken ct)
        {
            int numberOfWrite = 0;
            while (true)
            {
                await Task.Delay(100, ct);
                Console.WriteLine(++numberOfWrite);
            }
        }

        Console.WriteLine("Waiting for cancel");
        Thread.Sleep(1000);
        _cts.Cancel();
        Console.ReadKey();
    }

[![console output][1]][1]


  [1]: https://i.stack.imgur.com/FJU8X.png