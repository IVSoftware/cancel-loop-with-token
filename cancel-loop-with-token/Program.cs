namespace cancel_loop_with_token
{
    internal class Program
    {
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
                    await Task.Delay(1000, ct);
                    Console.WriteLine(++numberOfWrite);
                }
            }

            Console.WriteLine("Waiting for cancel");
            Thread.Sleep(10000);
            _cts.Cancel();
            Console.ReadKey();
        }
    }
}