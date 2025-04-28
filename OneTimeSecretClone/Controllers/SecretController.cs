using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
//using OneTimeSecretClone.Context;
using OneTimeSecretClone.Models;
using OneTimeSecretClone.Services;

namespace OneTimeSecretClone.Controllers
    {
    public class SecretController : Controller
    //{
    //private readonly ISecretService _secretService= new SecretServiceImpl();
        {
        private readonly ISecretService _secretService;
        //private IMapper _mapper;
        public SecretController(ISecretService secretService)
            {
            this._secretService = secretService;
            //this._mapper = mapper;
            }

        //[Route("index")]
        public IActionResult Index()
            {
            return View();
            }


        [HttpPost]
        public IActionResult GenerateSecretLink(SecretModel secret)
            {
            if (ModelState.IsValid)
                {
                string generatedLink = this._secretService.GenerateLink(secret);
                ViewData["message"] = "Link generated successfully!";
                ViewData["link"] = generatedLink;

                }
            //return RedirectToAction("Index","Secret");
            return View("Index");
            }


        [HttpGet]
        [Route("Secret/ViewMessage/{secretId:int}")]
        public IActionResult ViewMessage(int secretId)
            {
            ViewData["SecretId"] = secretId;
            return View(new ResponseDto());
            }

        [HttpPost]
        public IActionResult ViewMessage(int secretId, string password)
            {
            ResponseDto response = _secretService.ViewMessageHandler(secretId, password);
            return View("ViewMessage", response);
            }

        //[HttpPost]
        //[Route("Secret/ViewMessage/{secretId:int}/{password:alpha}")]
        //public IActionResult ViewMessage(int secretId, string password)
        //    {
        //    ResponseDto response = this._secretService.ViewMessageHandler(secretId,password);
        //    return View("ViewMessage",response);
         
        //    }
        }
    }
    
