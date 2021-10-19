using System;
using System.Diagnostics;

namespace L2
{
    public static class JsonPythonDeserialize
    {
        public static void Run()
        {
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = "Assets",
                Arguments = "customjsonoperations.py",
                FileName = "python3",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false
            };

            var process = Process.Start(psi);
            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();

            Console.WriteLine(output);
        }
    }
}