﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuzzleN.Model;


namespace PuzzleN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartGame(int size)
        {
            Board b = new Board();
            var metrix = b.InitBoard(size,true);
                  
            return View(metrix);
        }

        public IActionResult Move(string position, string arr, int size)
        {
            Board b = new Board();
            if (position==null)
            {
               
                var metrix = b.InitBoard(size,true);
            }
            else
            { 
            int[] pos = position.Split('.').Select(int.Parse).ToArray(); //x,y positions from the buttons
            int[] nums = arr.Split(',').Select(int.Parse).ToArray();

                // Board b = new Board();
                 
                var m = b.InitBoard(Convert.ToInt32(Math.Sqrt(nums.Length)),false);
                b.Matrix = b.FillMatrixFromArray(m, nums);

           var newmetrix = b.MoveTile(pos[0], pos[1]);
                if (b.CheckSolved(b.Matrix))
                    return Content("You Win :)");

            var cr = new JsonResult(b.Matrix);
            }
            return View(b.Matrix);
            
        }
    }
}
