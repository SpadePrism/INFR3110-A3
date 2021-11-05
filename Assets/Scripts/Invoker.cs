using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    private static Queue<ICommand> commandBuffer;

    private static List<ICommand> commandHistory;
    static int counter;

    private bool dirty_;

    // Start is called before the first frame update
    private void Start()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();

        dirty_ = false;
    }

    public static void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }

    // Update is called once per frame
    private void Update()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);
            counter++;
            Debug.Log("Command History length: " + commandHistory.Count);
        }

        if (dirty_)
        {
            List<string> lines = new List<string>();

            foreach(ICommand c in commandHistory)
            {
                lines.Add(c.ToString());
            }

            System.IO.File.WriteAllLines(Application.dataPath + "/logfile.txt", lines);

            dirty_ = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            dirty_ = true;
        }

    }
}