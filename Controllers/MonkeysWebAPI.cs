using CalculatorWebAPI.DTO;
using CalculatorWebAPI.Models;
using CollectionViewLesson.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class MonkeysWebAPI : ControllerBase
    {
        private static MonkeyList list = new MonkeyList();
        
        [HttpGet("monkey")]
        public IActionResult ReadAllMonkeys()
        {
            try
            {
                MonkeyList l = list;
                MonkeyListDto ret = new MonkeyListDto();
                for (int i = 0; i < l.Monkeys.Count; i++)
                {
                    MonkeyDto m = new MonkeyDto
                    {
                        Name = l.Monkeys[i].Name,
                        Location = l.Monkeys[i].Location,
                        Details = l.Monkeys[i].Details,
                        ImageUrl = l.Monkeys[i].ImageUrl,
                        IsFavorite = l.Monkeys[i].IsFavorite
                    };
                    ret.Monkeys.Add(m);
                }
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("monkeyname")]
        public IActionResult ReadMonkey([FromQuery] string name)
        {
            try
            {
                MonkeyList l = list;
                bool found = false;
                Monkey m = new Monkey();
                for (int i = 0; i < l.Monkeys.Count; i++)
                {
                    if (found == false && l.Monkeys[i].Name == name)
                    {
                            m.Name = l.Monkeys[i].Name;
                            m.Location = l.Monkeys[i].Location;
                            m.Details = l.Monkeys[i].Details;
                            m.ImageUrl = l.Monkeys[i].ImageUrl;
                            m.IsFavorite = l.Monkeys[i].IsFavorite;
                       
                        found = true;
                    }
                }
                if (found == false)
                {
                    return NotFound();
                }
                else
                {
                    MonkeyDto ret = new MonkeyDto
                    {
                        Name = m.Name,
                        Location = m.Location,
                        Details = m.Details,
                        ImageUrl = m.ImageUrl,
                        IsFavorite = m.IsFavorite
                    };
                    return Ok(ret);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("monkey")]
        public IActionResult AddMonkey([FromBody] MonkeyDto monkey)
        {
            try
            {
                Monkey m = new Monkey
                {
                    Name = monkey.Name,
                    Location = monkey.Location,
                    Details = monkey.Details,
                    ImageUrl = monkey.ImageUrl,
                    IsFavorite = monkey.IsFavorite
                };
                
                bool nameExist = false;
                for (int i = 0; i < list.Monkeys.Count; i++)
                {
                    if (list.Monkeys[i].Name == m.Name)
                        nameExist = true;
                }
                if (nameExist != true)
                {
                    list.Monkeys.Add(m);
                    return Ok();
                }
                else
                {
                    return BadRequest("name already exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
