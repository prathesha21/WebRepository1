using AutoMapper;
using System;
using WebRepository1.Domain;
using WebRepository1.Models.Dtos;
using WebRepository1.Models.Entities;

namespace WebRepository1.Interface
{
    public interface ISchoolRepository
    {
        public List<School> GetAll();

        public School GetById(int id);
        public SchoolDetailscs Add(School school);

        public SchoolDetailscs Uppdatedata(int id, School school);
        public void Delete(int id);
    }
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;
        public SchoolRepository(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // Methos 1:
        public List<School> GetAll()
        {
            var a = dbContext.StudentSchool.ToList();
            var b = mapper.Map<List<School>>(a);
            return b;
        }

        public School GetById(int id)
        {
            var ch = dbContext.StudentSchool.Find(id);
            var gg = mapper.Map<School>(ch);
            return gg;


        }
        public SchoolDetailscs Add(School school)
        {
            var scl = mapper.Map<SchoolDetailscs>(school);
            dbContext.StudentSchool.Add(scl);
            dbContext.SaveChanges();
            return scl;
        }


        public SchoolDetailscs Uppdatedata(int id, School school)
        {
            var gcc=dbContext.StudentSchool.Find(id);
            var bg = mapper.Map(school, gcc);
            dbContext.StudentSchool.Update(gcc);
            dbContext.SaveChanges();
            return bg;
        }

        public void Delete(int id)
        {
            var glc = dbContext.StudentSchool.Find(id);
            dbContext.StudentSchool.Remove(glc);
            dbContext.SaveChanges();

        }

    }
}
