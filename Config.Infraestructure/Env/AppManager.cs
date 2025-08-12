namespace Config.Infraestructure.Env;

public class AppManager(AppConfig appConfig)
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName
    ?? Directory.GetCurrentDirectory();
    private string ScriptRoot => Path.Combine(_solutionRoot, "Config.Infraestructure", "Env", "scripts");
    public AppConfig AppConfig { get; } = appConfig;

    public void Install()
    {
        var apkFolder = Path.Combine(_solutionRoot, "apks");
        RunScript("install_apks.sh", apkFolder);
    }

    public void Uninstall()
    {
        RunScript("uninstall_apks.sh", AppConfig.AppName);
    }
    private void RunScript(string scriptName, string argument)
    {
        var fullPath = Path.Combine(ScriptRoot, scriptName);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"Script not found: {fullPath}");
        }

        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = $"\"{fullPath}\" \"{argument}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi) ?? throw new Exception($"Failed to start: bash {fullPath}");
        string line;
        while ((line = process.StandardOutput.ReadLine()) != null)
        {
            if (line.Trim() == "Success")
            {
                continue;
            }

            Logger.Info($"{line}");
        }

        while ((line = process.StandardError.ReadLine()) != null)
        {
            Logger.Error($"{line}");
        }

        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new Exception($"Script failed with code {process.ExitCode}: {scriptName}");
    }
}