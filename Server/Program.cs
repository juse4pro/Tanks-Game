using System.Diagnostics;
using Server;

Console.WriteLine("Starting server...");
GameServer server = new GameServer();
server.Start();


const int targetTicksPerSeconds = 50;
TimeSpan targetTimePerTick = TimeSpan.FromSeconds(1.0 / targetTicksPerSeconds);

Stopwatch stopwatch = new();

// Main update loop
while (true)
{
	stopwatch.Restart();
	TimeSpan elapsed = stopwatch.Elapsed;

	try
	{
		server.Update((float)elapsed.TotalSeconds);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}

	TimeSpan sleepTime = targetTimePerTick - elapsed;

	Thread.Sleep(sleepTime);
}

server.Stop();