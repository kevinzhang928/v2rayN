using System.Diagnostics;

namespace ServiceLib.Common;

public static class ProcUtils
{
    private static readonly string _tag = "ProcUtils";

    public static void ProcessStart(string? fileName, string arguments = "")
    {
        ProcessStart(fileName, arguments, null);
    }

    public static int? ProcessStart(string? fileName, string arguments, string? dir)
    {
        if (fileName.IsNullOrEmpty())
        {
            return null;
        }
        try
        {
            if (fileName.Contains(' ')) fileName = fileName.AppendQuotes();
            if (arguments.Contains(' ')) arguments = arguments.AppendQuotes();

            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = dir
                }
            };
            process.Start();
            return process.Id;
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
        }
        return null;
    }

    public static void RebootAsAdmin(bool blAdmin = true)
    {
        try
        {
            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = true,
                Arguments = Global.RebootAs,
                WorkingDirectory = Utils.StartupPath(),
                FileName = Utils.GetExePath().AppendQuotes(),
                Verb = blAdmin ? "runas" : null,
            };
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Logging.SaveLog(_tag, ex);
        }
    }
}