using OpenTK.Graphics.OpenGL;

namespace GameSharp;

public class Shader  {
    private int program, vertexShader, fragmentShader;
    private bool disposed = false;
    
    public Shader() {
        string vertexSrc = "#version 330 core\nlayout (location = 0) in vec3 position;\n\nvoid main() {\n    gl_Position = vec4(position, 1);\n}";
        string fragmentSrc = "#version 330 core\n\nout vec4 outColor;\n\nvoid main() {\n    outColor = vec4(1, 0.5f, 0.2f, 1);\n}";

        vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSrc);
        
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSrc);
        
        GL.CompileShader(vertexShader);
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vertexCompiled);
        if (vertexCompiled == 0) {
            string info = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(info);
        }
        
        GL.CompileShader(fragmentShader);
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fragmentCompiled);
        if (fragmentCompiled == 0) {
            string info = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(info);
        }
        
        program = GL.CreateProgram();
        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);
        GL.LinkProgram(program);
        
        GL.GetProgram(program, ProgramParameter.LinkStatus, out int programLinked);
        if (programLinked == 0) {
            string info = GL.GetProgramInfoLog(program);
            Console.WriteLine(info);
        }
        
        GL.DetachShader(program, vertexShader);
        GL.DetachShader(program, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Bind() {
        GL.UseProgram(program);
    }
    
    public void UnBind() {
        GL.UseProgram(0);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing) {
        if (!disposed) {
            GL.DeleteProgram(program);
            disposed = true;
        }
    }
    
    ~Shader() {
        if (disposed == false) {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
}