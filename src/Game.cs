using OpenTK.Windowing.Desktop;

namespace GameSharp;

public class Game : GameWindow {

    public Game(int width, int height, String title) : base(GameWindowSettings.Default, 
            new NativeWindowSettings() { Size = (width, height), Title = title}) {}
    
    
}