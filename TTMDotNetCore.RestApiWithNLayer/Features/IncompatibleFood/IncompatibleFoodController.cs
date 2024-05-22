using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TTMDotNetCore.RestApiWithNLayer.Features.IncompatibleFood;

[Route("api/[controller]")]
[ApiController]
public class IncompatibleFoodController : ControllerBase
{
    private async Task<IncompatibleFood> GetDataAsync()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("IncompatibleFood.json");
        var model = JsonConvert.DeserializeObject<IncompatibleFood>(jsonStr);
        return model;
    }

    [HttpGet]
    public async Task<IActionResult> GetIncompatibleFood(string? description)
    {
        var model = await GetDataAsync();
        if (description is null)
        {
            return Ok(model);
        }

        var filteredFoods = model.Tbl_IncompatibleFood.Where(food => food.Description.Trim().Contains(description.Trim()));

        return Ok(filteredFoods);
    }

    public class IncompatibleFood
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
}
