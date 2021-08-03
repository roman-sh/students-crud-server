
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using AutoMapper;

using VimExercise.Logic;

namespace VimExercise.DataAccess
{
    /// <inheritdoc/>
    public class InMemoryJsonFileDoctorRepository : IDoctorRepository
    {
        private readonly string filePath = "../VimExercise.DataAccess/providers.json";
        /// <inheritdoc/>
        //not necessary for this part. 
        public void Add(Doctor entity)
        {
            throw new System.NotImplementedException();
        }
        /// <inheritdoc/>
        public IEnumerable<Doctor> GetSuitableDoctorsSorted(DateTimeOffset appoitmentTime, Specialty requiredSpecialty, double scoreThreshold)
        {
            if(scoreThreshold < 0 || scoreThreshold > 10)
                throw new ArgumentException("Invalid minimum score. Score should be between 0 - 10");
            return GetAllDoctors()
                .Where(doc => doc.CanScheduleAppointment(appoitmentTime))
                .Where(doc => doc.Score >= scoreThreshold)
                .Where(doc => doc.Specialties.Contains(requiredSpecialty))
                .OrderByDescending(doc => doc);
        }
        /// <inheritdoc/>
        public Doctor TryGetById(string entityIdentifier)
        {
            return GetAllDoctors()
                .Where(doc => doc.Name == entityIdentifier)
                .SingleOrDefault();
        }
        private IEnumerable<Doctor> GetAllDoctors()
        {
            string jsonDoctorData = File.ReadAllText(filePath);
            IEnumerable<StoredDoctor> storedDoctors = JsonSerializer.Deserialize<IEnumerable<StoredDoctor>>(jsonDoctorData);
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StoredToBuisnessObjectsProfile());
            });
            var mapper = config.CreateMapper();
            IEnumerable<Doctor> doctors = mapper.Map<IEnumerable<Doctor>>(storedDoctors);

            return doctors;
        }
    }
}
