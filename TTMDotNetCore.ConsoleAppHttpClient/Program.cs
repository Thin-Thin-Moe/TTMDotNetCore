// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");
string jsonStr = await File.ReadAllTextAsync("IncompatibleFood.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);
Console.WriteLine(model.Tbl_IncompatibleFood);

foreach (var food in model.Tbl_IncompatibleFood)
{
    Console.WriteLine(food.FoodA);
}
Console.ReadLine();


public class MainDto
{
    public Tbl_Incompatiblefood[] Tbl_IncompatibleFood { get; set; }
}

public class Tbl_Incompatiblefood
{
    public int Id { get; set; }
    public string FoodA { get; set; }
    public string FoodB { get; set; }
    public string Description { get; set; }
}
