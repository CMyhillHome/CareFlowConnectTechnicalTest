using System;

namespace RestApi.Models
{
    public class Episode
    {
        public int EpisodeId { get; set; }

        public int PatientId { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime DischargeDate { get; set; }

        public string Diagnosis { get; set; }
    }
}