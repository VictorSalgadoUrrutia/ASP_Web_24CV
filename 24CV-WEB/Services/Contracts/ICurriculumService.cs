﻿using _24CV_WEB.Models;

namespace _24CV_WEB.Services.Contracts
{
	public interface ICurriculumService
	{
        List<Curriculum> GetAll();
        Curriculum GetById(int id);
        Task<ResponseHelper> Create(Curriculum model);
        Task<ResponseHelper> Update(Curriculum model);
        Task<ResponseHelper> Delete(int id);
    }
}
