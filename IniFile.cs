using System.Collections.Generic;
using System.IO;

public static class IniFile
{
    public static Dictionary<string, Dictionary<string, string>> ReadIniFile(string filePath)
    {
        var data = new Dictionary<string, Dictionary<string, string>>();
        string currentSection = string.Empty;

        try
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";"))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Trim('[', ']');
                    if (!data.ContainsKey(currentSection))
                        data[currentSection] = new Dictionary<string, string>();
                }
                else if (trimmedLine.Contains("=") && !string.IsNullOrEmpty(currentSection))
                {
                    var parts = trimmedLine.Split('=', 2);
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    data[currentSection][key] = value;
                }
            }

            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return data;
    }

    public static void WriteIniFile(string filePath, Dictionary<string, Dictionary<string, string>> data)
    {
        using (var writer = new StreamWriter(filePath))
        {
            bool isFirstSection = true;

            foreach (var section in data)
            {
                // Skip writing the first newline
                if (!isFirstSection)
                {
                    writer.WriteLine();
                }

                // Write the section header
                writer.WriteLine($"[{section.Key}]");

                // Write each key-value pair in the section
                foreach (var pair in section.Value)
                {
                    writer.WriteLine($"{pair.Key}={pair.Value}");
                }

                isFirstSection = false;
            }
        }
    }
}