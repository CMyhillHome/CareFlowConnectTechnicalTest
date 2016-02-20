using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using RestApi.Controllers;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Tests
{
    [TestFixture]
    public class PatientsControllerTests
    {
        private UnityContainer _iocContainer = new UnityContainer();
        private PatientsController _patientsController;
        private int _validEpisodeId = 1;
        private int _validPatientId = 1;
        private int _invalidPatientId = 3;

        [OneTimeSetUp]
        public void SetupTestEnvironment()
        {
            //Arrange
            _iocContainer.RegisterType<IDatabaseContext, InMemoryPatientContext>(new HierarchicalLifetimeManager());
            _patientsController = _iocContainer.Resolve(typeof(PatientsController)) as PatientsController;

            CreatePatientRecords();
        }

        [Test]
        public void VerifyThatPatientRecordIsReturnedCorrectlyAndCheckDataReturned()
        {
            // Act
            var patientRecord = _patientsController.Get(_validPatientId);

            //Assert
            Assert.That(patientRecord, Is.Not.Null);
            Assert.That(patientRecord.PatientId, Is.EqualTo(_validPatientId));
            Assert.That(patientRecord.Episodes, Is.Not.Null);
            Assert.That(patientRecord.Episodes.Count(), Is.EqualTo(1));
            Assert.That(patientRecord.Episodes.First().PatientId, Is.EqualTo(_validPatientId));
            Assert.That(patientRecord.Episodes.First().EpisodeId, Is.EqualTo(_validEpisodeId));

            //Verify Patient Data
            Assert.That(patientRecord.FirstName, Is.EqualTo("Christopher"));
            Assert.That(patientRecord.LastName, Is.EqualTo("Myhill"));
            Assert.That(patientRecord.DateOfBirth, Is.EqualTo(new DateTime(1978, 03, 18)));
            Assert.That(patientRecord.Episodes.First().Diagnosis, Is.EqualTo("Headache"));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void VerifyThatPatientRecordsAreReturnedCorrectly(int patientId, int episodeId)
        {
            // Act
            var patientRecord = _patientsController.Get(patientId);

            //Assert
            Assert.That(patientRecord, Is.Not.Null);
            Assert.That(patientRecord.PatientId, Is.EqualTo(patientId));
            Assert.That(patientRecord.Episodes, Is.Not.Null);
            Assert.That(patientRecord.Episodes.Count(), Is.EqualTo(1));
            Assert.That(patientRecord.Episodes.First().PatientId, Is.EqualTo(patientId));
            Assert.That(patientRecord.Episodes.First().EpisodeId, Is.EqualTo(episodeId));
        }

        [Test]
        public void Return404WhenPatientRecordDoesNotExist()
        {
            HttpResponseException httpException = null;
            // Act
            try
            {
                //Could use a Assert.Thows here but I wanted to validate the http response code as it should be 404
                _patientsController.Get(_invalidPatientId);
            }
            catch (Exception ex)
            {
                httpException = ex as HttpResponseException;
            }

            //Assert
            Assert.That(httpException, Is.Not.Null);
            Assert.That(httpException.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private void CreatePatientRecords()
        {
            var database = _iocContainer.Resolve<IDatabaseContext>();
           
            database.Episodes.Add(new Episode()
            {
                AdmissionDate = DateTime.Now,
                Diagnosis = "Headache",
                EpisodeId = _validEpisodeId,
                PatientId = _validPatientId
            });

            database.Patients.Add(new Patient()
            {
                DateOfBirth = new DateTime(1978, 03, 18),
                FirstName = "Christopher",
                LastName = "Myhill",
                NhsNumber = "1234567",
                PatientId = _validPatientId
            });

            // Second record to make sure we can handle more than 1 record
            database.Episodes.Add(new Episode()
            {
                AdmissionDate = DateTime.Now,
                Diagnosis = "Itchy Knee",
                EpisodeId = _validEpisodeId + 1,
                PatientId = _validPatientId + 1
            });

            database.Patients.Add(new Patient()
            {
                DateOfBirth = new DateTime(1980, 01, 16),
                FirstName = "James",
                LastName = "Cameron",
                NhsNumber = "987654",
                PatientId = _validPatientId + 1
            });
        }
    }
}
