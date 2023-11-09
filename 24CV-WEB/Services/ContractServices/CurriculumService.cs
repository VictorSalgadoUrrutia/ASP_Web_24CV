﻿using _24CV_WEB.Models;
using _24CV_WEB.Repository;
using _24CV_WEB.Repository.CurriculumDAO;
using _24CV_WEB.Services.Contracts;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace _24CV_WEB.Services.ContractServices
{
	public class CurriculumService : ICurriculumService
	{
		private CurriculumRepository _repository;

        public CurriculumService(ApplicationDbContext context)
        {
            _repository = new CurriculumRepository(context);
        }

        public async Task<ResponseHelper> Create(Curriculum model)
		{
			ResponseHelper responseHelper = new ResponseHelper();

			try
			{
				string filePath="";
				string fileName = "";

                if (model.Foto != null && model.Foto.Length > 0)
				{
					fileName = Path.GetFileName(model.Foto.FileName);
					filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Fotos", fileName);
				}

				model.RutaFoto = fileName;

				//Copia el archivo en un directorio
				using (var fileStream = new FileStream(filePath,FileMode.Create))
				{
					await model.Foto.CopyToAsync(fileStream);
				}

				if (_repository.Create(model) > 0)
				{
					responseHelper.Success = true;
					responseHelper.Message = $"Se agregó el curriculum de {model.Nombre} con éxito.";
				}
				else
				{
					responseHelper.Message = "Ocurrió un error al agregar el dato.";
				}
			}
			catch (Exception e)
			{
				responseHelper.Message = $"Ocurrió un error al agregar el dato. Error: {e.Message}";
			}


			return responseHelper;	
		}

        public async Task<ResponseHelper> Delete(int id)
        {
            ResponseHelper responseHelper = new ResponseHelper();
            try
            {
                if (_repository.Delete(id) > 0)
                {
                    responseHelper.Success = true;
                    responseHelper.Message = $"Se elimino con exito";
                }
                else
                {
                    responseHelper.Message = "Ocurrió un error al agregar el dato.";
                }
            }
            catch (Exception e)
            {
                responseHelper.Message = $"Ocurrio un error al eliminar el dato. Error: {e.Message}";
            }

            return responseHelper;
        }

        public List<Curriculum> GetAll()
		{
			List<Curriculum> list = new List<Curriculum>();

			try
			{
				list = _repository.GetAll();
			}
			catch (Exception e)
			{

				throw;
			}

			return list;
		}

		public Curriculum GetById(int id)
		{
			try
			{
				Curriculum model = new Curriculum();
				model = _repository.GetById(id);



				return model;
			}
			catch (Exception e)
			{

				throw;
			}
		}


		

		public ResponseHelper Update(Curriculum model)
		{
			throw new NotImplementedException();
		}

        Task<ResponseHelper> ICurriculumService.Update(Curriculum model)
        {
            throw new NotImplementedException();
        }
    }
}
