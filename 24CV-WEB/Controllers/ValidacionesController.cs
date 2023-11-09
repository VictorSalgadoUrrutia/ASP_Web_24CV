﻿using _24CV_WEB.Models;
using _24CV_WEB.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace _24CV_WEB.Controllers
{
    public class ValidacionesController : Controller
    {
        private readonly ICurriculumService _curriculumService;

        public ValidacionesController(ICurriculumService curriculumService)
        {
            _curriculumService = curriculumService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarInformacion(Curriculum model) {

            string mensaje = "";
            //model.RutaFoto = "FakePath";

            if (ModelState.IsValid)
            {
                var response = _curriculumService.Create(model).Result;

                mensaje = response.Message;
                TempData["msj"] = mensaje;
                return RedirectToAction("Index");
            }
            else
            {
                mensaje = "Datos incorrectos";
                TempData["msj"] = mensaje;

                return View("Index",model);
            }

        }

        public IActionResult Lista()
        {
            return View(_curriculumService.GetAll());
        }

        public IActionResult CView(int id)
        {
            Curriculum model = new Curriculum();
            model= _curriculumService.GetById(id);
            return View(model);
        }

        public async Task<IActionResult> DeleteInfo(int id)
        {
            string mensaje = "";
            var response = await _curriculumService.Delete(id);
            mensaje = response.Message;
            TempData["msj"] = mensaje;
            return RedirectToAction("Lista");
        }

    }
}
