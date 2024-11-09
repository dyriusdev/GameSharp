using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameSharp;

public class Game : GameWindow {
    
    private Shader shader;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default,
        new NativeWindowSettings() { Size = (width, height), Title = title }) {
        shader = new Shader();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }
    }

    protected override void OnLoad() {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1);

        float[] vertices = {
            -0.5f, -0.5f, 0, // Bottom-left vertex
            0.5f, -0.5f, 0, // Bottom-right vertex
            0, 0.5f, 0 // Top vertex
        };
        
        int vbo = GL.GenBuffer(); // VBO (Vertex Buffer Object)
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        int vao = GL.GenVertexArray(); // VAO (Vertex Array Object)
        GL.BindVertexArray(vao);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        
        shader.Use();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.EnableVertexAttribArray(0);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DisableVertexAttribArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        
        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
}