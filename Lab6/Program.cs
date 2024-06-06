public interface ICommand
{
    void Execute();
    void Unexecute();
}

public class Shape
{
    public string Name { get; set; }

    public Shape(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}

public class Canvas
{
    private List<Shape> _shapes = new List<Shape>();

    public void AddShape(Shape shape)
    {
        _shapes.Add(shape);
        Console.WriteLine($"Added: {shape}");
    }

    public void RemoveShape(Shape shape)
    {
        _shapes.Remove(shape);
        Console.WriteLine($"Removed: {shape}");
    }

    public void ShowShapes()
    {
        Console.WriteLine("Current shapes on canvas:");
        foreach (var shape in _shapes)
        {
            Console.WriteLine(shape);
        }
    }
}

public class ShapeManager
{
    private Canvas _canvas;

    public ShapeManager(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void AddShape(Shape shape)
    {
        _canvas.AddShape(shape);
    }

    public void RemoveShape(Shape shape)
    {
        _canvas.RemoveShape(shape);
    }
}

public class AddShapeCommand : ICommand
{
    private ShapeManager _shapeManager;
    private Shape _shape;

    public AddShapeCommand(ShapeManager shapeManager, Shape shape)
    {
        _shapeManager = shapeManager;
        _shape = shape;
    }

    public void Execute()
    {
        _shapeManager.AddShape(_shape);
    }

    public void Unexecute()
    {
        _shapeManager.RemoveShape(_shape);
    }
}

public class Invoker
{
    private Stack<ICommand> _commands = new Stack<ICommand>();
    private Stack<ICommand> _undoneCommands = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commands.Push(command);
        _undoneCommands.Clear(); 
    }

    public void Undo()
    {
        if (_commands.Count > 0)
        {
            var command = _commands.Pop();
            command.Unexecute();
            _undoneCommands.Push(command);
        }
    }

    public void Redo()
    {
        if (_undoneCommands.Count > 0)
        {
            var command = _undoneCommands.Pop();
            command.Execute();
            _commands.Push(command);
        }
    }
}

class Program
{
    static void Main()
    {
        var canvas = new Canvas();
        var shapeManager = new ShapeManager(canvas);
        var invoker = new Invoker();

        var circle = new Shape("Circle");
        var square = new Shape("Square");

        var addCircleCommand = new AddShapeCommand(shapeManager, circle);
        var addSquareCommand = new AddShapeCommand(shapeManager, square);

        invoker.ExecuteCommand(addCircleCommand);
        invoker.ExecuteCommand(addSquareCommand);
        canvas.ShowShapes();

        invoker.Undo();
        canvas.ShowShapes();

        invoker.Redo();
        canvas.ShowShapes();
    }
}
