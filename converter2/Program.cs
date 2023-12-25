namespace converter2
{
    namespace converter
    {
        using System;
        using System.IO;
        using System.Xml.Serialization;
        using Newtonsoft.Json;

        namespace TextEditor
        {
            public class Figure
            {
                public string Name { get; set; }
                public int Width { get; set; }
                public int Height { get; set; }

                public Figure(string name, int width, int height)
                {
                    Name = name;
                    Width = width;
                    Height = height;
                }

                public Figure()
                {
                }
            }

            public class FileManager
            {
                private string filepath;

                public FileManager(string path)
                {
                    filepath = path;
                }

                public void LoadFile()
                {
                    if (File.Exists(filepath))
                    {
                        string extension = Path.GetExtension(filepath).ToLower();

                        switch (extension)
                        {
                            case ".txt":
                                LoadTxt();
                                break;
                            case ".json":
                                LoadJson();
                                break;
                            case ".xml":
                                LoadXml();
                                break;
                            default:
                                Console.WriteLine("Неподдерживаемый формат файла!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Файл не существует!");
                    }
                }

                public void SaveFile()
                {
                    string extension = Path.GetExtension(filepath).ToLower();

                    switch (extension)
                    {
                        case ".txt":
                            SaveTxt();
                            break;
                        case ".json":
                            SaveJson();
                            break;
                        case ".xml":
                            SaveXml();
                            break;
                        default:
                            Console.WriteLine("Неподдерживаемый формат файла!");
                            break;
                    }
                }

                private void LoadTxt()
                {
                    string[] lines = File.ReadAllLines(filepath);

                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }

                private void LoadJson()
                {
                    string json = File.ReadAllText(filepath);

                    Figure figure = JsonConvert.DeserializeObject<Figure>(json);

                    Console.WriteLine($"Имя: {figure.Name}");
                    Console.WriteLine($"Ширина: {figure.Width}");
                    Console.WriteLine($"Высота: {figure.Height}");
                }

                private void LoadXml()
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Figure));

                    using (StreamReader reader = new StreamReader(filepath))
                    {
                        Figure figure = (Figure)serializer.Deserialize(reader);

                        Console.WriteLine($"Имя: {figure.Name}");
                        Console.WriteLine($"Ширина: {figure.Width}");
                        Console.WriteLine($"Высота: {figure.Height}");
                    }
                }

                private void SaveTxt()
                {
                    Console.WriteLine("Введите содержимое файла:");
                    string content = Console.ReadLine();

                    File.WriteAllText(filepath, content);

                    Console.WriteLine("Файл сохранен в формате txt!");
                }

                private void SaveJson()
                {
                    Console.WriteLine("Введите имя фигуры:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите ширину фигуры:");
                    int width = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите высоту фигуры:");
                    int height = Convert.ToInt32(Console.ReadLine());

                    Figure figure = new Figure(name, width, height);
                    string json = JsonConvert.SerializeObject(figure);

                    File.WriteAllText(filepath, json);

                    Console.WriteLine("Файл сохранен в формате json!");
                }

                private void SaveXml()
                {
                    Console.WriteLine("Введите имя фигуры:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Введите ширину фигуры:");
                    int width = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите высоту фигуры:");
                    int height = Convert.ToInt32(Console.ReadLine());

                    Figure figure = new Figure(name, width, height);

                    XmlSerializer serializer = new XmlSerializer(typeof(Figure));

                    using (StreamWriter writer = new StreamWriter(filepath))
                    {
                        serializer.Serialize(writer, figure);
                    }

                    Console.WriteLine("Файл сохранен в формате xml!");
                }
            }

            public class TextEditor
            {
                private FileManager fileManager;

                public TextEditor(string filepath)
                {
                    fileManager = new FileManager(filepath);
                }

                public void Start()
                {
                    fileManager.LoadFile();

                    while (true)
                    {
                        Console.WriteLine("Нажмите F1 для сохранения файла или ESC для выхода.");

                        ConsoleKeyInfo keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.F1)
                        {
                            fileManager.SaveFile();
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
            }

            public class Program
            {
                public static void Main(string[] args)
                {
                    Console.WriteLine("Введите путь к файлу:");
                    string filepath = Console.ReadLine();

                    TextEditor textEditor = new TextEditor(filepath);
                    textEditor.Start();
                }
            }
        }

    }
}