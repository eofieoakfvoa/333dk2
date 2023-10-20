using System.Data;
using System.Diagnostics;
public static class InputHandler
{
    static List<int> Options = new();

    static int selectedOption = 1;
    private static ConsoleKeyInfo GetKey()
    {
        ConsoleKeyInfo temp = Console.ReadKey(true); //true gör så det man skriver inte kommer i consolen
        return temp;
    }
    public static int OptionSelection(string Active)
    {
        while (Active == "True")
        {
            //Får en list av knappar där den kollar ifall någon av knapparna är klickade sen så skickar den tillbaka?,
            //försöka komma på något sätt för att inte hardcoda in menyer?
            ConsoleKeyInfo key = GetKey();
            Debug.WriteLine("hi");
            if (key.Key == ConsoleKey.RightArrow)
            {
                    return 1;

            }
            if (key.Key == ConsoleKey.LeftArrow)
            {
                    return -1;
            }
        }
            Debug.WriteLine("hi2");
        return 0;
    }

    //Up Down funktion som ändrar Selectedoption??
    //Left to right funktion 
    public static int MenuSelection(string Active, List<int> Options, String ClickAble)
    {
        while (Active == "True")
        {
            //Får en list av knappar där den kollar ifall någon av knapparna är klickade sen så skickar den tillbaka?,
            //försöka komma på något sätt för att inte hardcoda in menyer?
            ConsoleKeyInfo key = GetKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (selectedOption == 1)
                {
                    selectedOption = Options.Count;
                    Debug.WriteLine(selectedOption);
                }
                else
                {
                    selectedOption--;
                    Debug.WriteLine(selectedOption);
                }

            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                if (selectedOption == Options.Count)
                {
                    selectedOption = 1;
                    Debug.WriteLine(selectedOption);
                }
                else
                {
                    selectedOption++;
                    Debug.WriteLine(selectedOption);
                }
            }
            if (key.Key == ConsoleKey.C)
            {
                Active = "False";
                ClickAble = string.Empty;
                return selectedOption;
            }
        }
        return 0;
    }
}