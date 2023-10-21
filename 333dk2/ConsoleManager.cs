//gör så den ritar ut från en dictionary ksk så att key vector2 t.ex 1,2 ska va ett i, kanske gör det lättare i framtiden?
//▒ ser ut som bricks typ ■ pixel nästan? □, https://en.wikipedia.org/wiki/Geometric_Shapes_(Unicode_block)

using System.Diagnostics;
using System.Numerics;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")] // tar bort problem messages
public static class ConsoleManager
{
    //ändra 1920, 1080 till en sak som faktiskt fungerar på alla skärmar
    readonly static float rowtopixelWidth = 1920 / Console.LargestWindowWidth;
    readonly static float rowtopixelHeight = 1080 / Console.LargestWindowHeight;
    public static Dictionary<Vector2, string> Grid = new();
    public static int maxY = 0;
    public static int maxX = 0;
    private static string _currentScreen = "Console";

    //property för current screen så att när currentScreen ändras blir det också en hardclear på skärm canvasen 
    public static string currentScreen
    {
        get
        {
            return _currentScreen;
        }
        set
        {
            _currentScreen = value;
            hardClear();
        }
    }

    
    public static void InitConsole()
    {
        //console använder rows, columns istället för pixlar därför behöver converta, pixelwidth/height inte perfekt men fungerar typ
        int windowsPixelWidth = 512;
        //--för någon anledning är max 1071 på en 1080 skärm?,,,,, 512 / 400
        // ifall man har 2+ skärmar tror den att resolutionen är bådas, lite knasigt
        int windowsPixelHeight = 400;
        int windowsWidth = (int)MathF.Floor(windowsPixelWidth / rowtopixelWidth);
        int windowsHeight = (int)MathF.Floor(windowsPixelHeight / rowtopixelHeight);
        Console.SetWindowSize(windowsWidth, windowsHeight);
        
        //skapar en grid där allting är tomt by default.
        for (int x = 0; x < windowsWidth; x++)
        {
            for (int y = 0; y < windowsHeight; y++)
            {
                Vector2 test = new(x, y);
                Grid.Add(test, " ");
                maxY = y;
            }
            maxX = x;
        }

    }
    //gör allt tomt dock borde jag nog ha en 1 border för att bordern ska var kvar
    public static void hardClear()
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                Vector2 test = new(x, y);
                Grid[test] = " ";
            }
        }
    }
    //uppdaterar det på skärmen
    public static void Update()
    {
        File.WriteAllText(@"Screen.txt", string.Empty);
        Console.Clear();
        //draw
        for (int y = 0; y < maxY; y++)
        {
            string Line = string.Empty;
            for (int x = 0; x < maxX; x++)
            {
                Line += Grid[new Vector2(x, y)];
            }
            //refresh
            File.AppendAllText(@"Screen.txt", Line + Environment.NewLine);
        }
        string[] textFromFile = File.ReadAllLines(@"Screen.txt");
        foreach (string line in textFromFile)
        {

            Console.Write(line + "\n");
            //använder write istället för writeline för färger behöver ändras mitt i bilden, och de är beroende av texten
        }
        
    }


    //-------------------------------------------------------------------------------------------------------------//    
    //-------------------------------------------------==Drawing==-------------------------------------------------//
    //-------------------------------------------------------------------------------------------------------------//


    //------------=text=------------//    
    private static void RenderText(string Text, Vector2 Position)
    {
        Position.X -= Text.Length / 2;

        for (int i = 0; i < Text.Length; i++)
        {
            string letter = Text[i].ToString();
            Grid[new Vector2(Position.X + i, Position.Y)] = letter;
        }

    }

    private static (string, Vector2) LoadText(int Location)
    {
        string Text = File.ReadLines(@"text.txt").ElementAtOrDefault(Location);
        string[] textSplit = Text.Split("|");
        Debug.WriteLine(textSplit);
        Vector2 text1vector = new(int.Parse(textSplit[1]), int.Parse(textSplit[2]));
        return (textSplit[0], text1vector);
    }

    public static void AddText(int textLine, string[] pivot)
    {
        (string text, Vector2 location) = LoadText(textLine);
        location = AddPivot(pivot, location);
        RenderText(text, location);
    }

    //------------=sprite=------------//    
    
    public static void RenderSprite(int start, int end, int xOffset, int yOffset)
    {
        List<string> sprite = new();
        
        for (int i = start; i <= end; i++)
        {
            string Text = File.ReadLines(@"sprite.txt").ElementAtOrDefault(i);
            sprite.Add(Text);
        }

        foreach (string line in sprite)
        {
            
            for (int i = 0; i < line.Length; i++)
            {
                string letter = line[i].ToString();
                Grid[new Vector2(xOffset + i, yOffset)] = letter;
            }
            yOffset++;
        }
    }
    //-------=något=-----//
    public static Vector2 AddPivot(string[] locations, Vector2 Offset)
    {
    int x = maxX/2;
    int y = maxY/2;
    
        foreach (string location in locations)
        {
            if (location == "Left")
                x = 2; // +1 för att den längst till vänster ska existera +1 för border
            else if (location == "Right")
                x = maxX;
            else if (location == "Top")
                y = 0;
            else if (location == "Bottom")
                y = maxY;
        }
    
    Vector2 Pivot = new(x,y);
    Pivot = Vector2.Add(Pivot, Offset);
    return Pivot;
    
    }
}