using EastWestTestTask;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Information("Start test application");

        try
        {
            new Startup();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
        finally
        {
            Log.Information("Application end");
        }
    }
}