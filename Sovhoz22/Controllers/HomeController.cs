using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sovhoz22.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sovhoz22.Models.UserModels;
using Sovhoz22.Models.PhoneModels;
using Sovhoz22.Models.OrderModels;
using Sovhoz22.Models.CommentModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Sovhoz22.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _appEmvironment;
        private UserManager<User> _userManager;
        MobileContext db;
        public HomeController(MobileContext context,  UserManager<User> user, IHostingEnvironment hostingEnvironment)
        {
            db = context;
            _userManager = user;
            _appEmvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }
        [HttpPost]
        public IActionResult Index(string ByPrice,string ByModel)
        {
            if(ByPrice=="desc")
            {
                return View(db.Phones.OrderByDescending(x => x.Price).ToList());
            }else if (ByPrice == "asc")
            {
                return View(db.Phones.OrderBy(x => x.Price).ToList());
            }
            else if (ByModel == "NonAlph")
            {
                return View(db.Phones.OrderByDescending(x => x.Company).ThenByDescending(x=>x.Name).ToList());
            }
            else if (ByModel == "Alph")
            {
                return View(db.Phones.OrderBy(x => x.Company).ThenBy(x=>x.Name).ToList());
            }
            else if (ByPrice == "desc" && ByModel == "NonAlph")
            {
                return View(db.Phones.OrderByDescending(x => x.Company).ThenByDescending(x=>x.Name).ThenByDescending(x=>x.Price).ToList());
            }
            else if (ByPrice == "asc" && ByModel == "Alph")
            {
                return View(db.Phones.OrderBy(x=>x.Company).ThenBy(x=>x.Name).ThenBy(x=>x.Price).ToList());
            }
            else if (ByPrice == "desc" && ByModel == "Alph")
            {
                return View(db.Phones.OrderBy(x => x.Company).ThenBy(x=>x.Name).ThenByDescending(x=>x.Price).ToList());
            }
            else if(ByPrice=="asc"&&ByModel=="Non Alph")
            {
                return View(db.Phones.OrderByDescending(x => x.Company).ThenByDescending(x=>x.Name).ThenBy(x=>x.Price).ToList());
            }
            return View();
        }
        [HttpGet]
        public IActionResult PhoneInfo(int? id)
        {
            Phone p = db.Phones.FirstOrDefault(x => x.Id == id);
            if (p != null)
            {
                ViewBag.Id = id;
                ViewBag.Company = p.Company;
                ViewBag.Name = p.Name;
                ViewBag.Price = p.Price;
                return View(db.Comments.Where(x=>x.Phone==id).OrderByDescending(x=>x.Time).ToList());
            }else {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PhoneInfo(CreateCommentModel model)
        {
            await CreateComment(model);
            return RedirectToAction("PhoneInfo");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult AddPhone()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task< IActionResult> AddPhone(EditPhoneViewModel model)
        {
            Phone phone = new Phone { Name = model.Name, Company = model.Company, Price = model.Price };
            if (model.PhoneImage != null)
            {
                byte[] imageData = null;
                using(var binaryReader=new BinaryReader(model.PhoneImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.PhoneImage.Length);
                }
                phone.PhoneImage = imageData;
            }
            await db.Phones.AddAsync(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("AddPhone");
        }

        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public async Task<string> Buy(Order order)
        {
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return "Спасибо, " + order.User + ", за покупку!";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult EditForm()
        {
            return View(db.Phones.ToList());
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult EditPhone(int? id)
        {
            Phone p = db.Phones.FirstOrDefault(x => x.Id == id);
            ViewBag.Company = p.Company;
            ViewBag.Name = p.Name;
            ViewBag.Price = p.Price;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task< IActionResult> EditPhone(EditPhoneViewModel model)
        {
            Phone phone = db.Phones.FirstOrDefault(x => x.Id == model.Id);
                if (phone != null)
                {
                    phone.Name = model.Name;
                    phone.Company = model.Company;
                    phone.Price = model.Price;
                    if (model.PhoneImage != null)
                    {
                    byte[] imageData = null;
                        using(var binaryReader=new BinaryReader(model.PhoneImage.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)model.PhoneImage.Length);
                        }
                    }
                    db.Phones.Update(phone);
                    await db.SaveChangesAsync();
                    return RedirectToAction("EditForm");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неправильная форма");
                }
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task< IActionResult> DeletePhone(int? id)
        {
            Phone phone =  db.Phones.FirstOrDefault(x=>x.Id==id);
            if (phone != null)
            {
                db.Phones.Remove(phone);
                await db.SaveChangesAsync();
                return RedirectToAction("EditForm");
            }
            else
            {
                return NotFound();
            } 
        }
        public async Task CreateComment(CreateCommentModel model)
        {
            CommentModel comment = new CommentModel { Id = db.Comments.Max(x => x.Id)+1, Phone = model.Phone, Text = model.Text, Time = DateTime.Now, UserName = model.UserName, Rating = model.Rating };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }
        
        
    }
}