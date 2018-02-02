//This project implements a task for sorting images with Stroop effects.
//The C# program monitors if each image is sorted correctly.

using System;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Windows.Forms;

public class Watcher
{
    static string name, mode;
    static System.Media.SoundPlayer player;
    static List<string> answers;
    static int correct,wrong;
    static StreamWriter up;

    public static void Main()
    {
        correct = 0;
        wrong = 0;
        Console.WriteLine("Type the name of the participant:");
        name = Console.ReadLine();
        Console.WriteLine("Congruent mode? (y/n):");
        string option = Console.ReadLine();
        if (option.Equals("y"))
            mode = "congruent";
        else
            mode = "incongruent";
        Console.WriteLine("Monitoring " + name + "/" + mode + "...");
        RunMonitor();
        Application.Run();
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    public static void RunMonitor()
    {
        // Create a new FileSystemWatcher and set its properties.
        FileSystemWatcher watcher = new FileSystemWatcher();
        // the path is set to the visual studio project
        string appPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        player = new System.Media.SoundPlayer(appPath+"\\audio\\fail.wav");
        watcher.Path = appPath+"\\"+name + "\\" + mode;

        // log the user performance
        int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        up = new StreamWriter(watcher.Path + "\\" + epoch + "_log.csv");
        up.WriteLine("#Correct,#Wrong,#Total");
        up.Flush();

        // read the answers from the file
        answers = readAnswer(watcher.Path);

        /* Watch for changes in LastAccess and LastWrite times, and
           the renaming of files or directories. */
        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        // Only watch text files.
        watcher.Filter = "*.png";
        watcher.IncludeSubdirectories = true;

        // Add event handlers.
        watcher.Created += new FileSystemEventHandler(OnCreated);

        // Begin watching.
        watcher.EnableRaisingEvents = true;
    }

    // Define the event handlers.
    private static void OnCreated(object source, FileSystemEventArgs e)
    {
        // Specify what is done when a file is changed, created, or deleted.
        //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        String[] substrings = e.FullPath.Split('\\');

        String[] fileName = substrings[substrings.Length - 1].Split('.');
        int fileNumber = Int32.Parse(fileName[0]);
        string submission = substrings[substrings.Length - 2];

        if (!submission.Equals(answers[fileNumber]))
        {
            if (mode.Equals("incongruent"))
                player.Play();
            wrong++;
        }
        else
        {
            correct++;
        }
        Console.WriteLine("### Correct: " + correct + "  Wrong:" + wrong + "  Total:" + (correct + wrong) + " ###");
        up.WriteLine(correct + "," + wrong + "," + (correct + wrong));
        up.Flush();
    }
    // read the answer from the ans files
    private static List<string> readAnswer(string path)
    {
        List<string> answers = new List<string>();
        string line;
        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader(path+"\\ans.txt");

        while ((line = file.ReadLine()) != null)
        {
            String[] substrings = line.Split('.');
            answers.Add(substrings[1]);
        }
        file.Close();
        return answers;
    }
}