using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThucHanh01.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThucHanh01.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();

        public StudentController()
        {
            if (listStudents.Count == 0)
            {
                listStudents = new List<Student>()
                {
                    new Student()
                    {
                        Id = 101,
                        Name = "Hải Nam",
                        Branch = Branch.IT,
                        Gender = Gender.Male,
                        IsRegular = true,
                        Address = "A1-2018",
                        Email = "nam@gmail.com",
                        Password = "Abcd@123",
                        Diem = 8.0,
                        DateOfBorth = new DateTime(2003,1,1),
                        AvatarPath = "/images/default.png"
                    },
                    new Student()
                    {
                        Id = 102,
                        Name = "Minh Tuấn",
                        Branch = Branch.BE,
                        Gender = Gender.Male,
                        IsRegular = true,
                        Address = "A1-2019",
                        Email = "tuan@gmail.com",
                        Password = "Abcd@123",
                        Diem = 7.5,
                        DateOfBorth = new DateTime(2002,5,5),
                        AvatarPath = "/images/default.png"
                    },
                    new Student()
                    {
                        Id = 103,
                        Name = "Hoàng Phong",
                        Branch = Branch.CE,
                        Gender = Gender.Male,
                        IsRegular = false,
                        Address = "A1-2020",
                        Email = "phong@gmail.com",
                        Password = "Abcd@123",
                        Diem = 9.0,
                        DateOfBorth = new DateTime(2001,3,3),
                        AvatarPath = "/images/default.png"
                    },
                    new Student()
                    {
                        Id = 104,
                        Name = "Mai Cúc",
                        Branch = Branch.EE,
                        Gender = Gender.Female,
                        IsRegular = false,
                        Address = "A1-2021",
                        Email = "cuc@gmail.com",
                        Password = "Abcd@123",
                        Diem = 8.8,
                        DateOfBorth = new DateTime(2003,10,10),
                        AvatarPath = "/images/default.png"
                    }
                };
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            LoadDropdowns();
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Student s, IFormFile AvatarFile)
        {
            if (ModelState.IsValid)
            {
                HandleAvatarUpload(s, AvatarFile);
                s.Id = listStudents.Last().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }

            LoadDropdowns();
            return View(s);
        }

        [HttpPost]
        public IActionResult Add(Student s, IFormFile AvatarFile)
        {
            if (ModelState.IsValid)
            {
                HandleAvatarUpload(s, AvatarFile);
                s.Id = listStudents.Last().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }

            LoadDropdowns();
            return View("Create", s);
        }

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult List()
        {
            return View("Index", listStudents);
        }

        private void HandleAvatarUpload(Student s, IFormFile AvatarFile)
        {
            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AvatarFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    AvatarFile.CopyTo(stream);
                }

                s.AvatarPath = "/images/" + fileName;
            }
            else
            {
                s.AvatarPath = "/images/default.png";
            }
        }

        private void LoadDropdowns()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
        }
    }
}
